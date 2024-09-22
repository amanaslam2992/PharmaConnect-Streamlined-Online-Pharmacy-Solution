using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class CartController : Controller
    {
        PakMedEntities db = new PakMedEntities();

        // GET: Cart
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                string loggedInUsername = Session["username"].ToString();
                var loggedInUser = db.Users.FirstOrDefault(u => u.User_Name == loggedInUsername);
                if (loggedInUser != null)
                {
                    int loggedInUserId = loggedInUser.User_Id;
                    var cartItems = db.Carts.Where(c => c.User_Id == loggedInUserId).ToList();
                    return View(cartItems);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCart(List<int> cartItemIds, List<int> quantities)
        {
            // Validate inputs (e.g., check if cartItemIds and quantities have the same length)
            for (int i = 0; i < cartItemIds.Count; i++)
            {
                int cartItemId = cartItemIds[i];
                int quantity = quantities[i];

                // Update the quantity of the cart item in the database
                var cartItem = db.Carts.Find(cartItemId);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                }
            }
            // Calculate and update total for the cart
            var cartItems = db.Carts.Where(c => cartItemIds.Contains(c.Cart_Id)).ToList();
            decimal subtotal = cartItems.Sum(c => (c.Quantity * c.Product.Price) ?? 0);
            foreach (var cartItem in cartItems)
            {
                // Updating Total for each cart item
                cartItem.Total = cartItem.Quantity * cartItem.Product.Price;
            }
            // Save changes to the database
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult RemoveCartItem(int cartItemId)
        {
            var cartItem = db.Carts.Find(cartItemId);
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PlaceOrder()
        {
            string loggedInUsername = Session["username"].ToString();
            var loggedInUser = db.Users.FirstOrDefault(u => u.User_Name == loggedInUsername);

            if (loggedInUser != null)
            {
                int loggedInUserId = loggedInUser.User_Id;
                var cartItems = db.Carts.Where(c => c.User_Id == loggedInUserId).ToList();

                // Calculate subtotal
                decimal subtotal = cartItems.Sum(c => c.Quantity * c.Product.Price)?? 0;

                // Calculate total price including Rs. 100
                decimal total = subtotal + 100;

                // Create a new Order
                var order = new Order
                {
                    User_Id = loggedInUserId,
                    Order_Date = DateTime.Now,
                    Total_Price = total
                };
                db.Orders.Add(order);
                db.SaveChanges();

                // Add OrderDetails for each cart item
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Order_Id = order.Order_Id,
                        Product_Id = cartItem.Product_Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price * cartItem.Quantity
                    };
                    db.OrderDetails.Add(orderDetail);
                }

                // Remove cart items after placing the order
                db.Carts.RemoveRange(cartItems);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Thank");
        }
    }
}