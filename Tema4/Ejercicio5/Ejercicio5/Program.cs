namespace Ejercicio5
{
    internal class Program
    {

        public delegate void MyDelegate();
        static void Main(string[] args)
        {
            //string[] opciones = { "A", "B", "C", "Exit" };
            PedirEnteros(1, 4);
            MenuGenerator(new string[] { "Op1", "Op2", "Op3" }, new MyDelegate[] { f1, f2, f3 });
            Console.ReadKey();


        }


        public static void MenuGenerator(string[] opciones, MyDelegate[] delegado)
        {

            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine(opciones[i]);
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

        public static int PedirEnteros(int min, int max)
        {
            int num;
            bool flag = true;

            do
            {
                Console.WriteLine($"Introduce una opcion entre {min} y {max}");
                flag = int.TryParse(Console.ReadLine(), out num);
                if (!flag)
                {
                    Console.WriteLine("Argumento invalido");

                }
                else if (num < min || num > max)
                {
                    Console.WriteLine("Numero fuera de rango");
                }
            }
            while (!flag || (num < min || num > max));


            return num;
        }



    }
}
