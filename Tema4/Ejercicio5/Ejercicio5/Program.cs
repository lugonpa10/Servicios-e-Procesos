namespace Ejercicio5
{
    internal class Program
    {

        public delegate void MyDelegate();
        static void Main(string[] args)
        {
            //string[] opciones = { "A", "B", "C", "Exit" };


            MenuGenerator(new string[] { "Op1", "Op2", "Op3" }, new MyDelegate[] { f1, f2, f3 });




        }


        public static void MenuGenerator(string[] opciones, MyDelegate[] delegado)
        {
            int opcion;
            if (opciones.Length != delegado.Length)
            {
                Console.WriteLine("Los vectores tienen longitudes distintas");
            }
            if (opciones == null || delegado == null)
            {
                Console.WriteLine("Algun parametro es invalido");
            }


            Console.WriteLine("Opciones: ");

            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine(opciones[i]);
            }

            Console.WriteLine($"{opciones.Length + 1} Salir" );
            opcion = PedirEnteros();

            if (opcion < opciones.Length && opcion >= 1)
            {
                
                    delegado[opcion - 1]();
                  
            }

            static void f1()
            {
                Console.WriteLine("A");
            }
            static void f2()
            {
                Console.WriteLine("B");
            }
            static void f3()
            {
                Console.WriteLine("C");
            }

        }
        public static int PedirEnteros()
        {
            int num;
            bool flag = true;

            do
            {
                Console.WriteLine($"Introduce una opcion");
                flag = int.TryParse(Console.ReadLine(), out num);
                if (!flag)
                {
                    Console.WriteLine("Argumento invalido");

                }
                
            }
            while (!flag);


            return num;
        }



    }
}
