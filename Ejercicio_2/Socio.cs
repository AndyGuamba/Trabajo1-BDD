
using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_2
{
    public class Socio
    {
        private string nombre;
        private string apellido;
        private string cedula;
        private List<Cuenta> cuentas;

        public Socio(string nombre, string apellido, string cedula)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.cedula = cedula;
            this.cuentas = new List<Cuenta>();
        }

        public List<Cuenta> Cuentas
        {
            get { return cuentas; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        public bool AgregarCuenta(Cuenta cuenta)
        {
            if (cuenta == null)
            {
                return false;
            }

            if (cuenta.Socio != this)
            {
                return false;
            }

            if (cuentas.Contains(cuenta))
            {
                return false;
            }

            cuentas.Add(cuenta);
            return true;
        }

        public bool EliminarCuenta(Cuenta cuenta)
        {
            if (cuenta == null || !cuentas.Contains(cuenta))
            {
                return false;
            }

            if (cuenta.Historial.Count > 0)
            {
                // Eliminación lógica.
                return cuenta.DesactivarCuenta();
            }

            if (cuenta.Saldo != 0)
            {
                return false;
            }

            // Si no tiene movimientos, se puede eliminar físicamente.
            return cuentas.Remove(cuenta);
        }

        public string Informacion()
        {
            return $"Nombre: {nombre}, Apellido: {apellido}, Cedula: {cedula}";
        }
    }
}
