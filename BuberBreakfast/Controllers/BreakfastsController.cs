using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;
public class BreakfastsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IMapper mapper, IBreakfastService breakfastService)
    {
        _mapper = mapper;
        _breakfastService = breakfastService;
    }
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var requestToBreakfastResult = _mapper.Map<ErrorOr<Breakfast>>(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<Created> createdBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createdBreakfastResult.Match(
            created => CreatedAsGetBreakfast(breakfast),
            errors => Problem(errors));
    }

    private IActionResult CreatedAsGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: _mapper.Map<BreakfastResponse>(breakfast));
    }

    [HttpGet("/breakfasts/{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(_mapper.Map<BreakfastResponse>(breakfast)),
            errors => Problem(errors)
            );



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        //if (getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.Breakfast.NotFound)
        //{
        //    return NotFound();
        //}

        //var breakfast = getBreakfastResult.Value;

        //var breakfastResponse = _mapper.Map<BreakfastResponse>(breakfast);

        //return Ok(breakfast);
    }

    [HttpPut("/breakfasts/{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        //_mapper.Config.ForType<UpsertBreakfastRequest, Breakfast>()
        //.Map(dest => dest.Id, _ => id)
        //.Map(dest => dest.LastModifiedDateTime, s => DateTime.UtcNow);

      
        var requestToBreafastResult = _mapper.Map<ErrorOr<Breakfast>>((id,request));

        if (requestToBreafastResult.IsError)
        {
            Problem(requestToBreafastResult.Errors);
        }

        var breakfast = requestToBreafastResult.Value;

        ErrorOr<UpsertedBreakfastResult> upsertedBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

        //todo: return 201 if  breakfast was created

        return upsertedBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAsGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
            );

    }
    [HttpDelete("/breakfasts/{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {

        ErrorOr<Deleted> deletedResult = _breakfastService.DeleteBreakfast(id);

        return deletedResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
            );

        return NoContent();
    }
}
