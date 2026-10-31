using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Prestamo
    {
        public Usuario Lector { get; private set; }
        public Ejemplar ItemFisico { get; private set; }
        public DateTime FechaSalida { get; private set; }
        public bool EstaActivo { get; private set; }

        // Guardamos las reglas del negocio directamente en el préstamo
        public int DiasPermitidos { get; private set; }
        public double TarifaMulta { get; private set; }

        public DateTime FechaEntrega { get; private set; }
        public double MultaGenerada { get; private set; }

        // Pedimos los días y la tarifa en el constructor
        public Prestamo(Usuario lector, Ejemplar itemFisico, DateTime fechaSalida, int diasPermitidos, double tarifaMulta)
        {
            this.Lector = lector;
            this.ItemFisico = itemFisico;
            this.FechaSalida = fechaSalida;
            this.DiasPermitidos = diasPermitidos;
            this.TarifaMulta = tarifaMulta;
            this.EstaActivo = true;

            this.ItemFisico.Disponible = false;
            this.Lector.Historial.Add(this);
        }

        // Ahora Devolver solo necesita la fecha, porque ya conoce sus días y su tarifa
        public void Devolver(DateTime fechaEntrega)
        {
            if (!EstaActivo) return;

            this.EstaActivo = false;
            this.ItemFisico.Disponible = true;
            this.FechaEntrega = fechaEntrega;

            TimeSpan tiempoTranscurrido = fechaEntrega - FechaSalida;
            int diasRetraso = tiempoTranscurrido.Days - this.DiasPermitidos;

            if (diasRetraso > 0)
            {
                double totalMulta = diasRetraso * this.TarifaMulta;
                this.MultaGenerada = totalMulta;
                this.Lector.MultasPendientes += totalMulta;
            }
        }
    }
}
