using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace web_assignment_2020.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();
            if (loginID == "abc@npbook.com" && password == "pass1234")
            {
                // Store Login ID in session with the key “LoginID”
                HttpContext.Session.SetString("LoginID", loginID);
                // Store user role “Staff” as a string in session with the key “Role”
                HttpContext.Session.SetString("Role", "Admin");
                //Store Datetime for login
                HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString("dd-MMM-yy h:mm:ss tt"));
                // Redirect user to the "AdminMain" view through an action
                return RedirectToAction("AdminMain");
            }
            else
            {
                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }
        public ActionResult AdminMain()
        {
            return View();
        }

        
        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }


    }
}