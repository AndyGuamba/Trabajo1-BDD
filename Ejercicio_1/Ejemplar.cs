using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Ejemplar
    {
        public string Codigo { get; private set; }
        public Material ejemplar;
        public bool Disponible { get; set; } = true;

        public Ejemplar(string codigo, Material material)
        {
            this.Codigo = codigo;
            this.ejemplar = material;
        }
        public bool EstaDisponible()
        {
            if (Disponible)
            {
                Disponible = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
