using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    readonly Dictionary<Guid, Breakfast> _breakpoints = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakpoints.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakpoints.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakpoints.TryGetValue(id, out Breakfast breakfast))
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_breakpoints.ContainsKey(breakfast.Id);

        _breakpoints[breakfast.Id]=breakfast;

        return new UpsertedBreakfastResult(isNewlyCreated);
    }
}
