using System;
using System.Diagnostics.Contracts;
using System.Security.AccessControl;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ejercicio7
{
    public class Principal
    {
         public static   List<Astro> astros = new List<Astro>();
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.WriteLine("1- Añade planeta");
                Console.WriteLine("2-Añade Cometa");
                Console.WriteLine("3-Mostrar datos");
                Console.WriteLine("4-Incrementa/Decrementa n.º de satélites");
                Console.WriteLine("5-Eliminar no terraformables");
                Console.WriteLine("6-Salir");

                opcion = PedirEnteroRango(1, 6);


                switch (opcion)
                {
                    case 1:

                        Planeta planeta = new Planeta();

                        planeta.Nombre = PedirNombre();
                        planeta.Radio = PedirRadio();
                        planeta.Gaseoso = Gaseoso();
                        planeta.NumSatelites = CantLunas();

                        astros.Add(planeta);
                        break;
                    case 2:

                        Cometa cometa = new Cometa();

                        cometa.Nombre = PedirNombre();
                        cometa.Radio = PedirRadio();

                        astros.Add(cometa);
                        break;
                    case 3:
                        MostrarDatos(astros);
                        break;

                    case 4:
                        SumaRestaSatelites(astros);

                        break;

                    case 5:

                        EliminaAstros(astros);

                        break;

                    case 6:
                        Console.WriteLine("Adios");
                        guardarCol(astros);
                        break;
                }

            }
            while (opcion != 6);
        }




        static bool Gaseoso()
        {
            bool gas = false;
            Console.WriteLine("es gaseoso?");
            string respuesta = Console.ReadLine();

            if (respuesta == "si")
            {
                gas = true;
            }
            return gas;
        }


        static string PedirNombre()
        {


            Console.WriteLine("Introduce el nombre");
            return Console.ReadLine();



        }

        static double PedirRadio()
        {

            do
            {


                try
                {

                    Console.WriteLine("Introduce el radio");

                    double radio = Convert.ToDouble(Console.ReadLine());
                    return radio;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Ha introducido un valor no entero");


                }
                catch (OverflowException)
                {
                    Console.WriteLine("Numero invalido");


                }

            }
            while (true);

        }

        static int CantLunas()
        {
            do
            {
                bool error;
                try
                {
                    error = false;
                    Console.WriteLine("Introduce la cantidad de lunas");
                    int satelites = Convert.ToInt32(Console.ReadLine());
                    return satelites;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ha introducido un valor no entero");
                    error = true;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Numero invalido");
                    error = true;

                }
            }
            while (true);


        }
        static void MostrarDatos(List<Astro> astros)
        {


            for (int i = 0; i < astros.Count; i++)
            {
                if (astros[i].GetType() == typeof(Planeta))
                {

                    Console.WriteLine(((Planeta)astros[i]).ToString());

                    if (((Planeta)astros[i]).EsHabitable())
                    {
                        Console.WriteLine("Es habitable");
                    }
                    else
                    {
                        Console.WriteLine("No es habitable");
                    }
                }

                if (astros[i].GetType() == typeof(Cometa))
                {
                    Console.WriteLine(((Cometa)astros[i]).Nombre);
                    Console.WriteLine("No es habitable");
                }

            }

        }

        static void SumaRestaSatelites(List<Astro> astros)
        {
            Planeta planeta = new Planeta();

            // planeta = planeta;
            Console.WriteLine("Introduce el nombre del Planeta");
            planeta.Nombre = Console.ReadLine();
            int respuesta = 0;

            for (int i = 0; i < astros.Count; i++)//TODO sin bucle
            {
                if (astros.IndexOf(astros[i]) != -1)
                {
                    if (astros[i] is Planeta planeta1 && astros[i].Equals(planeta.Nombre))
                    {
                        Console.WriteLine("Quieres aumentar (1) o decrementar (2)?");

                        respuesta = PedirEnteroRango(1, 2);

                        if (respuesta == 1)
                        {
                            //     planeta1 = astros[i];// ++;
                            planeta1++;
                            Console.WriteLine("Se ha sumado 1 satelite");
                        }
                        else
                        {
                            planeta1--;//TODO arregla
                                       //  astros[i] = planeta1;
                            Console.WriteLine("Se ha restado 1 satelite");

                        }
                    }
                }
                else
                {
                    Console.WriteLine("El elemento no existe");
                }








            }




        }


        static void EliminaAstros(List<Astro> astros)
        {
            for (int i = astros.Count - 1; i >= 0; i--)
            {
                //TODO uso de interface
                if (astros[i].GetType() == typeof(Cometa))
                {


                    astros.RemoveAt(i);

                }

                else if (astros[i].GetType() == typeof(Planeta))
                {
                    if (!((Planeta)astros[i]).EsHabitable())
                    {
                        Console.WriteLine("Los planetas no habitables son {0}", astros[i]);

                        astros.RemoveAt(i);
                    }
                }





            }

        }


        static int PedirEntero()
        {
            bool flag = true;
            int num;
            do
            {
                Console.WriteLine("Introduce un numero");
                flag = int.TryParse(Console.ReadLine(), out num);

            }
            while (flag);



            return num;

        }

        static int PedirEnteroRango(int min, int max)
        {
            bool flag = true;
            int num;
            do
            {
                Console.WriteLine("Introduce una opcion entre {0} y {1}", min, max);
                flag = int.TryParse(Console.ReadLine(), out num);
                if (num < min || num > max)
                {
                    Console.WriteLine("Numero fuera de rango");
                    flag = false;
                }

            }
            while (!flag);



            return num;
        }


        public static void leerCol(string nombreArchivo)
        {

            using (StreamReader sr = new StreamReader(nombreArchivo))
            {
                sr.ReadLine();
            }

        }

        public static void guardarCol(List<Astro> astros)
        {
            try
            {
                foreach (Astro a in astros)
                {
                    string appdata = Environment.GetEnvironmentVariable("appdata");
                    string direccion = appdata + "//astros.txt";

                    using (StreamWriter sw = new StreamWriter(direccion))
                    {
                        sw.WriteLine(a.ToString());
                    }
                }



            }catch(IOException)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
