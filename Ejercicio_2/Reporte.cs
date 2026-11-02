
using System;
using System.Collections.Generic;

namespace Trabajo1_BDD.Ejercicio_2
{
    public static class Reporte
    {
        private static List<Socio> socios = new List<Socio>();

        public static void AgregarSocio(Socio socio)
        {
            if (socio != null && !socios.Contains(socio))
            {
                socios.Add(socio);
            }
        }

        // 1. Estado de cuenta.
        public static string EstadoCuenta(Cuenta cuenta)
        {
            if (cuenta == null)
            {
                return "Cuenta inexistente";
            }

            string info = "";

            info += $"Socio: {cuenta.Socio.Informacion()}\n";
            info += $"Saldo inicial: {cuenta.SaldoInicial:F2}\n";

            foreach (RegistroTransferencia rt in cuenta.Historial)
            {
                info += rt.Informacion() + "\n";
            }

            info += $"Saldo actual: {cuenta.Saldo:F2}\n";

            return info;
        }

        // 2. Intereses del mes.
        public static string InteresesCuentasAhorro()
        {
            string info = "";

            foreach (Socio socio in socios)
            {
                foreach (Cuenta cuenta in socio.Cuentas)
                {
                    if (cuenta is CuentaAhorro)
                    {
                        CuentaAhorro ca = (CuentaAhorro)cuenta;

                        info += $"Socio: {socio.Informacion()}, ";
                        info += $"Interés mensual: {ca.CalcularInteres():F2}\n";
                    }
                }
            }

            return info;
        }

        // 3. Cuentas en sobregiro.
        public static string CuentasSobreGiro()
        {
            string info = "";

            foreach (Socio socio in socios)
            {
                foreach (Cuenta cuenta in socio.Cuentas)
                {
                    if (cuenta is CuentaCorriente && cuenta.Saldo < 0)
                    {
                        info += $"Socio: {socio.Informacion()}, ";
                        info += $"Saldo: {cuenta.Saldo:F2}\n";
                    }
                }
            }

            return info;
        }

        // 4. Total depositado y retirado.
        public static string TotalDepositadoRetirado(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            string info = "";

            foreach (Socio socio in socios)
            {
                double totalDepositado = 0;
                double totalRetirado = 0;

                foreach (Cuenta cuenta in socio.Cuentas)
                {
                    foreach (RegistroTransferencia rt in cuenta.Historial)
                    {
                        if (rt.Fecha >= fechaInicio &&
                            rt.Fecha <= fechaFin)
                        {
                            if (rt.Tipo == "Deposito" ||
                                rt.Tipo == "Interes")
                            {
                                totalDepositado += rt.Monto;
                            }
                            else if (rt.Tipo == "Retiro")
                            {
                                totalRetirado += rt.Monto;
                            }
                        }
                    }
                }

                info += $"Socio: {socio.Informacion()}, ";
                info += $"Depositado: {totalDepositado:F2}, ";
                info += $"Retirado: {totalRetirado:F2}\n";
            }

            return info;
        }

        // 5. Auditoría de saldos.
        public static string AuditoriaSaldos()
        {
            string info = "";

            foreach (Socio socio in socios)
            {
                foreach (Cuenta cuenta in socio.Cuentas)
                {
                    double saldoCalculado = cuenta.SaldoInicial;

                    foreach (RegistroTransferencia rt in cuenta.Historial)
                    {
                        if (rt.Tipo == "Deposito" ||
                            rt.Tipo == "Interes")
                        {
                            saldoCalculado += rt.Monto;
                        }
                        else if (rt.Tipo == "Retiro")
                        {
                            saldoCalculado -= rt.Monto;
                        }

                        if (Math.Abs(saldoCalculado - rt.SaldoResultante) > 0.01)
                        {
                            info += $"ERROR: {socio.Informacion()}, ";
                            info += $"Saldo inconsistente en movimiento ";
                            info += $"{rt.Fecha}\n";
                        }
                    }

                    if (Math.Abs(saldoCalculado - cuenta.Saldo) > 0.01)
                    {
                        info += $"ERROR: {socio.Informacion()}, ";
                        info += $"Saldo calculado: {saldoCalculado:F2}, ";
                        info += $"Saldo real: {cuenta.Saldo:F2}\n";
                    }
                }
            }

            if (info == "")
            {
                info = "Auditoría correcta. No se encontraron inconsistencias.";
            }

            return info;
        }
    }
}
