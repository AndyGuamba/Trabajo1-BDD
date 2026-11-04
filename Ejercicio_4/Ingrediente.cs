using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_4
{
    public class Ingrediente
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedida { get; set; } // "kg" o "litros"
        public decimal CostoUnitario { get; set; }
        public double StockActual { get; set; }
        public double StockMinimo { get; set; }

        public bool EstaBajoMinimo() => StockActual <= StockMinimo;
    }
}
