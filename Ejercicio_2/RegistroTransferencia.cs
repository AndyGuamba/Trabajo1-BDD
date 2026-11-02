
using System;

namespace Trabajo1_BDD.Ejercicio_2
{
    public class RegistroTransferencia
    {
        private Cuenta cuenta;
        private double monto;
        private DateTime fecha;
        private string tipo;
        private double saldoResultante;

        public RegistroTransferencia(
            Cuenta cuenta,
            double monto,
            string tipo,
            double saldoResultante)
        {
            this.cuenta = cuenta;
            this.monto = monto;
            this.tipo = tipo;
            this.saldoResultante = saldoResultante;
            this.fecha = DateTime.Now;
        }

        public Cuenta Cuenta
        {
            get { return cuenta; }
        }

        public double Monto
        {
            get { return monto; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
        }

        public string Tipo
        {
            get { return tipo; }
        }

        public double SaldoResultante
        {
            get { return saldoResultante; }
        }

        public string Informacion()
        {
            return $"Fecha: {fecha}, " +
                   $"Tipo: {tipo}, " +
                   $"Monto: {monto:F2}, " +
                   $"Saldo: {saldoResultante:F2}";
        }
    }
}
