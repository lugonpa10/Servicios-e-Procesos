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
            int tope;
            int cont = 0;
            string archivo;


            if (args.Length == 1)//TODO control excep
            {

                
                archivo = args[0];
                using (StreamReader sr = new(archivo))
                {

                    //Console.WriteLine(sr.ReadToEnd());
                }


            }

            else if (args.Length == 2 && args[0].StartsWith("-n")) {

                string num = args[0].Substring(2);
                tope = int.Parse(num);//TODO comprobar que es nº
                archivo= args[1];

                using (StreamReader sr = new(archivo))
                {

                    string linea;
                    linea = sr.ReadLine();

                    while (tope > cont && linea!= null)
                    {
                      Console.WriteLine(linea);
                        linea = sr.ReadLine();
                        cont++;



                    }

                }


            }
        }
    }
}
