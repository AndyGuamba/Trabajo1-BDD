using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_5
{
    public class Comunidad
    {
        public string IdComunidad { get; set; }
        public string Nombre { get; set; }

        public Comunidad(string idComunidad, string nombre)
        {
            IdComunidad = idComunidad;
            Nombre = nombre;
        }
    }
}