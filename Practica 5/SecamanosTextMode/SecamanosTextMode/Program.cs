using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishFa30;

namespace SecamanosTextMode
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Se deben conectar:
             * lámpara en la salida digital M1
             * célula óptica en la entrada digital E1
             * motor en la salida digital M2
            */
            Boolean luzDetectada = true;
            Boolean echo = false;
            try
            {
                // Instanciamos el interfaz FishFace
                FishFace fish = new FishFace();
                fish.OpenInterface(Port.COM1);
                // Encendemos la lámpara
                fish.SetMotor(Nr.M1, Dir.On);
                Console.WriteLine("Iniciando el sistema...");
                // Esperamos un segundo para que se encienda la lámpara
                fish.Pause(1000);
                Console.WriteLine("Sistema iniciado.");
                while (true)
                {
                    luzDetectada = fish.GetInput(Nr.E1);
                    if (!luzDetectada)
                    { // Si no se detecta luz es porque hemos pasado la mano
                        fish.SetMotor(Nr.M2, Dir.On);
                        echo = !luzDetectada;
                    }
                    else if (echo)
                    {
                        // Si no se detecta la luz realizamos el retardo
                        fish.Pause(5000);
                        fish.SetMotor(Nr.M2, Dir.Off);
                        echo = false;
                    }
                }
            }
            catch (FishFaceException)
            {
                Console.WriteLine("ERROR:: No se pudo iniciar el interfaz.");
                Console.ReadLine();
            }
        }
    }
}
