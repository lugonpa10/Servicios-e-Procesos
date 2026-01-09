using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    internal class FechaHora
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { set; get; } = 31416;

        public void initServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    s.Bind(ie);
                    Console.WriteLine($"Puerto {Port} libre");
                    s.Listen(10);

                }
                catch (SocketException e) when (e.ErrorCode == 10048)
                {
                    Console.WriteLine($"Puerto {Port} en uso");
                }
                catch (SocketException e)
                {
                    Console.WriteLine($"Error: {e.SocketErrorCode} - {e.Message}");
                }
         ;
                Console.WriteLine($"Se ha iniciado el servidor"
                    + $" Escuchando en {ie.Address}:{ie.Port}");

                while (ServerRunning)
                {
                    Socket client = s.Accept();
                    Thread hilo = new Thread(() => ClientDispatcher(client));
                    hilo.Start();


                }
            }

        }


        private void ClientDispatcher(Socket sClient)
        {
            using (sClient)
            {
                IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado: {ieClient.Address}" +
                    $"en puerto {ieClient.Port}");

                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamWriter sw = new StreamWriter(ns))
                using (StreamReader sr = new StreamReader(ns))
                {
                    sw.AutoFlush = true;
                    string? opcion = "";
                    sw.WriteLine("Bienvenido a mi servidor,introduce un comando");
                    while (opcion != null)
                    {
                        try
                        {
                            opcion = sr.ReadLine();
                            if (opcion != null)
                            {

                                sw.WriteLine("Introduce otro comando: ");
                                switch (opcion)
                                {
                                    case "time":
                                        sw.WriteLine(DateTime.Now.ToString("T"));
                                        break;
                                    case "date":
                                        sw.WriteLine(DateTime.Now.ToString("d"));
                                        break;

                                    case "all":
                                        sw.WriteLine(DateTime.Now.ToString("G"));
                                        break;

                                    case "close":
                                        sw.WriteLine("Introduce la contraseña");
                                        int password;



                                        break;
                                }


                            }

                        }
                        catch (IOException)
                        {
                            opcion = null;

                        }

                    }
                    Console.WriteLine("El cliente ha cerrado la conexion");







                }
            }
        }


    }
}
