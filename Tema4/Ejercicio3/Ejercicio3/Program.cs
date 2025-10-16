namespace Ejercicio3
{
    internal class Program
    {

        public delegate double Operaciones(double a);

        static void Main(string[] args)
        {

            Operaciones op = (a) => a * a;
            int res;
            int num;
            Console.WriteLine("quieres hacer el cuadrado(1) o el cubo(2) de un numero?");
            res = PedirEnteroRango(1, 2);
            if (res == 2)
            {
                op = (a) => a * a * a;
            }
            Console.WriteLine("prueba git");

            Console.Write("Introduce el numero: ");
            num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Resultado: {0}", op(num));




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
    }
}
