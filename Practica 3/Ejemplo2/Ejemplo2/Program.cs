using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo2
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean hayEntrada = false;
            // Creamos una instancia de la clase FishFace
            FishFa30.FishFace fish = new FishFa30.FishFace();
            try
            {
                // Abrimos la conexion con el interfaz
                fish.OpenInterface(FishFa30.Port.COM1);
                // Esperamos una señal por parte del sensor conectado a la entrada digital E1
                while (!hayEntrada)
                {
                    hayEntrada = fish.GetInput(FishFa30.Nr.E1);
                }
                // Una vez que se ha activado el interruptor...
                if (hayEntrada)
                {
                    // Activamos el actuador conectado a la salida digital M1
                    fish.SetMotor(FishFa30.Nr.M1, FishFa30.Dir.On);
                    // Esperamos 3000 ms
                    fish.Pause(3000);
                    // Paramos el actuador conectado a la salida digital M1
                    fish.SetMotor(FishFa30.Nr.M1, FishFa30.Dir.Off);
                }
                // Cerramos la conexion con el interfaz
                fish.CloseInterface();
                Console.ReadLine();

            }
            catch (FishFa30.FishFaceException)
            {
                Console.WriteLine("ERROR:: No se ha podido abrir la interfaz");
                Console.ReadLine();
            }
        }
    }
}
