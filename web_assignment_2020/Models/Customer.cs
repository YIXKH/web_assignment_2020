using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
namespace web_assignment_2020.Models
{
    public class Customer
    {
        [Display(Name = "ID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        [StringLength(20, ErrorMessage = "Required, StringLength – cannot exceed 20 characters")]
        public string TelNo { get; set; }

               
        [Display(Name = "Email Address")]
        [EmailAddress] // Validation Annotation for email address format
        // Custom Validation Attribute for checking email address exists
        [ValidateEmailExists]
        [Required]
        [StringLength(50, ErrorMessage = "Required, StringLength – cannot exceed 50 characters")]
        public string EmailAddr { get; set; }


        [Required]
        [Display(Name = "Password")]
        [StringLength(225, ErrorMessage = "Required, StringLength – cannot exceed 225 characters")]
        public string Password { get; set; }

        public Customer()
        {
            Password = "p@55Cust";
        }
    }
}
