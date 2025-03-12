using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class CargoOrder
    {

        public int Id { get; set; } // Primary Key

        public string CustomerName { get; set; }
        public string PickupLocation { get; set; } // Add PickupLocation
        public string DropLocation { get; set; }
        // Add DropLocation

        [Required(ErrorMessage = "Cargo Type is required.")]
        public string CargoType { get; set; }

        public double Weight { get; set; }

        public decimal Price { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string OrderNumber { get; set; }

        public string Status { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }


    }
}
