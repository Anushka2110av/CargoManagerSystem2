using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CargoOrderId { get; set; }

        [ForeignKey("CargoOrderId")]
        public CargoOrder CargoOrder { get; set; }

        public string FromLocation { get; set; }
        public string ToLocation { get; set; }

        [Required(ErrorMessage = "Stored Weight is required.")]
        public decimal StoredWeight { get; set; }  // Actual stored weight

        [Required(ErrorMessage = "Stored Volume is required.")]
        public decimal StoredVolume { get; set; }  // Actual stored volume

        [Required(ErrorMessage = "Warehouse location is required.")]
        public string WarehouseLocation { get; set; }  // Where the cargo is stored

        public DateTime StoredAt { get; set; } = DateTime.Now;
        public DateTime MovedDate { get; set; } = DateTime.Now;
    }
}
