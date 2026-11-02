
using System;

namespace Trabajo1_BDD.Ejercicio_2
{
    public class CuentaAhorro : Cuenta
    {
        private double interesPorciento;

        public CuentaAhorro(double interesPorciento, Socio socio, double saldo)
            : base(socio, saldo)
        {
            if (interesPorciento < 0)
            {
                throw new ArgumentException("Interés inválido");
            }

            this.interesPorciento = interesPorciento;
        }

        protected override bool SaldoPermitido(double nuevoSaldo)
        {
            return nuevoSaldo >= 5;
        }

        public double InteresPorciento
        {
            get { return interesPorciento; }
            set
            {
                if (value >= 0)
                {
                    interesPorciento = value;
                }
            }
        }

        public override double CalcularInteres()
        {
            double interes = saldo * (interesPorciento / 100.0) / 12.0;
            return interes;
        }

        public bool AplicarInteresMensual()
        {
            if (!Estado)
            {
                return false;
            }

            double interes = CalcularInteres();

            if (interes <= 0)
            {
                return false;
            }

            saldo += interes;

            RegistroTransferencia registro =
                new RegistroTransferencia(this, interes, "Interes", saldo);

            historial.Add(registro);

            return true;
        }
    }
}
