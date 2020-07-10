using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace web_assignment_2020.Models
{
    public class FlightScheduleViewModel
    {
        public int ScheduleID { get; set; }

        public string FlightNumber { get; set; }

        public int RouteID { get; set; }

        public int AircraftID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DepartureDateTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ArrivalDateTime { get; set; }

        public decimal EconomyClassPrice { get; set; }

        public decimal BusinessClassPrice { get; set; }

        public string Status { get; set; }
    }
}
