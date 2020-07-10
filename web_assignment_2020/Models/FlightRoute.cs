using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace web_assignment_2020.Models
{
    public class FlightRoute
    {
        public int RouteID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string DepartureCity { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string DepartureCountry { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string ArrivalCity { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string ArrivalCountry { get; set; }

        public int FlightDuration { get; set; }

    }
}
