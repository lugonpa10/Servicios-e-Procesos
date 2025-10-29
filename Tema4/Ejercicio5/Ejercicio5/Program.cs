namespace Ejercicio5
{
    internal class Program
    {

        public delegate void MyDelegate();
        static void Main(string[] args)
        {
            string[] opciones = { "A", "B", "C", "Exit" };
            MenuGenerator(opciones);

        }


        public static void MenuGenerator(string[] opciones)
        {

            int opcion = 0;

            do
            {
                Console.WriteLine("Introduce una opcion");
                opcion = int.Parse(Console.ReadLine());
                for (int i = 0; i < opciones.Length; i++)
                {
                    Console.WriteLine(opciones[i]);
                }


                switch (opcion)

                {
                    case 1:

                        static void f1()
                        {
                            Console.WriteLine("A");
                        }
                        break;

                    case 2:

                        static void f2()
                        {
                            Console.WriteLine("B");
                        }
                        break;

                    case 3:

                        static void f3()
                        {
                            Console.WriteLine("C");
                        }
                        break;
                }
            }
            while (opcion != 4);




        }



    }
}
