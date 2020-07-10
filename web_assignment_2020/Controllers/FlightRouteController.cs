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
    public class FlightRouteController : Controller
    {
        private FlightRouteDAL flightRouteContext = new FlightRouteDAL();


        // GET: FlightRoute
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                List<FlightRoute> flightRoutes = flightRouteContext.GetAllFlightRoutes();
                return View(flightRoutes);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: FlightRoute/Details/5
        public ActionResult Details(int id)
        {
            ViewData["routeID"] = id;
            List<FlightScheduleViewModel> flightSchedules = flightRouteContext.GetFlightSchedules(id);
            return View(flightSchedules);
        }

        // GET: FlightRoute/Create
        public ActionResult Create()
        {
            ViewData["minDate"] = DateTime.Today.AddDays(1).ToString();
            return View();
        }

        // POST: FlightRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightRoute flightRoute)
        {
            //Get country list for drop-down list
            //in case of the need to return to Create.cshtml view
            
            if (ModelState.IsValid)
            {
                //Add staff record to database
                flightRoute.RouteID = flightRouteContext.Add(flightRoute);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(flightRoute);
            }
        }

        // GET: FlightRoute/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightRoute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}