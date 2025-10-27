using System;

namespace Hilos1
{
    internal class Program
    {


        static object l = new object();
        static void Main(string[] args) // Hilo principal, foreground por defecto.
        {
            Thread thread = new Thread(writeDown);
            thread.IsBackground = true; // Punto clave. Prueba a cambiarlo a false.
            thread.Start("Mensaje"); // los parametros se ponen en start
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 1);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(50);
            }
        } // Cuando acaba el programa se cierra interrumpiendo el hilo background.
        static void writeDown(object o) // los parametros tienen que ser object 
        {
            string mensaje = (string)o;
            for (int i = 1; i < 50; i++)
            {
                lock (l)
                {
                    Console.SetCursorPosition(1, 20);
                    Console.Write("{0,4}", i);
                }
                Thread.Sleep(200);
            }
        }

        //static bool running = true;
        //static readonly object l = new();
        //static void Main(string[] args)
        //{
        //    Thread threadA = new Thread(charA);
        //    Thread threadB = new Thread(charB);
        //    threadA.Start();
        //    threadB.Start();

        //    threadA.Join(100); // cuando acabe el hilo A se escribe lo que hay debajo
        //    Console.WriteLine("Cuando se ejecuta esto?");
        //    Console.ReadKey();
        //}

        //static void charA()
        //{
        //    int contA = 1;
        //    while (running)
        //    {
        //        lock (l)
        //        {
        //            if (running)
        //            {

        //                Console.ForegroundColor = ConsoleColor.Green;
        //                Console.Write($" A:{contA}");
        //                contA++;
        //                if (contA > 1000)
        //                {
        //                    running = false;
        //                }
        //            }
        //        }
        //                //Thread.Sleep(500);
        //    }
        //}
        //static void charB()
        //{
        //    int contB = 1;
        //    while (running)
        //    {

        //        lock (l)
        //        {
        //            if (running) // si esta booleana esta a false no ejecuta el resto del codigo, por lo tanto no hay una ejecucion mas de ningun hilo
        //            {


        //                Console.ForegroundColor = ConsoleColor.Red;
        //                Console.Write($" B:{contB}");
        //                contB++;
        //                if (contB > 1000)
        //                {
        //                    running = false;
        //                }

        //            }
        //        }
        //                //Thread.Sleep(500);

        //    }
        //}
        ////static void Main(string[] args)
        ////{
        ////    Thread thread = new Thread(charA);// crear un objeto tipo hilo, no se llama a la funcion (charA())
        ////    thread.Priority = ThreadPriority.Highest;
        ////    thread.Start();
        ////    //char B
        ////    for (int i = 1; i < 1000; i++)
        ////    {
        ////        Console.Write('B');
        ////    }

        ////    Console.ReadKey();
        ////}
        ////static void charA()
        ////{
        ////    for (int i = 1; i < 1000; i++)
        ////    {
        ////        Console.Write('A');
        ////    }

        //static bool running = true; // Booleana compartida para controlar los bucles

        //static public readonly object l = new object(); // readonly se utiliza por seguridad para que el testigo no pueda ser null
        //static void Main(string[] args)
        //{
        //    Thread thread = new Thread(writeDown);
        //    thread.Start();
        //    // writeUp
        //    int i = 1;
        //    while (running)
        //    {
        //        lock (l) // si en algun momento este hilo sale de la CPU, el otro hilo queda en espera hasta que se ejecute el codigo entre llaves.
        //        {

        //            Console.SetCursorPosition(1, 1);
        //            Console.Write("{0,4}", i);
        //            i++;
        //            Thread.Sleep(10);
        //            if (i >= 1000)
        //            {
        //                running = false;
        //            }
        //        }
        //    }
        //    Console.ReadKey();
        //}
        //static void writeDown()
        //{
        //    int i = 1;
        //    while (running)
        //    {
        //        lock (l)
        //        {

        //            Console.SetCursorPosition(1, 20);
        //            Console.Write("{0,4}", i);
        //            i++;
        //            Thread.Sleep(10);

        //            if (i >= 1000)
        //            {
        //                running = false;
        //            }
        //        }
        //    }
        //}
    }
}
