﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHomeStore.Models
{
    public class CarritoProducto
    {
        public int IdCarritoProducto { get; set; }
        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
