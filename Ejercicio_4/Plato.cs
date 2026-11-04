using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_4
{
    public class Plato : Producto
    {
        public List<RecetaElemento> Receta { get; set; } = new List<RecetaElemento>();

        public decimal CalcularCosto()
        {
            return Receta.Sum(r => (decimal)r.CantidadNecesaria * r.Ingrediente.CostoUnitario);
        }

        public override decimal CalcularPrecioVenta()
        {
            return CalcularCosto() * 2; // El precio de venta es el doble del costo
        }
    }
}
