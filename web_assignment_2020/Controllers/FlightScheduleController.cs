using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_assignment_2020.DAL;
using web_assignment_2020.Models;

namespace web_assignment_2020.Controllers
{
    public class FlightScheduleController : Controller
    {
        private List<string> statusList = new List<string> { "Opened", "Full", "Delayed", "Cancelled" };
        private FlightScheduleDAL flightScheduleContext = new FlightScheduleDAL();
        private FlightRouteDAL flightRouteContext = new FlightRouteDAL();
        

        // GET: FlightSchedule
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                List<FlightSchedule> flightSchedules = flightScheduleContext.GetAllFlightSchedule();
                return View(flightSchedules);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: FlightSchedule/Details/5
        public ActionResult Details(int id)
        {
            ViewData["bookingList"] = flightScheduleContext.GetBooking(id);
            List<BookingViewModel> bookings = flightScheduleContext.GetBooking(id);
           
            return View(bookings);
        }

        // GET: FlightSchedule/Create
        public ActionResult Create()
        {
            ViewData["routeIDList"] = flightRouteContext.GetRouteIDs();
            ViewData["aircraftIDList"] = flightRouteContext.GetAircraftIDs();

            return View();
        }

        // POST: FlightSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightSchedule flightSchedule)
        {
            //Get country list for drop-down list
            //in case of the need to return to Create.cshtml view
            ViewData["routeIDList"] = flightRouteContext.GetRouteIDs();
            ViewData["aircraftIDList"] = flightRouteContext.GetAircraftIDs();
            if (ModelState.IsValid)
            {
                FlightRoute flightRoute = flightRouteContext.GetDetails(flightSchedule.RouteID);
                //Add staff record to database
                flightSchedule.ScheduleID = flightScheduleContext.Add(flightSchedule,flightRoute.FlightDuration);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(flightSchedule);
            }
        }
        

        // GET: FlightSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["statusList"] = statusList;
            FlightSchedule flightSchedule = flightScheduleContext.GetDetails(id.Value);
            return View(flightSchedule);
        }

        // POST: FlightSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string status)
        {
            ViewData["statusList"] = statusList;
            /*if (ModelState.IsValid)
            {
                //Update staff record to database
                flightScheduleContext.Update(flightSchedule.ScheduleID,flightSchedule.Status);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View("Index");
            }*/
            //Update staff record to database
            flightScheduleContext.Update(id.Value, status);
            return RedirectToAction("Index");
        }

        // GET: FlightSchedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightSchedule/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}