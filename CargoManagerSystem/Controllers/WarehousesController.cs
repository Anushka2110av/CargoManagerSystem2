using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CargoClass;
using CargoManagerSystem.Models;

namespace CargoManagerSystem.Controllers
{
    public class WarehousesController : Controller
    {
        // GET: Warehouses
        private CargoContext db = new CargoContext();

        // GET: Warehouses
        public async Task<ActionResult> Index()
        {
            var warehouses = db.Warehouses.Include(w => w.CargoOrder);
            return View(await warehouses.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = await db.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Warehouses/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CargoOrderId = new SelectList(db.CargoOrders, "Id", "OrderNumber");
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CargoOrderId,FromLocation,ToLocation,StoredWeight,StoredVolume,WarehouseLocation,StoredAt,MovedDate")] Warehouse warehouse)

        {
            if (ModelState.IsValid)
            {
                db.Warehouses.Add(warehouse);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CargoOrderId = new SelectList(db.CargoOrders, "Id", "OrderNumber", warehouse.CargoOrderId);
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = await db.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoOrderId = new SelectList(db.CargoOrders, "Id", "OrderNumber", warehouse.CargoOrderId);
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CargoOrderId,FromLocation,ToLocation,StoredWeight,StoredVolume,WarehouseLocation,StoredAt,MovedDate")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CargoOrderId = new SelectList(db.CargoOrders, "Id", "OrderNumber", warehouse.CargoOrderId);
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse warehouse = await db.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Warehouse warehouse = await db.Warehouses.FindAsync(id);
            db.Warehouses.Remove(warehouse);
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