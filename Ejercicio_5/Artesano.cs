using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_5
{
    public class Artesano
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public Comunidad Comunidad { get; set; }

        public Artesano(string nombres, string apellidos, string cedula, string telefono, string correoElectronico, Comunidad comunidad)
        {
            Nombres = nombres;
            Apellidos = apellidos;
            Cedula = cedula;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            Comunidad = comunidad;
        }
    }
}