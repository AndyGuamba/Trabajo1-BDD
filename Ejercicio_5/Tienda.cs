using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_5
{
    internal class Tienda
    {
        public List<Artesano> Artesanos { get; set; }
        public List<Artesania> Artesanias { get; set; }
        public List<Comunidad> Comunidades { get; set; }
        public List<Movimiento> Movimientos { get; set; }

        public Tienda()
        {
            Artesanos = new List<Artesano>();
            Artesanias = new List<Artesania>();
            Comunidades = new List<Comunidad>();
            Movimientos = new List<Movimiento>();
        }
    }
}