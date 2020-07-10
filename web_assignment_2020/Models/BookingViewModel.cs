using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace web_assignment_2020.Models
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }

        public int CustomerID { get; set; }

        public int ScheduleID { get; set; }

        public string PassengerName { get; set; }

        public string PassportNumber { get; set; }

        public string Nationality { get; set; }

        public string SeatClass { get; set; }

        public decimal AmtPayable { get; set; }

        public string Remarks { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreated { get; set; }
    }
}
