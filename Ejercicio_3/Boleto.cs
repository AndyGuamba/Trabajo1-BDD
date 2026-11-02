using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_3
{
    public class Boleto
    {
        public string Id { get; set; }
        public string ViajeId { get; set; }
        public int NumeroAsiento { get; set; }
        public string PasajeroId { get; set; }
        public bool DescuentoAplicado { get; set; }
        public decimal ValorPagado { get; set; }
        public bool Anulado { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
