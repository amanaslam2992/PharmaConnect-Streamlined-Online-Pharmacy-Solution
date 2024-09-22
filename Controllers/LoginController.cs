using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
        PakMedEntities db = new PakMedEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var log = db.Users.Where(x => x.User_Name == username && x.User_Pass == password).FirstOrDefault();
            if (log != null)
            {
                Session["username"] = username;
                int roleId = (int)log.Role_Id;

                // Redirect the user based on their role
                if (roleId == 1)
                {
                    return RedirectToAction("index", "AdminDashboard"); // Redirect to Admin Dashboard
                }
                else if (roleId == 2)
                {
                    return RedirectToAction("index", "Home"); // Redirect to Customer Dashboard
                }
            }

            TempData["ErrorMessage"] = "Incorrect username or password.";
            return View("Login");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}