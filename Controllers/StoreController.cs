using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class StoreController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: Store
        public ActionResult Index()
        {
            var products = db.Products.ToList(); // Fetch product data

            ViewBag.Products = products;
            return View();
        }
    }
}