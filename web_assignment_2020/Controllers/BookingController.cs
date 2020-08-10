using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using web_assignment_2020.DAL;
using web_assignment_2020.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace web_assignment_2020.Controllers
{
    public class BookingController : Controller
    {
        private BookingDAL BookingContext = new BookingDAL();
        public ActionResult Create()
        {
            /*
            if (HttpContext.Session.GetString("Role") != "Customer")
            {
                return RedirectToAction("Index", "Home");
            }
            */
            ViewData["CountryList"] = GetCountries();
            Booking booking = new Booking();
            return View(booking);
        }
        [HttpPost]
        public ActionResult Create(Booking booking)
        {
            ViewData["CountryList"] = GetCountries();
            if (ModelState.IsValid)
            {
                booking.BookingID = BookingContext.Add(booking);
                return RedirectToAction("AdminMain", "Home");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(booking);
            }
        }
        private List<SelectListItem> GetCountries()
        {
            List<SelectListItem> countries = new List<SelectListItem>();
            countries.Add(new SelectListItem
            {
                Value = "Singapore",
                Text = "Singapore"
            });
            countries.Add(new SelectListItem
            {
                Value = "Malaysia",
                Text = "Malaysia"
            });
            countries.Add(new SelectListItem
            {
                Value = "Indonesia",
                Text = "Indonesia"
            });
            countries.Add(new SelectListItem
            {
                Value = "China",
                Text = "China"
            });
            return countries;
        }

        public IActionResult Index()
        {
             
            return View();
        }
    }
}
