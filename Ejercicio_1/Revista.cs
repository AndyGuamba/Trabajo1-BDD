using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Revista: Material
    {
        public int DiasPrestamo { get; private set; }
        public double MultaDiaria { get; private set; }

        public Revista(string titulo, string descripcion, string autor) : base(titulo, descripcion, autor)
        {
            this.DiasPrestamo = 3;
            this.MultaDiaria = 0.50;
        }
    }
}
