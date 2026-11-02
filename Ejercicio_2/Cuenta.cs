
using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_2
{
    public abstract class Cuenta
    {
        protected double saldo;
        protected Socio socio;
        protected bool estado;
        protected DateTime fechaCreacion;
        protected List<RegistroTransferencia> historial;

        private double saldoInicial;

        public Cuenta(Socio socio, double saldo)
        {
            if (socio == null)throw new ArgumentNullException("El socio no puede ser nulo");
            

            if (!SaldoPermitido(saldo))
            {
                throw new ArgumentException("Saldo inicial inválido");
            }

            this.socio = socio;
            this.saldo = saldo;
            this.saldoInicial = saldo;
            this.fechaCreacion = DateTime.Now;
            this.estado = true;
            this.historial = new List<RegistroTransferencia>();
        }

        public Socio Socio
        {
            get { return socio; }
        }

        public double Saldo
        {
            get { return saldo; }
        }

        public bool Estado
        {
            get { return estado; }
        }

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
        }

        public List<RegistroTransferencia> Historial
        {
            get { return historial; }
        }

        public double SaldoInicial
        {
            get { return saldoInicial; }
        }

        protected abstract bool SaldoPermitido(double nuevoSaldo);

        public bool Depositar(double monto)
        {
            if (!estado || monto <= 0)
            {
                return false;
            }

            double nuevoSaldo = saldo + monto;

            if (!SaldoPermitido(nuevoSaldo))
            {
                return false;
            }

            saldo = nuevoSaldo;

            RegistroTransferencia registro =
                new RegistroTransferencia(this, monto, "Deposito", saldo);

            historial.Add(registro);

            return true;
        }

        public bool Retirar(double monto)
        {
            if (!estado || monto <= 0)
            {
                return false;
            }

            double nuevoSaldo = saldo - monto;

            if (!SaldoPermitido(nuevoSaldo))
            {
                return false;
            }

            saldo = nuevoSaldo;

            RegistroTransferencia registro =
                new RegistroTransferencia(this, monto, "Retiro", saldo);

            historial.Add(registro);

            return true;
        }

        public static bool Transferir(Cuenta origen, Cuenta destino, double monto)
        {
            if (origen == null || destino == null)
            {
                return false;
            }

            if (origen == destino || monto <= 0)
            {
                return false;
            }

            if (!origen.estado || !destino.estado)
            {
                return false;
            }

            double saldoOrigen = origen.saldo - monto;
            double saldoDestino = destino.saldo + monto;

            // Validamos ambas cuentas antes de modificar cualquiera.
            if (!origen.SaldoPermitido(saldoOrigen))
            {
                return false;
            }

            if (!destino.SaldoPermitido(saldoDestino))
            {
                return false;
            }

            // Aplicamos las dos operaciones.
            origen.saldo = saldoOrigen;
            destino.saldo = saldoDestino;

            RegistroTransferencia retiro =
                new RegistroTransferencia(origen, monto, "Retiro", origen.saldo);

            RegistroTransferencia deposito =
                new RegistroTransferencia(destino, monto, "Deposito", destino.saldo);

            origen.historial.Add(retiro);
            destino.historial.Add(deposito);

            return true;
        }

        public bool CerrarCuenta()
        {
            if (!estado || saldo != 0)
            {
                return false;
            }

            estado = false;
            return true;
        }

        public bool DesactivarCuenta()
        {
            if (!estado)
            {
                return false;
            }

            estado = false;
            return true;
        }

        public bool ActivarCuenta()
        {
            if (estado)
            {
                return false;
            }

            estado = true;
            return true;
        }

        public abstract double CalcularInteres();
    }
}
