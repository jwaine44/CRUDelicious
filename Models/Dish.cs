#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;

public class Dish
{
    [Key]

    [Required]
    public int DishId {get;set;}

    [Required]
    [Display(Name = "Name of Dish")]
    public string Name {get;set;}

    [Required]
    [Display(Name = "Chef's Name")]
    public string Chef {get;set;}

    [Required]
    [Range(1, 5, ErrorMessage = "Tastiness can only be between 1 and 5!")]
    [Display(Name = "Tastiness")]
    public int Tastiness {get;set;}

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0!")]
    [Display(Name = "# of Calories")]
    public int Calories {get;set;}

    [Required]
    [Display(Name = "Description")]
    public string Description {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;

    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}