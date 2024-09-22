using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ADProductController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: ADProduct
        public ActionResult Index()
        {
            var products = db.Products.ToList(); // Fetch product data

            ViewBag.Products = products;
            
            return View();
        }
    }
}