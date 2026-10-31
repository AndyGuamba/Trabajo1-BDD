using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Usuario
    {
        public string Cedula { get; private set; }
        public string Nombre { get; private set; }
        public double MultasPendientes { get; set; }
        public List<Prestamo> Historial { get; private set; } = new List<Prestamo>();

        public Usuario(string cedula, string nombre)
        {
            this.Cedula = cedula;
            this.Nombre = nombre;
            this.MultasPendientes = 0;
        }

        public int GetPrestamosActivos()
        {
            return Historial.Count(p => p.EstaActivo);
        }

        public bool PuedeSolicitar()
        {
            if (GetPrestamosActivos() >= 3)
            {
                Console.WriteLine("Error: Límite de 3 préstamos alcanzado.");
                return false;
            }

            if (MultasPendientes > 0)
            {
                Console.WriteLine("Error: Tiene multas pendientes de pagar.");
                return false;
            }

            return true; 
        }
    }
}
