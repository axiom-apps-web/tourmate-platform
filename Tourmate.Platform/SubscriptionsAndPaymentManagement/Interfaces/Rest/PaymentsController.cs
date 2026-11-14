
using Microsoft.AspNetCore.Mvc;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.CommandServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Application.QueryServices;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Commands;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.Queries;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Domain.Model.ValueObjects;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Resources;
using Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest.Transform;

namespace Tourmate.Platform.SubscriptionsAndPaymentManagement.Interfaces.Rest;

/// <summary>
/// REST controller for managing payment records.
/// </summary>
[ApiController]
[Route("api/v1/payments")]
[Produces("application/json")]
public class PaymentsController(
    IPaymentCommandService commandService,
    IPaymentQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Retrieves all payment records.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentResource>>> GetAll(
        CancellationToken cancellationToken)
    {
        var payments = await queryService.Handle(
            new GetAllPaymentsQuery(),
            cancellationToken);

        var resources = payments.Select(
            PaymentResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a payment by its identifier.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaymentResource>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var payment = await queryService.Handle(
            new GetPaymentByIdQuery(new PaymentId(id)),
            cancellationToken);

        if (payment is null)
            return NotFound();

        return Ok(
            PaymentResourceFromEntityAssembler
                .ToResourceFromEntity(payment));
    }

    /// <summary>
    /// Creates a new payment record.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaymentResource>> Create(
        [FromBody] CreatePaymentResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                CreatePaymentCommandFromResourceAssembler
                    .ToCommandFromResource(resource);

            var payment = await commandService.Handle(
                command,
                cancellationToken);

            var paymentResource =
                PaymentResourceFromEntityAssembler
                    .ToResourceFromEntity(payment);

            return CreatedAtAction(
                nameof(GetById),
                new { id = payment.Id.Value },
                paymentResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing payment record.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PaymentResource>> Update(
        int id,
        [FromBody] UpdatePaymentResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                UpdatePaymentCommandFromResourceAssembler
                    .ToCommandFromResource(id, resource);

            var payment = await commandService.Handle(
                command,
                cancellationToken);

            if (payment is null)
                return NotFound();

            return Ok(
                PaymentResourceFromEntityAssembler
                    .ToResourceFromEntity(payment));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an existing payment record.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeletePaymentCommand(
            new PaymentId(id));

        var deleted = await commandService.Handle(
            command,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
