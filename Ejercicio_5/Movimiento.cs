using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Factory
{
    public class Movimiento
    {
        public string IdMovimiento { get; set; }
        public string Tipo { get; set; } // "RECEPCION", "VENTA", "DEVOLUCION"
        public DateTime Fecha { get; set; }
        public Artesania ArtesaniaAsociada { get; set; }
        public int Cantidad { get; set; }
        public double ValorTotal { get; set; }
        public double ComisionTienda { get; set; }
        public double MontoArtesano { get; set; }

        public Movimiento(string idMovimiento, string tipo, DateTime fecha, Artesania artesaniaAsociada, int cantidad, double valorTotal = 0, double comisionTienda = 0, double montoArtesano = 0)
        {
            IdMovimiento = idMovimiento;
            Tipo = tipo;
            Fecha = fecha.Date;
            ArtesaniaAsociada = artesaniaAsociada;
            Cantidad = cantidad;
            ValorTotal = valorTotal;
            ComisionTienda = comisionTienda;
            MontoArtesano = montoArtesano;
        }
    }
}
