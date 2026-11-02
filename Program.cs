using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo1_BDD.Ejercicio_1;

namespace Trabajo1_BDD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //              EJERCICIO 1
            // Biblioteca miBiblioteca = new Biblioteca();

            // Usuario user1 = new Usuario("1002003004", "Andy Guamba");
            // Usuario user2 = new Usuario("1122334455", "Joel Altura");
            // miBiblioteca.Usuarios.Add(user1);
            // miBiblioteca.Usuarios.Add(user2);

            // Libro libro1 = new Libro("ISBN-001", "Diseño de Software", "Sommerville");
            // Revista revista1 = new Revista("REV-001", "PC World", "IDG");

            // Ejemplar ej1 = new Ejemplar("EJ-001", libro1);
            // Ejemplar ej2 = new Ejemplar("EJ-002", libro1);
            // Ejemplar ej3 = new Ejemplar("REV-001", revista1);

            // miBiblioteca.Ejemplares.Add(ej1);
            // miBiblioteca.Ejemplares.Add(ej2);
            // miBiblioteca.Ejemplares.Add(ej3);

            // Prestamo p1 = new Prestamo(user1, ej1, DateTime.Now.AddDays(-10), 7, 0.25);
            // miBiblioteca.PrestamosTotales.Add(p1);
            // p1.Devolver(DateTime.Now);

            // Prestamo p2 = new Prestamo(user1, ej2, DateTime.Now.AddDays(-8), 7, 0.25);
            // miBiblioteca.PrestamosTotales.Add(p2);

            // Prestamo p3 = new Prestamo(user2, ej3, DateTime.Now.AddDays(-1), 3, 0.50);
            // miBiblioteca.PrestamosTotales.Add(p3);

            //Console.WriteLine("========== MÓDULO DE REPORTES ==========");

            // miBiblioteca.ReporteAuditoriaDisponibles();
            // miBiblioteca.ReporteVencidos();
            // miBiblioteca.ReporteHistorialUsuario("1002003004");
            // miBiblioteca.ReporteMaterialesMasPrestados();

            // miBiblioteca.ReporteMultasPorPeriodo(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(1));

            // Console.ReadLine();


            //              EJERCICIO 2












            //              EJERCICIO 3
            Console.Clear();
            Console.WriteLine("Ejercicio 3: Cooperativa de Transporte");
            Ejercicio_3.Ejercicio3_Transporte.Ejecutar();

        }

    }
}
