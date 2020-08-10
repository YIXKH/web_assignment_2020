using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace web_assignment_2020.Models
{
    public class Booking
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Schedule ID")]
        public int ScheduleID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        [Display(Name = "Passenger Name")]
        public string PassengerName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string Nationality { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]
        [Display(Name = "Seat Class")]
        public string SeatClass { get; set; }

        [Required]
        [Display(Name = "Amount Payable")]
        public decimal AmtPayable { get; set; }

        [StringLength(3000, ErrorMessage = "Required, StringLength – cannot exceed 3000 characters")]

        public string Remarks { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "DateTime Created")]
        public DateTime DateTimeCreated { get; set; }

        public Booking()
        {
            SeatClass = "Economy";
            DateTimeCreated = DateTime.Now;
        }


    }
}
