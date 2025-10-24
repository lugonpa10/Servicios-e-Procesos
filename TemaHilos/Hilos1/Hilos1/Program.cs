namespace Hilos1
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Thread thread = new Thread(charA);// crear un objeto tipo hilo, no se llama a la funcion (charA())
        //    thread.Priority = ThreadPriority.Highest;
        //    thread.Start();
        //    //char B
        //    for (int i = 1; i < 1000; i++)
        //    {
        //        Console.Write('B');
        //    }

        //    Console.ReadKey();
        //}
        //static void charA()
        //{
        //    for (int i = 1; i < 1000; i++)
        //    {
        //        Console.Write('A');
        //    }

        static bool running = true; // Booleana compartida para controlar los bucles

        static public readonly object l = new object();
        static void Main(string[] args)
        {
            Thread thread = new Thread(writeDown);
            thread.Start();
            // writeUp
            int i = 1;
            while (running)
            {
                lock (l)
                {

                    Console.SetCursorPosition(1, 1);
                    Console.Write("{0,4}", i);
                    i++;
                    Thread.Sleep(10);
                    if (i >= 1000)
                    {
                        running = false;
                    }
                }
            }
            Console.ReadKey();
        }
        static void writeDown()
        {
            int i = 1;
            while (running)
            {
                lock (l)
                {

                    Console.SetCursorPosition(1, 20);
                    Console.Write("{0,4}", i);
                    i++;
                    Thread.Sleep(10);

                    if (i >= 1000)
                    {
                        running = false;
                    }
                }
            }
        }
    }
}
