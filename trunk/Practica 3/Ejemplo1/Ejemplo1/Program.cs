using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using FishFa30;

namespace Ejemplo1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creamos una instancia de la clase FishFace
            FishFa30.FishFace fish = new FishFa30.FishFace();
            try
            {
                // Abrimos la conexion con interfaz
                fish.OpenInterface(FishFa30.Port.COM1);
                // Activamos el actuador conectado a la salida M2.
                fish.SetMotor(FishFa30.Nr.M2, FishFa30.Dir.On);
                // Activamos el actuador conectado a la salida M1 a la Derecha
                fish.SetMotor(FishFa30.Nr.M1, FishFa30.Dir.Right);
                // Realizamos un wait para esperar un tiempo, en este caso 2000
                fish.WaitForTime(2000);
                // Desactivamos los actuadores conectados a las salidas M1 y M2
                fish.SetMotor(FishFa30.Nr.M1, FishFa30.Dir.Off);
                fish.SetMotor(FishFa30.Nr.M2, FishFa30.Dir.Off);
                // Por ultimo cerramos la conexion con la interfaz
                fish.CloseInterface();
            }
            catch (FishFa30.FishFaceException)
            {
                Console.WriteLine("ERROR:: No se ha podido abrir la interfaz");
                Console.ReadLine();
            }
        }
    }
}
