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
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Schedule ID")]
        public int ScheduleID { get; set; }

        [Display(Name = "Passenger Name")]
        public string PassengerName { get; set; }

        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Seat Class")]
        public string SeatClass { get; set; }

        [Display(Name = "Amount Payable")]
        public decimal AmtPayable { get; set; }

        public string Remarks { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "DateTime Created")]
        public DateTime DateTimeCreated { get; set; }
    }
}
