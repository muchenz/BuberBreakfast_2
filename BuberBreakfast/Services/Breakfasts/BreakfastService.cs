using BuberBreakfast.Models;
using BuberBreakfast.Persistence;
using BuberBreakfast.ServiceErrors;
using ErrorOr;
using MapsterMapper;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    readonly BubberBreakfastDbContext _dbContext = null!;
    private readonly IMapper _mapper;

    public BreakfastService(BubberBreakfastDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public ErrorOr<Created> CreateBreakfastAsync(Breakfast breakfast)
    {

        _dbContext.Add(breakfast);
        _dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = _dbContext.Breakfasts.Find(id);
        if (breakfast is null)
        {
            return Errors.Breakfast.NotFound;
        }

        _dbContext.Remove(breakfast);

        _dbContext.SaveChangesAsync();

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_dbContext.Breakfasts.Find(id) is Breakfast breakfast)
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_dbContext.Breakfasts.Any(a=>a.Id ==breakfast.Id);
        

        if (isNewlyCreated)
        {
            _dbContext.Breakfasts.Add(breakfast);
        }else
        {
            _dbContext.Breakfasts.Update(breakfast);
        }
        _dbContext.SaveChanges();

        return new UpsertedBreakfastResult(isNewlyCreated);
    }



    //readonly Dictionary<Guid, Breakfast> _breakpoints = new();
    //public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    //{
    //    _breakpoints.Add(breakfast.Id, breakfast);

    //    return Result.Created;
    //}

    //public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    //{
    //    _breakpoints.Remove(id);

    //    return Result.Deleted;
    //}

    //public ErrorOr<Breakfast> GetBreakfast(Guid id)
    //{
    //    if(_breakpoints.TryGetValue(id, out Breakfast breakfast))
    //    {
    //        return breakfast;
    //    }

    //    return Errors.Breakfast.NotFound;
    //}

    //public ErrorOr<UpsertedBreakfastResult> UpsertBreakfast(Breakfast breakfast)
    //{
    //    var isNewlyCreated = !_breakpoints.ContainsKey(breakfast.Id);

    //    _breakpoints[breakfast.Id]=breakfast;

    //    return new UpsertedBreakfastResult(isNewlyCreated);
    //}
}
