using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_4
{
    public abstract class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        // Método abstracto que obligará a Plato y Bebida a implementar su propio cálculo de precio
        public abstract decimal CalcularPrecioVenta();
    }
}
