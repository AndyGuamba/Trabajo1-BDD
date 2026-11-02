using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_3
{
    internal class DatosTransporte
    {
        public List<Viaje> Viajes { get; set; } = new List<Viaje>();
        public List<Boleto> Boletos { get; set; } = new List<Boleto>();
        public List<Pasajero> Pasajeros { get; set; } = new List<Pasajero>();
    }
}
