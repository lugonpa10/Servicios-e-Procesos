namespace Ejercicio1
{
    internal class Program
    {
        static int cont = 0;
        static bool flag = true;
        static readonly object l = new object();
        static void Main(string[] args)
        {
            Thread th1 = new Thread(() =>
                {

                    while (flag)
                    {

                        lock (l)
                        {
                            if (!flag)
                            {
                                break;
                            }
                            cont++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{cont}\t");
                            Console.ResetColor();

                            if (cont >= 500)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                });

            Thread th2 = new Thread(() =>
            {
                while (flag)
                {
                    lock (l)
                    {
                        if (!flag)
                        {
                            break;
                        }
                        cont--;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{cont}\t");
                        Console.ResetColor();
                        if (cont <= -500)
                        {
                            flag = false;
                            break;
                        }


                    }


                }
            });
            th1.Start();
            th2.Start();
            //th1.Join();
            th2.Join();
            if (cont >= 500)
            {
                Console.WriteLine("Ganador hilo 1");
            }
            else if (cont <= -500)
            {
                Console.WriteLine("Ganador hilo 2");
            }
        }
    }
}

