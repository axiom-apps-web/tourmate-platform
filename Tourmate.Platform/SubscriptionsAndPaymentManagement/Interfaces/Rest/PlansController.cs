
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
/// REST controller for managing subscription plans.
/// </summary>
[ApiController]
[Route("api/v1/plans")]
[Produces("application/json")]
public class PlansController(
    IPlanCommandService commandService,
    IPlanQueryService queryService) : ControllerBase
{
    /// <summary>
    /// Retrieves all subscription plans.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanResource>>> GetAll(
        CancellationToken cancellationToken)
    {
        var plans = await queryService.Handle(
            new GetAllPlansQuery(), cancellationToken);

        var resources = plans.Select(
            PlanResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a plan by its identifier.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlanResource>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var plan = await queryService.Handle(
            new GetPlanByIdQuery(new PlanId(id)),
            cancellationToken);

        if (plan is null)
            return NotFound();

        return Ok(
            PlanResourceFromEntityAssembler.ToResourceFromEntity(plan));
    }

    /// <summary>
    /// Creates a new subscription plan.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PlanResource>> Create(
        [FromBody] CreatePlanResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                CreatePlanCommandFromResourceAssembler
                    .ToCommandFromResource(resource);

            var plan = await commandService.Handle(
                command, cancellationToken);

            var planResource =
                PlanResourceFromEntityAssembler
                    .ToResourceFromEntity(plan);

            return CreatedAtAction(
                nameof(GetById),
                new { id = plan.Id.Value },
                planResource);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing subscription plan.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PlanResource>> Update(
        int id,
        [FromBody] UpdatePlanResource resource,
        CancellationToken cancellationToken)
    {
        try
        {
            var command =
                UpdatePlanCommandFromResourceAssembler
                    .ToCommandFromResource(id, resource);

            var plan = await commandService.Handle(
                command, cancellationToken);

            if (plan is null)
                return NotFound();

            return Ok(
                PlanResourceFromEntityAssembler
                    .ToResourceFromEntity(plan));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an existing subscription plan.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeletePlanCommand(new PlanId(id));

            var deleted = await commandService.Handle(
                command, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
 