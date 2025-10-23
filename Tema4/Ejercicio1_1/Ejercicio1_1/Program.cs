namespace Ejercicio1_1

{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ls(args);//TODO comprobaciones sobre args
              //Ejercicio1_2.cat(args);
             Ejercicio1_3.NewFile(args);




        }


        public static void ls(string[] args)//TODO control excep
        {



            if (args.Length == 0)
            {
                Console.WriteLine("No se ha pasado ningun argumento");
            }

            if (args.Length == 1)
            {

                DirectoryInfo d = new(args[0]);


                if (!Directory.Exists(args[0]))
                {

                    Console.WriteLine("El directorio no existe {0}", args);
                    return;



                }

                Console.WriteLine($"Subdirectorios de {d.Name}");

                //TODO color de los primeros elementos

                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t{dir.Name}");
                }
                Console.ResetColor();

                Console.WriteLine($"Archivos:");
                foreach (var archivos in d.GetFiles())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\t{archivos.Name}\t{archivos.Length}");
                }
                Console.ResetColor();

            }







        }
    }
}
