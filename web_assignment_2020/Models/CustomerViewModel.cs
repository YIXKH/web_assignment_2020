using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;


namespace web_assignment_2020.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Telephone Number")]
        public string TelNo { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public string Password { get; set; }

        
    }
}
