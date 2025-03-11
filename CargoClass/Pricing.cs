using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class Pricing
    {
        [Key]
        public int PricingId { get; set; }
        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }
        [Required]
        [ForeignKey("CargoType")]
        public int CargoTypeId { get; set; }
        [Required]
        public double Price { get; set; }
        public virtual City City { get; set; }
        public virtual CargoType CargoType { get; set; }

    }
}
