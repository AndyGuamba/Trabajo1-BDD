using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_3
{
    public class Viaje
    {
        public string Id { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaHoraSalida { get; set; }
        public int CapacidadAsientos { get; set; }
        public decimal TarifaBase { get; set; }
        public bool Directo { get; set; }
        public List<int> AsientosOcupados { get; set; } = new List<int>();
    }
}
