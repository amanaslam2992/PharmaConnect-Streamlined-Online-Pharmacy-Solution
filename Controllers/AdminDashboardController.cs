using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class AdminDashboardController : Controller
    {
        PakMedEntities db = new PakMedEntities();
        public ActionResult Index()
        {
            int totalProducts = db.Products.Count();
            ViewBag.TotalProducts = totalProducts;

            int totalUsers = db.Users.Count();
            ViewBag.TotalUsers = totalUsers;

            int totalOrders = db.Orders.Count();
            ViewBag.TotalOrders = totalOrders;

            int totalRoles = db.Roles.Count();
            ViewBag.TotalRoles = totalRoles;

            var products = db.Products.ToList(); // Fetch product data
            var users = db.Users.ToList(); // Fetch user data
            var orders = db.Orders.ToList(); // Fetch order data
            var orderdetails = db.OrderDetails.ToList(); // Fetch order data

            ViewBag.Products = products;
            ViewBag.Users = users;
            ViewBag.Orders = orders;
            ViewBag.OrderDetails = orderdetails;

            return View();
        }

        //Create Product
        public ActionResult CreateProduct() { return View(); }
        [HttpPost]
        public ActionResult CreateProduct(HttpPostedFileBase ImageFile, Product em)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);
            string _fileName = fileName;
            string extension = Path.GetExtension(ImageFile.FileName);
            string path = Path.Combine(Server.MapPath("~/uploads/"), fileName);
            em.Product_Img = "~/uploads/" + _fileName;
            db.Products.Add(em);
            if (db.SaveChanges() > 0)
            {
                ImageFile.SaveAs(path);
                return RedirectToAction("Index");
            }
            return View();
        }
        //Edit Product
        public ActionResult EditProduct(int? id)
        {
            if (id == null || db.Products == null)
            {
                return HttpNotFound();
            }
            var em = db.Products.Find(id);
            if (em == null)
            {
                return HttpNotFound();
            }
            return View(em);
        }

        [HttpPost]
        public ActionResult EditProduct(HttpPostedFileBase ImageFile, Product em)
        {
            if (ModelState != null)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                string _fileName = fileName;
                string extension = Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/uploads/"), fileName);
                em.Product_Img = "~/uploads/" + _fileName;
                db.Products.AddOrUpdate(em);
                if (db.SaveChanges() > 0)
                {
                    ImageFile.SaveAs(path);
                    return RedirectToAction("Index");
                }
                return View();
            }
            return View();
        }
        //Delete Product
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null || db.Products == null)
            {
                return HttpNotFound();
            }
            var em = db.Products.Find(id);
            if (em == null)
            {
                return HttpNotFound();
            }
            return View(em);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState != null)
            {
                var em = db.Products.Find(id);
                db.Products.Remove(em);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //Details Product
        public ActionResult DetailsProduct(int? id)
        {
            var em = db.Products.Find(id);
            return View(em);
        }

        //Delete User
        public ActionResult DeleteUser(int? id)
        {
            if (id == null || db.Users == null)
            {
                return HttpNotFound();
            }
            var em = db.Users.Find(id);
            if (em == null)
            {
                return HttpNotFound();
            }
            return View(em);
        }
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            if (ModelState != null)
            {
                var em = db.Users.Find(id);
                db.Users.Remove(em);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //Details User
        public ActionResult DetailsUser(int? id)
        {
            var em = db.Users.Find(id);
            return View(em);
        }
        //Delete Order
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null || db.Orders == null)
            {
                return HttpNotFound();
            }
            var em = db.Orders.Find(id);
            if (em == null)
            {
                return HttpNotFound();
            }
            return View(em);
        }
        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            if (ModelState != null)
            {
                var em = db.Orders.Find(id);
                foreach (var detail in em.OrderDetails.ToList())
                {
                    db.OrderDetails.Remove(detail);
                }
                db.Orders.Remove(em);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}