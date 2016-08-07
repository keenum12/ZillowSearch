using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZillowSearch.Models
{
    public class HomeViewModel
    {
        [Required]
        [Display(Name = "Street Address")]
        public string Address { get; set; }
        
        [Required]
        [Display(Name = "City, State or Zip Code")]
        public string CityAndStateOrZipCode { get; set; }

        [Required]
        [Display(Name = "Include Rent Estimate")]
        public bool IncludeRentEstimate { get; set; }
    }
}