using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TemaNetworking
{
    public class EchoServer
    {

        // Se usaría para parar el servidor
        public bool ServerRunning { set; get; } = true;
        public int Port { get; set; } = 31416;
        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            using (Socket s = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(10);
                Console.WriteLine($"Servidor iniciado. " +
                $"Escuchando en {ie.Address}:{ie.Port}");
                Console.WriteLine("Esperando conexiones... (Ctrl+C para salir)");
                // Bucle para atender clientes de forma continua.
                while (ServerRunning)
                {
                    Socket client = s.Accept();
                    // Se crea hilo para atender el nuevo cliente.
                    Thread hilo = new Thread(() => ClientDispatcher(client));
                    // Aquí se podría configurar la prioridad y
                    // background si fuera necesario
                    // Se lanza el hilo y se espera a un nuevo cliente
                    hilo.Start();
                }
            }
        }

        private void ClientDispatcher(Socket sClient)
        {
            using (sClient)
            {
                IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado:{ieClient.Address} " +
                $"en puerto {ieClient.Port}");
                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamReader sr = new StreamReader(ns, codificacion))
                using (StreamWriter sw = new StreamWriter(ns, codificacion))
                {
                    sw.AutoFlush = true;
                    string welcome = "Welcome to The Echo-Logic, Odd, Desiderable, " +
                    "Incredible, and Javaless Echo Server (T.E.L.O.D.I.J.E Server)";
                    sw.WriteLine(welcome);
                    string? msg = "";
                    while (msg != null)
                    {
                        try
                        {
                            msg = sr.ReadLine();
                            if (msg != null)
                            {
                                Console.WriteLine($"El cliente dice {msg}");
                                Thread.Sleep(3000);
                                sw.WriteLine($"El servidor dice {msg}");
                            }
                        }
                        catch (IOException)
                        {
                            msg = null;
                        }
                    }
                    Console.WriteLine("Cliente desconectado.\nConexión cerrada");
                }
            }
        }
    }
}

