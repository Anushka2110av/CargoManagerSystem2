﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoClass
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public double PricePerKm { get; set; }

        public DateTime PickupDate { get; set; }
    }
}
