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
using System.Dynamic;
using System.Security;

namespace CargoManagerSystem.Controllers
{
    public class AdministratorsController : Controller
    {
        private CargoContext db = new CargoContext();


        public async Task<ActionResult> Index()
        {
            dynamic obj = new ExpandoObject();
            obj.EmployeeList = db.Employees.ToList();
            obj.CargoTypeUpdate=db.CargoTypes.ToList();

            return View(obj);
        }
        public async Task<ActionResult> EmployeeList()
        {

            return View(await db.Employees.ToListAsync());

        }

        [HttpGet]

        public ActionResult CreateEmployee() => View();

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> CreateEmployee(Employee employee,string Password)
        {
            EmployeeDetails details = new EmployeeDetails();

            if (ModelState.IsValid)

            {
                employee.Password = Password;
                details.CreateRolesAndAdminUser(employee.Email, employee.Password , employee.Role);
                db.Employees.Add(employee);

                await db.SaveChangesAsync();

                return RedirectToAction("EmployeeList");

            }

            return View(employee);

        }

        [HttpGet]

        public ActionResult UpdateEmployee(int id)

        {

            var emp = db.Employees.Find(id);

            if (emp == null) return HttpNotFound();

            return View(emp);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> UpdateEmployee(Employee emp)

        {

            if (ModelState.IsValid)

            {

                db.Entry(emp).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return RedirectToAction("EmployeeList");

            }

            return View(emp);

        }

        // 🔹 Search Employee
        [HttpGet]
        public ActionResult SearchEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchEmployee(string searchQuery)
        {
            var employees = await db.Employees.Where(e => e.Name.Contains(searchQuery) || e.Email.Contains(searchQuery)).ToListAsync();
            return View("EmployeeList", employees);
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {

            Employee employee = await db.Employees.FindAsync(id);

            if (employee == null) return HttpNotFound();

            db.Employees.Remove(employee);

            await db.SaveChangesAsync();

            return RedirectToAction("EmployeeList");

        }
        //customer management
        public async Task<ActionResult> CustomerList()
        {

            return View(await db.Customers.ToListAsync());

        }

        //[HttpGet]

        public ActionResult SearchCustomer()
        {
            return View();

        }

        [HttpPost]

        public async Task<ActionResult> SearchCustomer(string searchQuery)

        {

            var customers = await db.Customers.Where(c => c.Name.Contains(searchQuery) || c.Email.Contains(searchQuery)).ToListAsync();

            return View("CustomerList", customers);

        }
        //



        // 🔹 Cargo Type Management

        // ===========================

        public async Task<ActionResult> CargoTypeList()

        {

            return View(await db.CargoTypes.ToListAsync());

        }

        [HttpGet]

        public ActionResult AddCargoType() => View();

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> AddCargoType(CargoType cargoType)
        {

            if (ModelState.IsValid)

            {

                db.CargoTypes.Add(cargoType);

                await db.SaveChangesAsync();

                return RedirectToAction("CargoTypeList");

            }

            return View(cargoType);

        }

        [HttpGet]

        public async Task<ActionResult> UpdateCargoType(int id)

        {

            var cargoType = await db.CargoTypes.FindAsync(id);

            if (cargoType == null) return HttpNotFound();

            return View(cargoType);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> UpdateCargoType(CargoType cargoType)

        {

            if (ModelState.IsValid)

            {

                db.Entry(cargoType).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return RedirectToAction("CargoTypeList");

            }

            return View(cargoType);

        }
        

// City Management

public async Task<ActionResult> CityList()
        {

            return View(await db.Cities.ToListAsync());

        }

        [HttpGet]

        public ActionResult AddCity() => View();

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> AddCity(City city)
        {

            if (ModelState.IsValid)

            {

                db.Cities.Add(city);

                 db.SaveChanges();

                return RedirectToAction("CityList");

            }

            return View(city);

        }

        [HttpGet]
        public async Task<ActionResult> UpdateCity(int id)
        {
            var city = await db.Cities.FindAsync(id);
            if (city == null) return HttpNotFound();
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCity(City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CityList");
            }
            return View(city);
        }


        public async Task<ActionResult> CargoList()

        {

            return View(await db.CargoOrders.ToListAsync());

        }

        [HttpGet]

        public ActionResult BookCargo()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<ActionResult> BookCargo(CargoOrder order)

        {

            if (ModelState.IsValid)

            {

                db.CargoOrders.Add(order);

                await db.SaveChangesAsync();

                return RedirectToAction("CargoList");

            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");


            return View(order);

        }

        public async Task<ActionResult> CalculateCargoPrice(int cargoId)
        {
            var cargo = await db.CargoOrders.FindAsync(cargoId);
            if (cargo == null) return HttpNotFound();

            var city = await db.Cities.FirstOrDefaultAsync(c => c.Name == cargo.DropLocation);
            if (city == null) return HttpNotFound();

            cargo.Price = (decimal)(cargo.Weight * city.PricePerKm);
            db.Entry(cargo).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return View("CalculateCargoPrice", cargo);
        }

        // GET action to show confirmation page
        [HttpGet]
        public async Task<ActionResult> CancelCargo(int cargoId)
        {
            var cargo = await db.CargoOrders.FindAsync(cargoId);
            if (cargo == null)
            {
                return HttpNotFound();
            }

            // Return the cargo details to confirm cancellation
            return View(cargo);
        }

        // POST action to perform the cancellation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelCargoConfirmed(int cargoId)
        {
            var cargo = await db.CargoOrders.FindAsync(cargoId);
            if (cargo == null)
            {
                return HttpNotFound();
            }

            // Update the status to "Cancelled"
            cargo.Status = "Cancelled";

            // Mark the entry as modified and save changes
            db.Entry(cargo).State = EntityState.Modified;
            await db.SaveChangesAsync();

            // Redirect to the Cargo List or any other relevant page
            return RedirectToAction("CargoList");
        }

    }
}
