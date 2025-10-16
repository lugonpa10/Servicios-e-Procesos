namespace Ejercicio1_1

{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ls(args[0]);//TODO comprobaciones sobre args
          //  Ejercicio1_2.cat(args);
           // Ejercicio1_3.NewFile(args);




        }


        public static void ls(string[] args)//TODO control excep
        {


            if (args.Length > 1)
            {
                
            }

            DirectoryInfo d = new (cadena);
       

            if (!Directory.Exists(cadena))
            {

                Console.WriteLine("El directorio no existe {0}", cadena);
                return;
                


            }

            Console.WriteLine($"Subdirectorios de {d.Name}");
            
            //TODO color de los primeros elementos

            foreach (DirectoryInfo dir in d.GetDirectories())
            {
                Console.WriteLine($"\t{dir.Name}");
                Console.ForegroundColor = ConsoleColor.Red;


            }
            Console.ResetColor();

            Console.WriteLine($"Archivos:");
            foreach (var archivos in d.GetFiles())
            {
                Console.WriteLine($"\t{archivos.Name}\t{archivos.Length}");
                Console.ForegroundColor = ConsoleColor.Blue;






            }


            Console.ResetColor();





        }
    }
}
