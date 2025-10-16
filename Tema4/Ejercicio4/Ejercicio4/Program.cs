namespace Ejercicio4
{
    internal class Program
    {

        static void Main(string[] args)
        {


            int[] notas = { 5, 2, 8, 1, 9, 4 };

            //1
            bool aprobados = Array.Exists(notas, a => a >= 5);
            Console.WriteLine(aprobados);

            //2
            int[] mostrarAprobados = Array.FindAll(notas, a => a >= 5);
            foreach (int a in mostrarAprobados)
            {
                Console.WriteLine(a);

            }

            //3
            int ultimoAprobado = Array.FindLastIndex(notas, a => a >= 5);
            Console.WriteLine(ultimoAprobado);

            //4
            int ultimaNotaAprobado = Array.FindLast(notas, a => a >= 5);
            Console.WriteLine(ultimaNotaAprobado);

            //5
            int notaPar = notas.Count(a => a % 2 == 0);
            Console.WriteLine("Hay {0} alumnos que tienen nota par ", notaPar);

            string[] palabras = { "Sol", "Luna", "Estrella", "Cielo" };

            //1
            //int minimo = 3;
            //string[] palabra3 = Array.FindAll(palabras, a => a.Length > minimo);

            //Console.WriteLine("La primera palabra con mas de 3 caracteres: {0}",palabra3);

            //2
            //Array.ForEach(palabras, a => Console.WriteLine(a.ToUpper()));

            //3
            //string palabraE = Array.Find(palabras, a => a.StartsWith("E"));
            //Console.WriteLine(palabraE);










        }
    }
}
