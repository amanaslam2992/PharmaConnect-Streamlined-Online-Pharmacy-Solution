using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class SignUpController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string password, string city, string address, long number)
        {
            try
            {
                var newUser = new User
                {
                    User_Name = username,
                    User_Pass = password,
                    User_City = city,
                    User_Address = address,
                    User_Num = number,
                    Role_Id = 2 // new users are customers by default
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                // Redirect to login page after successful registration
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                // Handle exceptions or validation errors appropriately
                ModelState.AddModelError("", "Error occurred while registering: " + ex.Message);
                return View("Index");
            }
        }
    }

}