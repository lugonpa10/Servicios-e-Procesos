using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_1
{
    internal class Ejercicio1_3
    {



        public static void NewFile(string[] args)//TODO control excep
        {
            string archivo;

            if (args.Length == 2)
            {
                archivo = args[0];
                StreamWriter s = new(archivo);//TODO using
                s.WriteLine(args[1]);
                s.Close();
                Console.WriteLine($"Se ha creado el archivo {archivo}");




            }
            else if (args.Length == 3 && args[0].StartsWith("-a"))//TODO igual a -a 
            {

                archivo = args[1];
                StreamWriter s = new(archivo, true);
                s.WriteLine(args[2]);
                s.Close();
                Console.WriteLine($"Se ha sobreescrito el archivo {archivo}");

            }





        }
    }
}
