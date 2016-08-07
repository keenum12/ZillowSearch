using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZillowSearch.Models
{
    public class HomeViewModel
    {
        [Required]
        [Display(Name = "Street Address")]
        [RegularExpression("[0-9]+\\s+[0-9a-zA-Z .]+", ErrorMessage = "Please provide a valid street address.")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City, State or Zip Code")]
        [RegularExpression("(?:\\d{5}(?:[-\\s]\\d{4})?)|(?:[a-zA-Z ]+[, ]\\s*[a-zA-Z ]{2,})*", ErrorMessage = "Please provide a valid city & state, or zip code.")]
        public string CityAndStateOrZipCode { get; set; }

        [Required]
        [Display(Name = "Include Rent Estimate")]
        public bool IncludeRentEstimate { get; set; }
    }
}