using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_1
{
    internal class Ejercicio1_2
    {
        public static void cat(string[] args)
        {

            int cont = 0;
            string archivo;

            if (args.Length == 0)
            {
                Console.WriteLine("No se ha pasado ningun argumento");
            }

            if (args.Length == 1) 
            {


                archivo = args[0];

                try
                {

                    using (StreamReader sr = new(archivo))
                    {

                        Console.WriteLine(sr.ReadToEnd());
                    }

                }
                catch (FileNotFoundException e)//permisos
                {
                    Console.WriteLine($"No se encontro el archivo: '{e}'");
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine($"No se encontro el directorio: '{e}'");
                }
                catch (IOException e)
                {
                    Console.WriteLine($"No se pudo abrir el archivo: '{e}'");
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine($"No tienes los permisos necesarios: '{e}'");

                }


            }

            else if (args.Length == 2 && args[0].StartsWith("-n"))
            {
                bool bandera;
                int n;



                string num = args[0].Substring(2);
                bandera = int.TryParse(num, out n); 
                archivo = args[1];

                if (!bandera)
                {
                    Console.WriteLine("No es un numero");
                    return;
                }


                try
                {

                    using (StreamReader sr = new(archivo))
                    {

                        string linea;
                        linea = sr.ReadLine();

                        while (cont < n && linea != null)
                        {
                            Console.WriteLine(linea);
                            linea = sr.ReadLine();
                            cont++;



                        }

                    }
                }
                catch (FileNotFoundException e)
                {

                    Console.WriteLine($"No se encontro: '{e}'");


                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine($"No se encontro el directorio: '{e}'");


                }
                catch (IOException e)
                {

                    Console.WriteLine($"No se pudo abrir el archivo: '{e}'");

                }




            }
        }
    }
}
