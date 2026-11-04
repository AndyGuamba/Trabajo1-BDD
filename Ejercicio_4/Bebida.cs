using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_4
{
    public class Bebida : Producto
    {
        public decimal PrecioBase { get; set; }
        public int StockUnidades { get; set; }

        public override decimal CalcularPrecioVenta()
        {
            return PrecioBase * 1.15m; // Precio base + 15% IVA
        }
    }
}
