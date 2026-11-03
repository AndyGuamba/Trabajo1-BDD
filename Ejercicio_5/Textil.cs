using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_5
{
    public class Textil : Artesania
    {
        public Textil(string codigo, string nombre, double precio, int stock, DateTime fechaEnVenta, Artesano propietario)
                        : base(codigo, nombre, precio, stock, fechaEnVenta, propietario)
        {
            
        }

        public override double CalcularComision(double valorVenta)
        {
            return valorVenta * 0.20;
        }
    }
}