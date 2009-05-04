using System;
using System.Collections.Generic;
using System.Text;
using FishFa30;
using System.IO;

namespace Parking
{
    class Program
    {
        // Instancia de la interfaz
        public static FishFace fish = null;

        // Entradas
        public static Nr pulsadorEntrada = Nr.E1;
        public static Nr pulsadorSalida = Nr.E2;
        public static Nr finCarreraCerrada = Nr.E3;
        public static Nr celulaAcceso = Nr.E4;
        public static Nr finCarreraAbierta = Nr.E5;

        // Salidas
        public static Nr lamparaVerde = Nr.M1;
        public static Nr lamparaRoja = Nr.M1;
        public static Nr zumbador = Nr.M2;
        public static Nr lamparaCelulaOptica = Nr.M3;
        public static Nr motorBarrera = Nr.M4;

        // Variables de control
        public static int capacidadActual = 0;
        public static int capacidadMaxima = 2;
        public static int deadline = 5;
        public static Boolean parkingLleno = false;

        public static void Main(string[] args)
        {
            fish = new FishFace(true, false, 0);
            try
            {
                Boolean pulsadorDetectado = false;
                DateTime inicio;
                DateTime fin;
                Boolean dentro = false;
                Boolean fuera = false;

                Help();
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
                fish.OpenInterface(Port.COM1);
                Console.WriteLine("Iniciando sistema...");
                // Encender la lampara
                fish.SetMotor(lamparaCelulaOptica, Dir.On);
                actualizarLamparas();
                Console.WriteLine("Sistema iniciado.");
                while (true)
                {
                    pulsadorDetectado = fish.GetInput(pulsadorEntrada);
                    // Si se pulsa el pulsador de entrada al parking...
                    if (pulsadorDetectado)
                    {
                        if (!parkingLleno)
                        {
                            Console.WriteLine("Abriendo barrera...");
                            abrirBarrera();
                            Console.WriteLine("Barrera abierta. Entre, por favor.");
                            inicio = DateTime.Now;
                            fin = DateTime.Now;
                            while (((fin - inicio).Seconds < deadline) && !dentro)
                            {
                                if (!fish.GetInput(celulaAcceso)) dentro = true;
                                fin = DateTime.Now;
                            }
                            if (dentro) // El coche ha pasado dentro del deadline
                            {
                                escribirAccesos(1);
                                dentro = false;
                                cerrarBarrera();
                                capacidadActual++;
                                actualizarLamparas();
                            }
                            else // El coche NO ha pasado dentro del deadline
                            {
                                escribirIncidencias(1);
                                fish.SetMotor(zumbador, Dir.On);
                                Console.WriteLine(":: ERROR: No ha entrado ningun coche...");
                                Console.WriteLine("Pulse ENTER para desactivar la alarma.");
                                Console.ReadKey();
                                dentro = false;
                                fish.SetMotor(zumbador, Dir.Off);
                                cerrarBarrera();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Parking lleno");
                            fish.Pause(500);
                        }
                    }
                    pulsadorDetectado = fish.GetInput(pulsadorSalida);
                    // Si se pulsa el pulsador de salida del parking...
                    if (pulsadorDetectado)
                    {
                        Console.WriteLine("Abriendo barrera...");
                        abrirBarrera();
                        Console.WriteLine("Barrera abierta. Salga, por favor");
                        inicio = DateTime.Now;
                        fin = DateTime.Now;
                        while (((fin - inicio).Seconds < deadline) && !fuera)
                        {
                            if (!fish.GetInput(celulaAcceso)) fuera = true;
                            fin = DateTime.Now;
                        }
                        if (fuera)
                        {
                            escribirAccesos(2);
                            fuera = false;
                            cerrarBarrera();
                            capacidadActual--;
                            actualizarLamparas();
                        }
                        else
                        {
                            escribirIncidencias(2);
                            fish.SetMotor(zumbador, Dir.On);
                            Console.WriteLine(":: ERROR: No ha entrado ningun coche...");
                            Console.WriteLine("Pulse ENTER para desactivar la alarma.");
                            Console.ReadKey();
                            fuera = false;
                            fish.SetMotor(zumbador, Dir.Off);
                            cerrarBarrera();
                        }
                    }
                }
            }
            catch (FishFaceException)
            {
                Console.WriteLine("ERROR:: No se pudo abrir el interfaz");
                fish.CloseInterface();
                Console.ReadKey();
            }
        }

        public static void escribirFichero(String fichero, String linea)
        {
            String accessFile = System.AppDomain.CurrentDomain.BaseDirectory + fichero;
            File.AppendAllText(accessFile, linea);
        }

        public static void escribirIncidencias(int tipoAcceso)
        {
            String date = DateTime.Now.ToString("yyyy/MM/dd");
            String time = DateTime.Now.ToString("HH:mm:ss");
            String file = "incidencias.csv";
            String acceso = "Unknown operation"; // tipo == 1 (entrada); tipo == 2 (salida)
            if (tipoAcceso == 1) acceso = "Entrada";
            if (tipoAcceso == 2) acceso = "Salida";
            String line = acceso + ";" + date + ";" + time + "\n";
            escribirFichero(file, line);
            Console.Write("Incidencia registrada: " + line);
        }

        public static void escribirAccesos(int tipoAcceso)
        {
            String date = DateTime.Now.ToString("yyyy/MM/dd");
            String time = DateTime.Now.ToString("HH:mm:ss");
            String file = "accesos.csv";
            String acceso = "Unknown operation"; // tipo == 1 (entrada); tipo == 2 (salida)
            if (tipoAcceso == 1) acceso = "Entrada";
            if (tipoAcceso == 2) acceso = "Salida";
            String line = acceso + ";" + date + ";" + time + "\n";
            escribirFichero(file, line);
            Console.Write("Evento registrado: " + line);
        }

        public static void abrirBarrera()
        {
            Boolean stop = false;
            fish.SetMotor(motorBarrera, Dir.Right);
            while (!stop)
            {
                stop = fish.GetInput(finCarreraAbierta);
            }
            fish.SetMotor(motorBarrera, Dir.Off);
        }

        public static void cerrarBarrera()
        {
            Boolean stop = false;
            fish.SetMotor(motorBarrera, Dir.Left);
            while (!stop)
            {
                stop = fish.GetInput(finCarreraCerrada);
            }
            fish.SetMotor(motorBarrera, Dir.Off);
        }

        public static void actualizarLamparas()
        {
            if (capacidadActual < capacidadMaxima)
            {
                fish.SetMotor(lamparaVerde, Dir.Left);
                parkingLleno = false;
            }
            else
            {
                fish.SetMotor(lamparaRoja, Dir.Right);
                parkingLleno = true;
            }
        }

        public static void Help()
        {
            Console.WriteLine("Autores: José Domingo López López y Raúl Arias García");
            Console.WriteLine("Asignatura: Automatización Industrial");
            Console.WriteLine("Grupo: 08 (tarde)");
            Console.WriteLine("Curso: 4, 2008-009");
            Console.WriteLine();
            Console.WriteLine("Esta aplicación crea dos ficheros para mantener una traza de incidencias y entrada/salida de vehículos. Ambos ficheros son generados con formato CSV (comma-separated values) y serán creados en el directorio de la aplicación:\n" + System.AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine();
            Console.WriteLine("La conexión de los periféricos al interfaz debe ser la siguiente: ");
            Console.WriteLine("\t* Salida M1 -> Lámparas de semáforo (cableado en GND)");
            Console.WriteLine("\t* Salida M2 -> Zumbador");
            Console.WriteLine("\t* Salida M3 -> Lámpara incandescente en célula óptica");
            Console.WriteLine("\t* Salida M4 -> Motor de accionamiento de la barrera");
            Console.WriteLine("\t* Entrada E1 -> Pulsador para ENTRAR al parking");
            Console.WriteLine("\t* Entrada E2 -> Pulsador para SALIR del parking");
            Console.WriteLine("\t* Entrada E3 -> Interruptor de fin de carrera para CERRAR la barrera");
            Console.WriteLine("\t* Entrada E4 -> Célula óptica de acceso al parking");
            Console.WriteLine("\t* Entrada E5 -> Interruptor de fin de carrera para la APERTURA de la barrera");
        }
    }
}
