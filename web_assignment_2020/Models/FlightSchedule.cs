using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace web_assignment_2020.Models
{
    public class FlightSchedule
    {
        public int ScheduleID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]
        public string FlightNumber { get; set; }

        [Required]
        public int RouteID { get; set; }

        public int AircraftID { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime DepartureDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        public decimal EconomyClassPrice { get; set; }

        [Required]
        public decimal BusinessClassPrice { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]
        public string Status { get; set; }


        public FlightSchedule()
        {
            Status = "Opened";
        }
    }
}
