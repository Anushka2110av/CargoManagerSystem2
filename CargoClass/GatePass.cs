using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class GatePass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoOrderId { get; set; }

        [ForeignKey("CargoOrderId")]
        public CargoOrder CargoOrder { get; set; }

        public string PassType { get; set; } // "Entry" or "Exit"
        public DateTime IssuedDate { get; set; } = DateTime.Now;

        public string Status { get; set; }

        public DateTime? ExitTime { get; set; }
    }
}
