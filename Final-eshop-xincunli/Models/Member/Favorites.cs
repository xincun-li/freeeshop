using System.ComponentModel.DataAnnotations;

namespace Final_eshop_xincunli.Models
{
    public enum Favorites
    {
        [Display(Name = "Movie")]
        Movie,
        [Display(Name = "Music")]
        Music,
        [Display(Name = "Car")]
        Car,
        [Display(Name = "Sport")]
        Sports,
        [Display(Name = "Entertainment")]
        Entertainment,
        [Display(Name = "Other")]
        Other
    }
}