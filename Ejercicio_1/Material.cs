using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo1_BDD.Ejercicio_1
{
    public class Material
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Autor { get; set; }
        protected Material(string titulo, string descripcion, string autor)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Autor = autor;
        }
    }
}
