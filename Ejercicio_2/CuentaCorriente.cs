
namespace Trabajo1_BDD.Ejercicio_2
{
    public class CuentaCorriente : Cuenta
    {
        public CuentaCorriente(Socio socio, double saldo)
            : base(socio, saldo)
        {
        }

        protected override bool SaldoPermitido(double nuevoSaldo)
        {
            return nuevoSaldo >= -200;
        }

        public override double CalcularInteres()
        {
            return 0;
        }
    }
}
