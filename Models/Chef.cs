#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsNDishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [AgeValidation]
    public DateTime DateOfBirth { get; set; }

    public List<Dish> AllDishes { get; set; } = new List<Dish>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class AgeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        DateTime varTime = DateTime.Now;

        if (value == null)
        {
            return new ValidationResult("Please enter a date of birth.");
        }

        // Get the DateOfBirth property of the Chef model from the validation context
        DateTime dateOfBirth = (DateTime)validationContext.ObjectType.GetProperty("DateOfBirth").GetValue(validationContext.ObjectInstance, null);

        if (dateOfBirth > varTime)
        {
            return new ValidationResult("Please enter a valid date of birth.");
        } 
        else 
        {
            return ValidationResult.Success;
        }
    }
}