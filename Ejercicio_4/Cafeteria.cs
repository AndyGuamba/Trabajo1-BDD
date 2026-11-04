using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_4
{
    public class Cafeteria
    {
        public List<Ingrediente> InventarioIngredientes { get; set; } = new List<Ingrediente>();
        public List<Producto> MenuProductos { get; set; } = new List<Producto>();

        // Método para registrar una venta validando el stock de ingredientes
        bool VenderProducto(string productoId, int cantidad)
        {
            var producto = MenuProductos.FirstOrDefault(p => p.Id == productoId);
            if (producto == null) return false;

            if (producto is Plato plato)
            {
                // Validar si hay suficiente stock de cada ingrediente en la receta
                foreach (var item in plato.Receta)
                {
                    if (item.Ingrediente.StockActual < (item.CantidadNecesaria * cantidad))
                    {
                        Console.WriteLine($"Stock insuficiente de {item.Ingrediente.Nombre}");
                        return false;
                    }
                }

                // Descontar stock si pasa la validación
                foreach (var item in plato.Receta)
                {
                    item.Ingrediente.StockActual -= (item.CantidadNecesaria * cantidad);
                }
            }
            else if (producto is Bebida bebida)
            {
                if (bebida.StockUnidades < cantidad)
                {
                    Console.WriteLine($"Stock insuficiente de {bebida.Nombre}");
                    return false;
                }
                bebida.StockUnidades -= cantidad;
            }

            return true;
        }
    }

}
