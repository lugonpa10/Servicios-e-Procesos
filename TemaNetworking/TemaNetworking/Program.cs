using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TemaNetworking
{
    internal class Program
    {
        //static void Main(string[] args)
        //{


        //    IPEndPoint ie = new IPEndPoint(IPAddress.Any, 31416);
        //    // Creacion del Socket. Un socket es IDisposable,
        //    // por tanto con using para cierre automático.
        //    using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
        //    ProtocolType.Tcp))
        //    {
        //        // Enlace de socket al puerto (y en cualquier interfaz de red)
        //        // Ojo: Habría que realizar comprobación de puerto ocupado
        //        // Según se vio previamente
        //        s.Bind(ie);
        //        // Esperando una conexión y estableciendo cola de clientes pendientes
        //        s.Listen(10);
        //        // Información de depuración
        //        Console.WriteLine($"Servidor iniciado. Escuchando en {ie.Address}:{ie.Port}");
        //        Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
        //        Socket sClient = s.Accept();
        //        using (sClient) // Este using por separado pues luego irá en un hilo
        //        {
        //            // Indicamos mediante sobrecarga la codificación de la consola.
        //            // Por defecto c# trabaja con Encoding.UTF8
        //            Encoding codificacion = Console.OutputEncoding;
        //            using (NetworkStream ns = new NetworkStream(sClient))
        //            using (StreamReader sr = new StreamReader(ns, codificacion))
        //            using (StreamWriter sw = new StreamWriter(ns, codificacion))
        //            {
        //                sw.AutoFlush = true; // Flush automático.
        //                string welcome = "Welcome to The Echo-Logic, Odd, Desiderable, " +
        //                "Incredible, and Javaless Echo Server (T.E.L.O.D.I.J.E Server)";
        //                sw.WriteLine(welcome);
        //                string? msg = "";
        //                while (msg != null)
        //                {
        //                    try
        //                    {
        //                        // Leemos el mensaje del cliente
        //                        msg = sr.ReadLine();
        //                        // Si el cliente manda mensaje se le envía de vuelta
        //                        // pero si manda null es que ha desconectado.
        //                        if (msg != null)
        //                        {
        //                            Console.WriteLine($"El cliente dice {msg}");
        //                            sw.WriteLine($"El servidor dice {msg}");
        //                        }
        //                    }
        //                    // Si se cierra el cliente, salta excepción
        //                    // Al siguiente readline que será null
        //                    catch (IOException)
        //                    {
        //                        msg = null;
        //                    }
        //                }
        //                Console.WriteLine("Cliente desconectado.\nConexión cerrada");
        //            }
        //        }
        //    }

        static void Main(string[] args)
        {
            (new EchoServer()).InitServer();
        }
    }
}
// Esperamos y aceptamos la conexion del cliente (socket bloqueante)
// Luego se hará en bucle para aceptar varios clientes


