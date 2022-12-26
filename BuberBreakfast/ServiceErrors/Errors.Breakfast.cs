using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.Netfound",
            description: "Breakfast not found");

        public static Error InvalidName => Error.Validation(
           code: "Breakfast.InvalidName",
           description: $"Breakfast name must be at least {Models.Breakfast.MinNameLenght} characters long " +
            $"and at most {Models.Breakfast.MaxNameLenght} characters long."); 
        
        public static Error InvalidDescription => Error.Validation(
           code: "Breakfast.InvalidName",
           description: $"Breakfast description must be at least {Models.Breakfast.MinDescriptionLenght} characters long " +
            $"and at most {Models.Breakfast.MaxDescriptionLenght} characters long.");
    }

}
