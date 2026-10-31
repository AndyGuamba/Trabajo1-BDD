using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    internal class Libro: Material
    {
        public int DiasPrestamo { get; private set; }
        public double MultaDiaria { get; private set; }
        public Libro(string titulo, string descripcion, string autor) : base(titulo, descripcion, autor)
        {
            this.DiasPrestamo = 7;
            this.MultaDiaria = 0.25;
        }
    }
}
