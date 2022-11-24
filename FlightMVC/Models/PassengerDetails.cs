using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FlightMVC.Models
{
   //public record PassengerDetails(string Name, int Weight) { }

    public record PassengerDetails
    {
        [Required]
        [StringLength(12, ErrorMessage = "{0} length should be less than or equal to {1}")]
        public string Name { get; init; } = default!;

        [Required]
        [Range(0,30, ErrorMessage ="{0} should be between {1} and {2}")]
        public int Weight { get; init; } = default!;

        [Required]
        [StringLength(6, ErrorMessage = "{0} length should be less than or equal to {1}")]
        public string FlightNo { get; init; }

        public PassengerDetails() {
            this.Name = string.Empty;
            this.FlightNo = string.Empty;
        }   

        public PassengerDetails(string Name, int Weight, string FlightNo)
        {
            this.Name = Name;
            this.Weight = Weight;
            this.FlightNo = FlightNo;
        }
    }
}
