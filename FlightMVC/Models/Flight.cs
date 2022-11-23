using System.ComponentModel.DataAnnotations;

namespace FlightMVC.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "{0} length should be less than or equal to {1}")]
        public string? FlightNo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length should be less than or equal to {1}")]
        public string? Origin { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length should be less than or equal to {1}")]
        public string? Destination { get; set; }
    }
}
