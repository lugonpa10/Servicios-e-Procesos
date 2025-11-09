namespace Ejercicio2
{
    internal class Program
    {
        static int caballoGanador ;
        static bool carreraTerminada;
        static object l = new object();
        static int meta = 25;
        static Random rd = new Random();

        static void Main(string[] args)
        {
            int respuesta;
            do
            {
                caballoGanador = -1;
                carreraTerminada = false;
                Console.Clear();
                Console.WriteLine("Elige un caballo del 1 al 5");
                int caballoUsuario = pedirEntero(1, 5);

                Thread[] caballos = new Thread[5];
                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i] = new Thread(correCaballos);
                    caballos[i].Start(i + 1);
                }
                

                for (int i = 0; i < caballos.Length; i++)
                {
                    caballos[i].Join();
                }

                Console.SetCursorPosition(0, caballos.Length + 2);

                if (caballoUsuario == caballoGanador)
                {
                    Console.WriteLine("enhorabuena");
                }
                else
                {
                    Console.WriteLine($"lo siento,tu caballo era el {caballoUsuario}");
                }
                Console.WriteLine($"El caballo ganador es {caballoGanador}");

                Console.WriteLine("Quieres repetir el juego: 1 (Si) o 2 (No)");
                respuesta = pedirEntero(1, 2);



            } while (respuesta != 2);
        }

        static void correCaballos(object numCaballo)
        {

            int id = (int)numCaballo;


            int pos = 0;

            while (true)
            {
                int avance;
                int espera;

                lock (rd)
                {
                    avance = rd.Next(1, 5);
                    espera = rd.Next(50, 100);

                }

                pos += avance;
                if (pos > meta)
                {
                    pos = meta;
                }

                lock (l)
                {
                    if (carreraTerminada)
                    {
                        break;
                    }

                    Console.SetCursorPosition(0, id);

                    Console.Write($"Caballo {id} ");


                    for (int i = 0; i < pos; i++)
                    {
                        Console.Write("_");
                    }

                    Console.Write(">");


                    if (!carreraTerminada && pos >= meta)
                    {
                        carreraTerminada = true;
                        caballoGanador = id;
                    }


                }

                if (carreraTerminada)
                {
                    break;
                }
                Thread.Sleep(espera);




            }


        }

        static int pedirEntero(int min, int max)
        {
            while (true)
            {

                int num;
                bool flag = int.TryParse(Console.ReadLine(), out num);

                if (flag)
                {
                    if (num >= min && num <= max)
                    {

                        return num;

                    }
                    else
                    {
                        Console.WriteLine("Numero fuera de rango");
                    }
                }
                else
                {
                    Console.WriteLine("Caracter no valido");
                }
            }

        }

    }

}
