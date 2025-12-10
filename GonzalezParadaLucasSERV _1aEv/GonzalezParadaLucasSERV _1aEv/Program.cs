namespace GonzalezParadaLucasSERV__1aEv
{
    internal class Program
    {
        static int NCebollas;
        static int NPatatas;
        static int NTortillas;
        static readonly object l = new object();
        static bool cocinar = true;
        static readonly Random rd = new Random();

        static void Tortilla()
        {
            while (cocinar)
            {
                lock (l)
                {
                    if (NCebollas >= 5 && NPatatas >= 5)
                    {
                        NCebollas -= 5;
                        NPatatas -= 5;
                        NTortillas++;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Tortillas{NTortillas,3}\tPatatas{NPatatas,3}\tCebollas{NCebollas,3}");
                        if (NTortillas == 10)
                        {
                            cocinar = false;
                        }
                    }
                }
            }
        }

        static void Ingrediente(string ingrediente)
        {
            
            while (cocinar)
            {
                lock (l)
                {
                    if (cocinar)
                    {
                        if (ingrediente == "Cebolla")
                        {
                            NCebollas++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{ingrediente}: {NCebollas}");
                        }
                        else
                        {
                            NPatatas++;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{ingrediente}: {NPatatas}");
                        }
                    }

                }
                if(rd.Next(2) == 0)
                {
                    Thread.Sleep(50);
                }
            }

        }
        static void Main(string[] args)
        {
            Thread hiloCebollas = new Thread(() => Ingrediente("Cebolla"));
            Thread hiloPatatas = new Thread(() => Ingrediente("Patata"));
            Thread hiloTortilla = new Thread(Tortilla);
            hiloCebollas.Start();
            hiloPatatas.Start();
            hiloTortilla.Start();
            hiloCebollas.Join();
            hiloPatatas.Join();
            hiloTortilla.Join();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Han sobrado {NCebollas} cebollas y {NPatatas} patatas");
        }
    }
}
