using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ADUserController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: ADUser
        public ActionResult Index()
        {
            var users = db.Users.ToList(); // Fetch product data

            ViewBag.Users = users;

            return View();
        }
    }
}