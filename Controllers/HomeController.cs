using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: Home
        public ActionResult Index()
        {
            var products = db.Products.ToList(); // Fetch product data

            ViewBag.Products = products;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}