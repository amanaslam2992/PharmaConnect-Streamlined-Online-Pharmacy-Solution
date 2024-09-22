using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ProductController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: Product
        public ActionResult Index(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Product_Id == id);

            if (product == null)
            {
                return HttpNotFound(); // Or handle the case where the product is not found
            }

            return View(product);
        }

        public ActionResult AddToCart(int productId, int quantity)
        {
            if (Session["username"] != null)
            {
                string loggedInUsername = Session["username"].ToString();
                var loggedInUser = db.Users.FirstOrDefault(u => u.User_Name == loggedInUsername);

                if (loggedInUser != null)
                {
                    int loggedInUserId = loggedInUser.User_Id;

                    var existingCartItem = db.Carts.FirstOrDefault(c => c.User_Id == loggedInUserId && c.Product_Id == productId);

                    if (existingCartItem != null)
                    {
                        existingCartItem.Quantity += quantity;
                    }
                    else
                    {
                        Cart cartItem = new Cart
                        {
                            User_Id = loggedInUserId,
                            Product_Id = productId,
                            Quantity = quantity
                        };
                        db.Carts.Add(cartItem);
                    }
                    db.SaveChanges();
                    return RedirectToAction("index", "Cart");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return HttpNotFound();
        }
    }
}