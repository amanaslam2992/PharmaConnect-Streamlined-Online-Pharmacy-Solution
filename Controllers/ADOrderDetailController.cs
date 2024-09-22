using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ADOrderDetailController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: ADOrderDetail
        public ActionResult Index()
        {
            var orderdetails = db.OrderDetails.ToList(); // Fetch product data

            ViewBag.OrderDetails = orderdetails;

            return View();
        }
    }
}