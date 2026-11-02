using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Trabajo1_BDD.Ejercicio_3
{
    public static class Ejercicio3_Transporte
    {
        private const string Archivo = "transporte.json";

        public static void Ejecutar()
        {
            var datos = CargarDatos();

            if (!datos.Viajes.Any())
            {
                CargarDatosDePrueba(datos);
                GuardarDatos(datos);
            }

            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("   EJERCICIO 3: COOPERATIVA DE TRANSPORTE IBARRA    ");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Venta de Boleto");
                Console.WriteLine("2. Anulación de Boleto");
                Console.WriteLine("3. Reporte: Mapa de Asientos de un Viaje");
                Console.WriteLine("4. Reporte: Recaudacion por Ruta en un Periodo");
                Console.WriteLine("5. Reporte: Porcentaje de Ocupacion por Viaje");
                Console.WriteLine("6. Reporte: Auditoría de Asientos Libres");
                Console.WriteLine("7. Volver al Menu Principal");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Seleccione una opcion: ");

                switch (Console.ReadLine())
                {
                    case "1": VenderBoleto(datos); break;
                    case "2": AnularBoleto(datos); break;
                    case "3": MapaAsientos(datos); break;
                    case "4": RecaudacionRuta(datos); break;
                    case "5": PorcentajeOcupacion(datos); break;
                    case "6": AuditoriaLibres(datos); break;
                    case "7": volver = true; break;
                    default:
                        Console.WriteLine("Opcion no valida. Presione ENTER...");
                        Console.ReadLine();
                        break;
                }
            }
            GuardarDatos(datos);
        }

        private static void VenderBoleto(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- VENTA DE BOLETO ---");
            Console.Write("ID del Viaje (ej. V-QUITO): "); string vId = Console.ReadLine()?.Trim();
            var viaje = datos.Viajes.FirstOrDefault(x => x.Id.Equals(vId, StringComparison.OrdinalIgnoreCase));

            if (viaje == null || DateTime.Now >= viaje.FechaHoraSalida)
            {
                Console.WriteLine("ERROR: El viaje no existe o ya ha salido.");
                Pausar(); return;
            }

            Console.Write($"Numero de Asiento (1 a {viaje.CapacidadAsientos}): ");
            if (!int.TryParse(Console.ReadLine(), out int numAsiento) || numAsiento < 1 || numAsiento > viaje.CapacidadAsientos)
            {
                Console.WriteLine("ERROR: Numero de asiento no valido.");
                Pausar(); return;
            }

            if (viaje.AsientosOcupados.Contains(numAsiento))
            {
                Console.WriteLine("ERROR: El asiento seleccionado ya est ocupado.");
                Pausar(); return;
            }

            Console.Write("Cedula del Pasajero: "); string pasajeroId = Console.ReadLine()?.Trim();
            var pasajero = datos.Pasajeros.FirstOrDefault(p => p.Id.Equals(pasajeroId, StringComparison.OrdinalIgnoreCase));

            if (pasajero == null)
            {
                Console.Write("Nombre del Pasajero: "); string nombre = Console.ReadLine()?.Trim();
                Console.Write("¿Aplica 50% descuento (3ra Edad / Discapacidad)? (s/n): ");
                bool descto = Console.ReadLine()?.Trim().ToLower() == "s";

                pasajero = new Pasajero { Id = pasajeroId, Nombre = nombre, AplicaDescuento = descto };
                datos.Pasajeros.Add(pasajero);
            }

            decimal tarifaCalculada = viaje.Directo ? viaje.TarifaBase : (viaje.TarifaBase * 0.85m);
            if (pasajero.AplicaDescuento)
            {
                tarifaCalculada *= 0.50m;
            }

            tarifaCalculada = Math.Round(tarifaCalculada, 2);

            viaje.AsientosOcupados.Add(numAsiento);
            var nuevoBoleto = new Boleto
            {
                Id = "B-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                ViajeId = viaje.Id,
                NumeroAsiento = numAsiento,
                PasajeroId = pasajero.Id,
                DescuentoAplicado = pasajero.AplicaDescuento,
                ValorPagado = tarifaCalculada,
                Anulado = false,
                FechaEmision = DateTime.Now
            };

            datos.Boletos.Add(nuevoBoleto);
            GuardarDatos(datos);

            Console.WriteLine($"\n¡Boleto Emitido con Exito!");
            Console.WriteLine($"ID Boleto: {nuevoBoleto.Id} | Pasajero: {pasajero.Nombre} | Asiento: #{numAsiento} | Total a Pagar: ${tarifaCalculada:F2}");
            Pausar();
        }

        private static void AnularBoleto(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- ANULACIÓN DE BOLETO ---");
            Console.Write("ID del Boleto: "); string bId = Console.ReadLine()?.Trim();
            var boleto = datos.Boletos.FirstOrDefault(x => x.Id.Equals(bId, StringComparison.OrdinalIgnoreCase));

            if (boleto == null || boleto.Anulado)
            {
                Console.WriteLine("ERROR: Boleto no encontrado o ya fue anulado.");
                Pausar(); return;
            }

            var viaje = datos.Viajes.FirstOrDefault(x => x.Id == boleto.ViajeId);

            if (viaje == null || (viaje.FechaHoraSalida - DateTime.Now).TotalHours < 2)
            {
                Console.WriteLine("ERROR: Solo se puede anular un boleto hasta 2 horas antes de la salida.");
                Pausar(); return;
            }

            boleto.Anulado = true;
            viaje.AsientosOcupados.Remove(boleto.NumeroAsiento);

            Console.WriteLine($"\n¡Boleto {boleto.Id} anulado exitosamente! Asiento #{boleto.NumeroAsiento} liberado.");
            GuardarDatos(datos);
            Pausar();
        }

        private static void MapaAsientos(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- REPORTE: MAPA DE ASIENTOS DE UN VIAJE ---");
            Console.Write("Ingrese ID del Viaje: "); string vId = Console.ReadLine()?.Trim();
            var viaje = datos.Viajes.FirstOrDefault(x => x.Id.Equals(vId, StringComparison.OrdinalIgnoreCase));

            if (viaje == null)
            {
                Console.WriteLine("ERROR: Viaje no encontrado.");
                Pausar(); return;
            }

            Console.WriteLine($"\nRuta: {viaje.Ruta} | Salida: {viaje.FechaHoraSalida:dd/MM/yyyy HH:mm} | Tipo: {(viaje.Directo ? "Directo" : "Intermedio")}");
            Console.WriteLine("--------------------------------------------------");
            for (int i = 1; i <= viaje.CapacidadAsientos; i++)
            {
                string estado = viaje.AsientosOcupados.Contains(i) ? "[ OCUPADO ]" : "[ LIBRE ]  ";
                Console.Write($"Asiento {i:D2}: {estado}   ");
                if (i % 2 == 0) Console.WriteLine();
            }
            Pausar();
        }

        private static void RecaudacionRuta(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- REPORTE: RECAUDACION POR RUTA ---");
            var recaudacion = datos.Boletos.Where(b => !b.Anulado)
                .Join(datos.Viajes, b => b.ViajeId, v => v.Id, (b, v) => new { v.Ruta, b.ValorPagado })
                .GroupBy(x => x.Ruta)
                .Select(g => new { Ruta = g.Key, Total = g.Sum(x => x.ValorPagado) });

            if (!recaudacion.Any())
            {
                Console.WriteLine("No se han registrado ventas activas.");
            }
            else
            {
                foreach (var r in recaudacion)
                {
                    Console.WriteLine($"Ruta: {r.Ruta,-20} | Total Recaudado: ${r.Total,8:F2}");
                }
            }
            Pausar();
        }

        private static void PorcentajeOcupacion(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- REPORTE: PORCENTAJE DE OCUPACION ---");
            foreach (var v in datos.Viajes)
            {
                double porcentaje = ((double)v.AsientosOcupados.Count / v.CapacidadAsientos) * 100;
                Console.WriteLine($"Viaje: {v.Id} | Ruta: {v.Ruta,-15} | Salida: {v.FechaHoraSalida:dd/MM HH:mm} | Ocupacion: {porcentaje,5:F1}% ({v.AsientosOcupados.Count}/{v.CapacidadAsientos})");
            }
            Pausar();
        }

        private static void AuditoriaLibres(DatosTransporte datos)
        {
            Console.Clear();
            Console.WriteLine("--- AUDITORIA DE ASIENTOS LIBRES ---");
            foreach (var v in datos.Viajes)
            {
                int libres = v.CapacidadAsientos - v.AsientosOcupados.Count;
                Console.WriteLine($"Viaje ID: {v.Id} | Ruta: {v.Ruta,-15} | Capacidad: {v.CapacidadAsientos} | Ocupados: {v.AsientosOcupados.Count} | Libres: {libres}");
            }
            Pausar();
        }

        private static void CargarDatosDePrueba(DatosTransporte datos)
        {
            datos.Viajes.Add(new Viaje { Id = "V-QUITO", Ruta = "Ibarra-Quito", FechaHoraSalida = DateTime.Now.AddHours(5), CapacidadAsientos = 20, TarifaBase = 4.00m, Directo = true });
            datos.Viajes.Add(new Viaje { Id = "V-TULCAN", Ruta = "Ibarra-Tulcán", FechaHoraSalida = DateTime.Now.AddHours(12), CapacidadAsientos = 15, TarifaBase = 3.50m, Directo = false });
            datos.Viajes.Add(new Viaje { Id = "V-OTAVALO", Ruta = "Ibarra-Otavalo", FechaHoraSalida = DateTime.Now.AddHours(1), CapacidadAsientos = 10, TarifaBase = 1.00m, Directo = true });
        }

        private static DatosTransporte CargarDatos()
        {
            if (!File.Exists(Archivo)) return new DatosTransporte();
            string json = File.ReadAllText(Archivo);
            return JsonSerializer.Deserialize<DatosTransporte>(json) ?? new DatosTransporte();
        }

        private static void GuardarDatos(DatosTransporte datos)
        {
            string json = JsonSerializer.Serialize(datos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Archivo, json);
        }

        private static void Pausar()
        {
            Console.WriteLine("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
