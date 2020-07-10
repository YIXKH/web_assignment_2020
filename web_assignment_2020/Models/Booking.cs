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
        public int BookingID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ScheduleID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]

        public string PassengerName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]

        public string PassportNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]

        public string Nationality { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]

        public string SeatClass { get; set; }

        [Required]
        public decimal AmtPayable { get; set; }

        [StringLength(3000, ErrorMessage = "Required, StringLength – cannot exceed 3000 characters")]

        public string Remarks { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeCreated { get; set; }

        public Booking()
        {
            SeatClass = "Economy";
        }

    }
}
