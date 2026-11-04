using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabajo1_BDD.Ejercicio_5
{
    internal class Tienda
    {
        public List<Artesano> Artesanos { get; set; }
        public List<Artesania> Artesanias { get; set; }
        public List<Comunidad> Comunidades { get; set; }
        public List<Movimiento> Movimientos { get; set; }

        public Tienda()
        {
            Artesanos = new List<Artesano>();
            Artesanias = new List<Artesania>();
            Comunidades = new List<Comunidad>();
            Movimientos = new List<Movimiento>();
        }

        // Registrar Recepción de una Artesanía
        public void RegistrarRecepcion(string idMovimiento, string codigo, string nombre, double precio, int stock, Artesano propietario, bool esTextil, DateTime fechaIngreso)
        {
            if (Artesanias.Any(a => a.Codigo == codigo))
            {
                throw new InvalidOperationException("Ya existe una artesanía registrada con ese código.");
            }

            Artesania nuevaArtesania;
            if (esTextil)
            {
                nuevaArtesania = new Textil(codigo, nombre, precio, stock, fechaIngreso, propietario);
            }
            else
            {
                nuevaArtesania = new TallaMadera(codigo, nombre, precio, stock, fechaIngreso, propietario);
            }

            Artesanias.Add(nuevaArtesania);

            var movimiento = new Movimiento(idMovimiento, "RECEPCION", fechaIngreso, nuevaArtesania, stock, 0, 0, 0);
            Movimientos.Add(movimiento);
        }

        // Registrar Venta
        public void RegistrarVenta(string idMovimiento, string codigoArtesania, int cantidad, DateTime fechaVenta)
        {
            var artesania = Artesanias.FirstOrDefault(a => a.Codigo == codigoArtesania);
            if (artesania == null) throw new ArgumentException("La artesanía no se encuentra registrada.");

            if (cantidad <= 0) throw new ArgumentException("La cantidad a vender debe ser mayor a cero.");

            if (cantidad > artesania.Stock)
            {
                throw new InvalidOperationException("No se puede vender una cantidad mayor que las unidades disponibles en stock.");
            }

            artesania.Stock -= cantidad;

            double valorTotal = cantidad * artesania.Precio;
            double comisionTienda = artesania.CalcularComision(valorTotal); // Polimórfico (20% textil, 25% madera)
            double montoArtesano = valorTotal - comisionTienda;

            var movimiento = new Movimiento(idMovimiento, "VENTA", fechaVenta, artesania, cantidad, valorTotal, comisionTienda, montoArtesano);
            Movimientos.Add(movimiento);
        }

        // Registrar Devolucion
        public void RegistrarDevolucion(string idMovimiento, string codigoArtesania, int cantidad, Artesano artesanoDevolucion, DateTime fechaDevolucion)
        {
            var artesania = Artesanias.FirstOrDefault(a => a.Codigo == codigoArtesania);
            if (artesania == null) throw new ArgumentException("La artesanía no se encuentra registrada.");

            if (artesania.Propietario.Cedula != artesanoDevolucion.Cedula)
            {
                throw new InvalidOperationException("Una artesanía solo se puede devolver al artesano dueño de la pieza.");
            }

            if (cantidad <= 0) throw new ArgumentException("La cantidad a devolver debe ser mayor a cero.");

            if (cantidad > artesania.Stock)
            {
                throw new InvalidOperationException("No se puede devolver una cantidad mayor que las unidades disponibles actualmente.");
            }

            artesania.Stock -= cantidad;

            var movimiento = new Movimiento(idMovimiento, "DEVOLUCION", fechaDevolucion, artesania, cantidad, 0, 0, 0);
            Movimientos.Add(movimiento);
        }

        public void EliminarArtesano(string cedula)
        {
            var artesano = Artesanos.FirstOrDefault(a => a.Cedula == cedula);
            if (artesano == null) return;

            // Validación: No se elimina un artesano que tiene artesanías registradas
            bool tieneArtesanias = Artesanias.Any(a => a.Propietario.Cedula == cedula);
            if (tieneArtesanias)
            {
                throw new InvalidOperationException("No se puede eliminar al artesano porque tiene artesanías registradas en el sistema.");
            }

            Artesanos.Remove(artesano);
        }

        public void EliminarArtesania(string codigo)
        {
            var artesania = Artesanias.FirstOrDefault(a => a.Codigo == codigo);
            if (artesania == null) return;

            // Validación: No se elimina una artesanía que tiene movimientos asociados
            bool tieneMovimientos = Movimientos.Any(m => m.ArtesaniaAsociada.Codigo == codigo);
            if (tieneMovimientos)
            {
                throw new InvalidOperationException("No se puede eliminar la artesanía porque cuenta con un historial de movimientos.");
            }

            Artesanias.Remove(artesania);
        }

        // Liquidación de cada artesano en un período
        public void ReporteLiquidacionArtesanos(DateTime inicio, DateTime fin)
        {
            Console.WriteLine($"Liquidacion de artesanos ({inicio.ToShortDateString()} - {fin.ToShortDateString()})");

            var ventasPeriodo = Movimientos
                .Where(m => m.Tipo == "VENTA" && m.Fecha >= inicio && m.Fecha <= fin)
                .ToList();

            foreach (var artesano in Artesanos)
            {
                double totalVendido = 0;
                double totalComision = 0;
                double totalPagar = 0;

                var ventasArtesano = ventasPeriodo.Where(v => v.ArtesaniaAsociada.Propietario.Cedula == artesano.Cedula);

                foreach (var venta in ventasArtesano)
                {
                    totalVendido += venta.ValorTotal;
                    totalComision += venta.ComisionTienda;
                    totalPagar += venta.MontoArtesano;
                }

                Console.WriteLine($"Artesano: {artesano.Nombres} {artesano.Apellidos} | Vendido: ${totalVendido:F2} | Comisión Tienda: ${totalComision:F2} | A Pagar: ${totalPagar:F2}");
            }
        }

        // Artesanías que deben devolverse (> 90 días en la tienda)
        public void ReporteArtesaniasDevolver()
        {
            Console.WriteLine("Artesanias que deben devolverse");
            var caducadas = Artesanias.Where(a => a.SuperaDiasEnVenta() && a.Stock > 0).ToList();

            foreach (var item in caducadas)
            {
                Console.WriteLine($"Código: {item.Codigo} | Nombre: {item.Nombre} | Días en tienda: {item.ObtenerDiasEnVenta()} | Stock: {item.Stock} | Dueño: {item.Propietario.Nombres} {item.Propietario.Apellidos}");
            }
        }

        // Comunidades con más ventas
        public void ReporteComunidadesConMasVentas()
        {
            Console.WriteLine("Comunidades con mas ventas");

            var rankingComunidades = Movimientos
                .Where(m => m.Tipo == "VENTA")
                .GroupBy(m => m.ArtesaniaAsociada.Propietario.Comunidad.Nombre)
                .Select(g => new { Comunidad = g.Key, TotalVendido = g.Sum(x => x.ValorTotal) })
                .OrderByDescending(x => x.TotalVendido);

            foreach (var c in rankingComunidades)
            {
                Console.WriteLine($"Comunidad: {c.Comunidad} | Total Vendido: ${c.TotalVendido:F2}");
            }
        }

        // Artesanías más vendidas
        public void ReporteArtesaniasMasVendidas()
        {
            Console.WriteLine("Artesanias mas vendidas");

            var masVendidas = Movimientos
                .Where(m => m.Tipo == "VENTA")
                .GroupBy(m => m.ArtesaniaAsociada.Nombre)
                .Select(g => new { Nombre = g.Key, CantidadVendida = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.CantidadVendida);

            foreach (var a in masVendidas)
            {
                Console.WriteLine($"Artesanía: {a.Nombre} | Unidades Vendidas: {a.CantidadVendida}");
            }
        }

        // Auditoría de las unidades disponibles
        public void ReporteAuditoriaStock()
        {
            Console.WriteLine("Auditoria de unidades disponibles");
            foreach (var a in Artesanias)
            {
                Console.WriteLine($"Código: {a.Codigo} | Nombre: {a.Nombre} | Tipo: {a.GetType().Name} | Stock Actual: {a.Stock} | Dueño: {a.Propietario.Nombres} {a.Propietario.Apellidos}");
            }
        }
    }
}