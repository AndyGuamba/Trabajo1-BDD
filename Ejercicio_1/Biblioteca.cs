using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Biblioteca
    {
        public List<Usuario> Usuarios { get; private set; } = new List<Usuario>();
        public List<Ejemplar> Ejemplares { get; private set; } = new List<Ejemplar>();
        public List<Prestamo> PrestamosTotales { get; private set; } = new List<Prestamo>();

        public void ReporteAuditoriaDisponibles()
        {
            var disponibles = Ejemplares.Where(e => e.Disponible).ToList();

            Console.WriteLine("\n--- AUDITORÍA DE EJEMPLARES DISPONIBLES ---");
            foreach (var e in disponibles)
            {
                Console.WriteLine($"Código: {e.Codigo} | Título: {e.ejemplar.Titulo}");
            }
            Console.WriteLine($"Total disponibles en estantes: {disponibles.Count}");
        }

        public void ReporteVencidos()
        {
            Console.WriteLine("\n--- PRÉSTAMOS VENCIDOS ---");
            var vencidos = PrestamosTotales.Where(p =>
                p.EstaActivo &&
                (DateTime.Now - p.FechaSalida).Days > p.DiasPermitidos).ToList();

            foreach (var p in vencidos)
            {
                int diasRetraso = (DateTime.Now - p.FechaSalida).Days - p.DiasPermitidos;
                Console.WriteLine($"Usuario: {p.Lector.Nombre} | Ejemplar: {p.ItemFisico.Codigo} | Retraso: {diasRetraso} días");
            }
        }

        public void ReporteHistorialUsuario(string cedula)
        {
            Console.WriteLine($"\n--- HISTORIAL DEL USUARIO ({cedula}) ---");
            var usuario = Usuarios.FirstOrDefault(u => u.Cedula == cedula);

            if (usuario != null)
            {
                foreach (var p in usuario.Historial)
                {
                    string estado = p.EstaActivo ? "En curso" : "Devuelto";
                    Console.WriteLine($"Material: {p.ItemFisico.ejemplar.Titulo} | Fecha Salida: {p.FechaSalida.ToShortDateString()} | Estado: {estado}");
                }
            }
        }

        public void ReporteMaterialesMasPrestados()
        {
            Console.WriteLine("\n--- MATERIALES MÁS PRESTADOS ---");
            var ranking = PrestamosTotales
                .GroupBy(p => p.ItemFisico.ejemplar.Titulo)
                .Select(grupo => new { Titulo = grupo.Key, Cantidad = grupo.Count() })
                .OrderByDescending(x => x.Cantidad)
                .ToList();

            foreach (var item in ranking)
            {
                Console.WriteLine($"Título: {item.Titulo} | Veces prestado: {item.Cantidad}");
            }
        }

        public void ReporteMultasPorPeriodo(DateTime inicio, DateTime fin)
        {
            Console.WriteLine($"\n--- MULTAS RECAUDADAS ({inicio.ToShortDateString()} al {fin.ToShortDateString()}) ---");
            double totalMultas = PrestamosTotales
                .Where(p => !p.EstaActivo && p.FechaEntrega >= inicio && p.FechaEntrega <= fin)
                .Sum(p => p.MultaGenerada);

            Console.WriteLine($"Total recaudado: ${totalMultas}");
        }
    }


}
