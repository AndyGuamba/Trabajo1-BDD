using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_5
{
    public abstract class Artesania
    {
        public string Codigo {get; set;}
        public string Nombre {get; set;}
        public double Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaEnVenta { get; set; }
        public Artesano Propietario { get; set; }

        public Artesania(string codigo, string nombre, double precio, int stock, DateTime fechaEnVenta, Artesano propietario)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            FechaEnVenta = fechaEnVenta;
            Propietario = propietario;
        }

        public int ObtenerDiasEnVenta()
        {
            return (DateTime.Now - FechaEnVenta).Days; // DateTime.Now reemplazable por una fecha especifica
        }

        public bool SuperaDiasEnVenta()
        {
            return ObtenerDiasEnVenta() > 90;
        }

        public abstract double CalcularComision(double valorVenta);
    }
}