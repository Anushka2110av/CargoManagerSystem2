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

        [Required(ErrorMessage = "Cargo Order Number is required.")]
        public string OrderNumber { get; set; }
      //  public string Status { get; set; }

        public string CustomerName { get; set; }
        public string Destination { get; set; }
        [Required(ErrorMessage = "Pickup Location is required.")]
        public string PickupLocation { get; set; } // Add PickupLocation
        public string DropLocation { get; set; } // Add DropLocation
        public DateTime PickupDate { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "Cargo Type is required.")]
        public string CargoType { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Volume is required.")]
        public decimal Volume { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; } // Foreign Key to Customer

        // Navigation property to access the associated customer
        public Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
