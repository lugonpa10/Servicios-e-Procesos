using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    internal class FechaHora
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { set; get; } = 31416;
        private Socket s;

        public void initServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Bind(ie);
                Console.WriteLine($"Puerto {Port} libre");
                s.Listen(10);
                Console.WriteLine($"Se ha iniciado el servidor"
                    + $" Escuchando en {ie.Address}:{ie.Port}");

                while (ServerRunning)
                {
                    Socket client = s.Accept();
                    Thread hilo = new Thread(() => ClientDispatcher(client));
                    hilo.Start();


                }

            }
            catch (SocketException e) when (e.ErrorCode == 10048)
            {
                Console.WriteLine($"Puerto {Port} en uso");
            }
            catch (SocketException)
            {

                Console.WriteLine("Fin del servidor");

            }

        }
        private void ClientDispatcher(Socket sClient)
        {
            using (sClient)
            {
                IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado: {ieClient.Address}" +
                    $" en puerto {ieClient.Port}");

                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamWriter sw = new StreamWriter(ns))
                using (StreamReader sr = new StreamReader(ns))
                {
                    sw.AutoFlush = true;

                    string? opcion = "";
                    sw.WriteLine("Bienvenido a mi servidor,introduce un comando");
                    {
                        try
                        {
                            opcion = sr.ReadLine();
                            if (opcion != null && opcion.StartsWith("close"))
                            {
                                string programData = Environment.GetEnvironmentVariable("PROGRAMDATA");
                                string archivo = "password.txt";
                                string rutaArchivo = programData + "\\" + archivo;
                                string contraseñaCorrecta;
                                try
                                {
                                    using (StreamReader sr2 = new StreamReader(rutaArchivo))
                                    {
                                        contraseñaCorrecta = sr2.ReadLine().Trim();

                                    }
                                    string[] contraseña = opcion.Split(" ");

                                    if (contraseña.Length < 2)
                                    {
                                        sw.WriteLine("No se ha enviado la contrasenha");
                                    }
                                    else if (contraseña[1] != contraseñaCorrecta)
                                    {
                                        sw.WriteLine("La contrasenha es incorrecta");

                                    }
                                    else
                                    {
                                        sw.WriteLine("Contrasenha Correcta");
                                        StopServer();
                                    }
                                }
                                catch (FileNotFoundException e)
                                {
                                    sw.WriteLine($"No se encontro el archivo: '{e}'");
                                }
                                catch (UnauthorizedAccessException e)
                                {
                                    sw.WriteLine($"No tienes los permisos necesarios: '{e}'");
                                }
                                catch (DirectoryNotFoundException e)
                                {
                                    sw.WriteLine($"No se pudo encontrar el directorio: '{e}'");
                                }
                                catch (IOException e)
                                {
                                    sw.WriteLine($"No se pudo abrir el archivo: '{e}'");
                                }
                            }
                            else if (opcion != "time" && opcion != "date" && opcion != "all")
                            {
                                sw.WriteLine("El comando no es valido");
                                StopServer();
                            }
                            else
                            {
                                switch (opcion)
                                {
                                    case "time":
                                        sw.WriteLine(DateTime.Now.ToString("T"));
                                        StopServer();

                                        break;
                                    case "date":
                                        sw.WriteLine(DateTime.Now.ToString("d"));
                                        StopServer();

                                        break;

                                    case "all":
                                        sw.WriteLine(DateTime.Now.ToString("G"));
                                        StopServer();

                                        break;
                                }

                            }
                        }
                        catch (IOException)
                        {
                            opcion = null;
                        }
                    }
                }
            }
        }
        public void StopServer()
        {
            Console.WriteLine("Deteniendo servidor");
            ServerRunning = false;
            s.Close();

        }


    }
}
