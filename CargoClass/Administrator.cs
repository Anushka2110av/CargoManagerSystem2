using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class Administrator
    {
        [Key]  //Primary key
        public int AdminId { get; set; } // Primary Key

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8, ErrorMessage = "Password must be atleast 8 character long.")]
        public string Password { get; set; }
    }
}
