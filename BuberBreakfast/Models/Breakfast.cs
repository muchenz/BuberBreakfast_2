using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{


    public const int MinNameLenght = 3;
    public const int MaxNameLenght = 23;

    public const int MinDescriptionLenght = 3;
    public const int MaxDescriptionLenght = 23;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }


    private Breakfast(Guid id,
    string name,
    string description,
    DateTime startDateTime,
    DateTime endDateTime,
    DateTime lastModifiedDateTime,
    List<string> savory,
    List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(
    Guid id,
    string name,
    string description,
    DateTime startDateTime,
    DateTime endDateTime,
    List<string> savory,
    List<string> sweet)
    {
        //enfirce validation

        List<Error> errors = new();

        if (name.Length is < MinNameLenght or > MaxNameLenght)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }

        if (name.Length is < MinDescriptionLenght or > MaxDescriptionLenght)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Breakfast(id, name, description, startDateTime, endDateTime, DateTime.UtcNow, savory, sweet);
    }
}
