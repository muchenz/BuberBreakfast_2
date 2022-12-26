using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using ErrorOr;
using Mapster;

namespace BuberBreakfast.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<CreateBreakfastRequest, Breakfast>()
        //    .Map(dest => dest.Id, _=> Guid.NewGuid()).Map(dest => dest.LastModifiedDateTime, _ => DateTime.UtcNow);

        config.NewConfig<CreateBreakfastRequest, ErrorOr<Breakfast>>().
            ConstructUsing(src => Breakfast.Create(
                Guid.NewGuid(),src.Name,src.Description,src.StartDateTime, src.EndDateTime, src.Savory, src.Sweet) );

        config.NewConfig<(Guid id, UpsertBreakfastRequest upsert), ErrorOr<Breakfast>>().
           ConstructUsing(src => Breakfast.Create(
              src.id, src.upsert.Name, src.upsert.Description, src.upsert.StartDateTime, src.upsert.EndDateTime, src.upsert.Savory, src.upsert.Sweet));


        config.NewConfig<Breakfast, CreateBreakfastRequest>();

    }
}
