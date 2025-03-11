using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CargoClass;
using CargoManagerSystem.Models;
using Microsoft.AspNet.Identity;

namespace CargoManagerSystem.Controllers
{
    public class CustomersController : Controller
    {
        private CargoContext db = new CargoContext();

        // GET: Customers
        public ActionResult Index()
        {
            //int customerId = User.Identity.GetUserId<int>(); // Get logged-in Customer ID
            int Id = -1;
            var pastOrders = db.CargoOrders.Where(o => o.Id == Id).ToList();
            return View(pastOrders);
        }

        public ActionResult CreateOrder()

        {

            ViewBag.Cities = db.Cities.ToList();

            ViewBag.CargoTypes = new List<string> { "Electronics", "Furniture", "Food", "Machinery" };

            return View();

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> CreateOrder(CargoOrder order)

        {

            if (ModelState.IsValid)

            {

                // Generate a random Order ID

                Random random = new Random();

                order.OrderNumber = "ORD" + random.Next(10000, 99999);

                // Set timestamps

                order.CreatedAt = DateTime.Now;

                order.UpdatedAt = DateTime.Now;

                // Auto-calculate Estimated Delivery Date (Pickup Date + 10 days)

                order.EstimatedDeliveryDate = order.PickupDate.AddDays(10);

                // Save order to the database

                db.CargoOrders.Add(order);

                await db.SaveChangesAsync();

                // Send order details to the Invoice view

                int savedOrderId = order.Id;

                return RedirectToAction("Invoice", new { id = order.Id });
                

            }

            

            // If ModelState is invalid, reload dropdowns and return to form

            ViewBag.Cities = db.Cities.ToList();

            ViewBag.CargoTypes = new List<string> { "Electronics", "Furniture", "Food", "Machinery" };

            return View(order);

        }

        //invoice method

        public ActionResult Invoice(int id)
        {
            var order = db.CargoOrders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Phone,Address,CreatedAt,UpdatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Phone,Address,CreatedAt,UpdatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}