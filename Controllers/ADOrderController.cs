using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ADOrderController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: ADOrder
        public ActionResult Index()
        {
            var orders = db.Orders.ToList(); // Fetch product data

            ViewBag.Orders = orders;

            return View();
        }
    }
}