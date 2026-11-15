
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
/// REST controller for managing agency subscriptions.
/// </summary>
[ApiController]
[Route("api/v1/subscriptions")]
[Produces("application/json")]
public class SubscriptionsController(
    ISubscriptionCommandService commandService,
    ISubscriptionQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Retrieves all agency subscriptions.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubscriptionResource>>> GetAll(
        CancellationToken cancellationToken)
    {
        var subscriptions = await queryService.Handle(
            new GetAllSubscriptionsQuery(),
            cancellationToken);

        var resources = subscriptions.Select(
            SubscriptionResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a subscription by its identifier.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubscriptionResource>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var subscription = await queryService.Handle(
            new GetSubscriptionByIdQuery(new SubscriptionId(id)),
            cancellationToken);

        if (subscription is null)
            return NotFound();

        return Ok(
            SubscriptionResourceFromEntityAssembler
                .ToResourceFromEntity(subscription));
    }

    /// <summary>
    /// Creates a new agency subscription.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SubscriptionResource>> Create(
        [FromBody] CreateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                CreateSubscriptionCommandFromResourceAssembler
                    .ToCommandFromResource(resource);

            var subscription = await commandService.Handle(
                command,
                cancellationToken);

            var subscriptionResource =
                SubscriptionResourceFromEntityAssembler
                    .ToResourceFromEntity(subscription);

            return CreatedAtAction(
                nameof(GetById),
                new { id = subscription.Id.Value },
                subscriptionResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing agency subscription.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<SubscriptionResource>> Update(
        int id,
        [FromBody] UpdateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                UpdateSubscriptionCommandFromResourceAssembler
                    .ToCommandFromResource(id, resource);

            var subscription = await commandService.Handle(
                command,
                cancellationToken);

            if (subscription is null)
                return NotFound();

            return Ok(
                SubscriptionResourceFromEntityAssembler
                    .ToResourceFromEntity(subscription));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an existing agency subscription.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSubscriptionCommand(
            new SubscriptionId(id));

        var deleted = await commandService.Handle(
            command,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
 