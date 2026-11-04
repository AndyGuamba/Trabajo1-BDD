using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabajo1_BDD.Ejercicio_1;
using Trabajo1_BDD.Ejercicio_2;
using Trabajo1_BDD.Ejercicio_5;

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


            //---------------------------------------------------------------------------------------------


            //              EJERCICIO #2


            //Socio socio1 = new Socio("Eduardo", "Valladares", "1001");
            //Socio socio2 = new Socio("Carlos", "Perez", "1002");

            //CuentaAhorro ahorro1 = new CuentaAhorro(4, socio1, 500);
            //CuentaCorriente corriente1 = new CuentaCorriente(socio1, 100);

            //CuentaAhorro ahorro2 = new CuentaAhorro(4, socio2, 1000);
            //CuentaCorriente corriente2 = new CuentaCorriente(socio2, 50);

            //socio1.AgregarCuenta(ahorro1);
            //socio1.AgregarCuenta(corriente1);

            //socio2.AgregarCuenta(ahorro2);
            //socio2.AgregarCuenta(corriente2);

            //Reporte.AgregarSocio(socio1);
            //Reporte.AgregarSocio(socio2);

            //Console.WriteLine(ahorro1.Depositar(100));
            //Console.WriteLine(ahorro1.Retirar(50));

            //Console.WriteLine(
            //Cuenta.Transferir(ahorro1, corriente1, 200));

            //Console.WriteLine(
            //Cuenta.Transferir(ahorro1, corriente1, 1000));

            //Console.WriteLine(corriente1.Retirar(300));

            //ahorro1.AplicarInteresMensual();
            //ahorro2.AplicarInteresMensual();

            //Console.WriteLine(Reporte.EstadoCuenta(ahorro1));
            //Console.WriteLine(Reporte.EstadoCuenta(corriente1));

            //Console.WriteLine(Reporte.InteresesCuentasAhorro());
            //Console.WriteLine(Reporte.CuentasSobreGiro());

            //Console.WriteLine(
            //    Reporte.TotalDepositadoRetirado(
            //        DateTime.Now.AddMonths(-1),
            //        DateTime.Now));

            //Console.WriteLine(Reporte.AuditoriaSaldos());

            //Console.WriteLine(ahorro1.CerrarCuenta());

            //Console.ReadKey();


            //----------------------------------------------------------------------------------------------

            //              EJERCICIO #3


            Console.Clear();
            Ejercicio_3.Ejercicio3_Transporte.Ejecutar();

            //----------------------------------------------------------------------------------------------

            //              EJERCICIO #5 
            /*
            Console.WriteLine();
            Tienda tienda = new Tienda();

            Comunidad comPeguche = new Comunidad("C01", "Peguche");
            Comunidad comQuinchuqui = new Comunidad("C02", "Quinchuqui");
            tienda.Comunidades.Add(comPeguche);
            tienda.Comunidades.Add(comQuinchuqui);

            Artesano artesano1 = new Artesano("María", "Andrango", "1001112223", "0991112233", "maria@mail.com", comPeguche);
            Artesano artesano2 = new Artesano("José", "Cachimuel", "1003334445", "0994445566", "jose@mail.com", comQuinchuqui);
            tienda.Artesanos.Add(artesano1);
            tienda.Artesanos.Add(artesano2);

            tienda.RegistrarRecepcion("M01", "TXT-01", "Poncho Otavalo Tradicional", 100.0, 10, artesano1, true, DateTime.Now.AddDays(-100));
            tienda.RegistrarRecepcion("M02", "MAD-01", "Batea Tallada en Cedro", 80.0, 5, artesano2, false, DateTime.Now.AddDays(-10));

            try
            {
                tienda.RegistrarVenta("M03", "TXT-01", 2, DateTime.Now);
                tienda.RegistrarVenta("M04", "MAD-01", 1, DateTime.Now);
                tienda.RegistrarDevolucion("M05", "TXT-01", 1, artesano1, DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Aviso operacional: {ex.Message}");
            }

            try
            {
                tienda.RegistrarVenta("M06", "TXT-01", 20, DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"- Restricción aplicada: {ex.Message}");
            }

            try
            {

                tienda.EliminarArtesano("1001112223");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"- Restricción aplicada: {ex.Message}");
            }

            Console.WriteLine("\nReportes Administrativos\n");

            tienda.ReporteAuditoriaStock();
            Console.WriteLine();

            tienda.ReporteArtesaniasDevolver();
            Console.WriteLine();

            tienda.ReporteLiquidacionArtesanos(DateTime.Now.AddDays(-30), DateTime.Now.AddDays(1));
            Console.WriteLine();

            tienda.ReporteComunidadesConMasVentas();
            Console.WriteLine();

            tienda.ReporteArtesaniasMasVendidas();
            */
        }
    }
}
