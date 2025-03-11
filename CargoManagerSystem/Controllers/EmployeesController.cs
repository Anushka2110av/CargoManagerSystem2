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

namespace CargoManagerSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private CargoContext db = new CargoContext();


        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult Index()
        {
            var employees = db.Employees.ToList(); // Get all employees from the database
            return View(employees);
        }
        public ActionResult CargoTypeIndex()
        {
            var cargo = db.CargoFares.ToList(); // Get all employees from the database
            return View(cargo);
        }
        public ActionResult CityIndex()
        {
            var city = db.Cities.ToList(); // Get all employees from the database
            return View(city);
        }
        public ActionResult CustomerIndex()
        {
            var Customer = db.Customers.ToList();
            return View(Customer);
        }
      
        public ActionResult EditSelf(int id)
        {
            // Retrieve the employee by ID
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound(); // If the employee is not found, return 404
            }

            
            return View(employee);
        }

        // POST: Employees/EditSelf/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSelf(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Find the existing employee from the database
                var existingEmployee = db.Employees.Find(employee.EmployeeId);
                if (existingEmployee == null)
                {
                    return HttpNotFound(); // If the employee is not found, return 404
                }

                // Update the employee's details
                existingEmployee.Name = employee.Name;
                existingEmployee.Role = employee.Role;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Address = employee.Address;

                // Mark the entity as modified and save the changes
                db.Entry(existingEmployee).State = EntityState.Modified;
                db.SaveChanges();

                
                return RedirectToAction("Index");
            }

          
            return View(employee);
        }

           
          


        // Search for Customer
        public ActionResult SearchCustomer(string searchTerm)
        {
          
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(new List<Customer>());
            }

          
            var customers = db.Customers
                               .Where(c => c.Name.Contains(searchTerm) || c.Email.Contains(searchTerm))
                               .ToList();

            return View(customers);
        }


        // Search for Cargo Fare
        public ActionResult SearchCargoFare(string searchTerm)
        {
            // Search for cargo fares by cargo type
            var cargoFares = db.CargoFares
                               .Where(c => c.CargoType.Contains(searchTerm))
                               .ToList();

            return View(cargoFares);
        }

        // Search for Orders
        public ActionResult SearchOrders(string searchTerm)
        {
            // Search for orders by order number or customer name
            var orders = db.CargoOrders.Include(o => o.Customer)
                                  .Where(o => o.OrderNumber.Contains(searchTerm) || o.Customer.Name.Contains(searchTerm))
                                  .ToList();

            return View(orders);
        }


        // Update Cargo Type
        public ActionResult UpdateCargoType(int id)
        {
            var cargoFare = db.CargoFares.Find(id);
            if (cargoFare == null)
            {
                return HttpNotFound();
            }
            return View(cargoFare);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCargoType(int id, [Bind(Include = "Id, CargoType, Fare")] CargoFare cargoFare)
        {

            var existingCargoFare = db.CargoFares.Find(id);
                if (existingCargoFare == null)
                {
                    return HttpNotFound();  // Record does not exist
                }

  

                // Proceed with the update
                existingCargoFare.CargoType = cargoFare.CargoType;
                existingCargoFare.Fare = cargoFare.Fare;

                db.SaveChanges();
                return RedirectToAction("CargoTypeIndex");
            }

       



        public ActionResult UpdatePlace(int id)
        {
            // Retrieve the place with the given ID
            var place = db.Cities.Find(id);

            if (place == null)
            {
                return HttpNotFound(); // Return HTTP 404 if the place doesn't exist
            }

            // Pass the place object to the view for editing
            return View(place);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePlace(int id, [Bind(Include = "Id, Name, Description")] City place)
        {
           
                var existingPlace = db.Cities.Find(id);

                if (existingPlace == null)
                {
                    return HttpNotFound(); // If the place doesn't exist, return HTTP 404
                }

                // Update the existing place's properties with the new values
                existingPlace.Name = place.Name;
               // existingPlace.Address = place.Address;
                existingPlace.Description = place.Description;

               

                // Save the changes to the database
                db.SaveChanges();

                // Redirect to the index or a confirmation page
                return RedirectToAction("CityIndex");
            }

            


        public ActionResult UpdateCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer([Bind(Include = "Id, Name, Email, Phone, Address,CreatedAt,UpdatedAt")] Customer customer)
        {

            var existingCustomer = db.Customers.Find(customer.Id);

            // Update the entity's properties
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;
            existingCustomer.CreatedAt = customer.CreatedAt;
            existingCustomer.UpdatedAt = customer.UpdatedAt;


            // Mark the entity as modified
            db.Entry(existingCustomer).State = EntityState.Modified;

            // Save changes to the database
            db.SaveChanges();
            return RedirectToAction("CustomerIndex");

            //return View(customer);
        }






































        // GET: Employees
        //        public async Task<ActionResult> Index()
        //        {
        //            return View(await db.Employees.ToListAsync());
        //        }

        //        // GET: Employees/Details/5
        //        public async Task<ActionResult> Details(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Employee employee = await db.Employees.FindAsync(id);
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(employee);
        //        }

        //        // GET: Employees/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Employees/Create
        //        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<ActionResult> Create([Bind(Include = "EmployeeId,Name,Role,Email,PhoneNumber,Address")] Employee employee)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Employees.Add(employee);
        //                await db.SaveChangesAsync();
        //                return RedirectToAction("Index");
        //            }

        //            return View(employee);
        //        }

        //        // GET: Employees/Edit/5
        //        public async Task<ActionResult> Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Employee employee = await db.Employees.FindAsync(id);
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(employee);
        //        }

        //        // POST: Employees/Edit/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<ActionResult> Edit([Bind(Include = "EmployeeId,Name,Role,Email,PhoneNumber,Address")] Employee employee)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(employee).State = EntityState.Modified;
        //                await db.SaveChangesAsync();
        //                return RedirectToAction("Index");
        //            }
        //            return View(employee);
        //        }

        //        // GET: Employees/Delete/5
        //        public async Task<ActionResult> Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Employee employee = await db.Employees.FindAsync(id);
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(employee);
        //        }

        //        // POST: Employees/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<ActionResult> DeleteConfirmed(int id)
        //        {
        //            Employee employee = await db.Employees.FindAsync(id);
        //            db.Employees.Remove(employee);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                db.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}
