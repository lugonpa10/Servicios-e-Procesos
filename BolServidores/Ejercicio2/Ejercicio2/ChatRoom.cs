using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class ChatRoom
    {
        public bool ServerRunning { set; get; } = true;
        public int Port { set; get; } = 31416;
        public int[] puertosAlternativos = { 135, 135, 135, 234 };
        public bool puertoOcupado = true;
        private Socket s;
        private int i = 0;
        public List<Cliente> clientes = new List<Cliente>();
        static object l = new object();


        public void initServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                while (puertoOcupado)
                {
                    try
                    {
                        puertoOcupado = false;
                        s.Bind(ie);

                    }
                    catch (SocketException e) when (e.ErrorCode == 10048)
                    {
                        puertoOcupado = true;
                        ie.Port = puertosAlternativos[i];
                        i++;
                    }
                }
                Console.WriteLine($"El puerto {ie.Port} esta libre");
                s.Listen(10);
                Console.WriteLine($"Se ha iniciado el servidor" + $" Escuchando en {ie.Address}:{ie.Port}");
                while (ServerRunning)
                {
                    Socket client = s.Accept();
                    Thread hilo = new Thread(() => ClientDispatcher(client));
                    hilo.Start();

                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Puertos ocupados");
            }
            catch (SocketException e) when (e.ErrorCode == 10048)
            {
                Console.WriteLine($"Puerto {Port} en uso ");
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
                IPEndPoint ie = (IPEndPoint)sClient.RemoteEndPoint;
                Console.WriteLine($"Cliente conectado desde {ie.Address} en puerto {ie.Port}");

                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.AutoFlush = true;
                    string nombreUsuario;
                    bool clienteConectado = true;

                    sw.WriteLine("Bienvenido al chat,introduce tu nombre de usuario");

                    nombreUsuario = sr.ReadLine();

                    Cliente nuevoCliente = null;



                    if (nombreUsuario == null)
                    {
                        clienteConectado = false;

                    }
                    else if (nombreUsuario == "" || nombreUsuario == " ")
                    {
                        sw.WriteLine("Nombre de usuario no valido");
                        clienteConectado = false;
                    }
                    if (clienteConectado)
                    {
                        sw.WriteLine($"Hola {nombreUsuario},puedes empezar a chatear");
                        nuevoCliente = new Cliente(nombreUsuario, ie.Address, sw);
                        lock (l)
                        {

                            clientes.Add(nuevoCliente);
                            for (int i = clientes.Count - 1; i >= 0; i--)
                            {

                                try
                                {
                                    if (clientes[i].Sw != nuevoCliente.Sw)
                                    {
                                        clientes[i].Sw.WriteLine($"{nuevoCliente.NombreUsuario} se ha conectado");
                                    }

                                }
                                catch (ObjectDisposedException)
                                {
                                    clientes.Remove(nuevoCliente);
                                }
                            }


                        }
                    }
                    string? msg = "";
                    while (msg != null && clienteConectado)
                    {
                        try
                        {
                            msg = sr.ReadLine()?.Trim();
                            switch (msg)
                            {
                                case "#exit":
                                    clienteConectado = false;
                                    lock (l)
                                    {

                                        for (int i = clientes.Count - 1; i >= 0; i--)
                                        {
                                            try
                                            {
                                                if (clientes[i].Sw != nuevoCliente.Sw)
                                                {
                                                    clientes[i].Sw.WriteLine($"{nuevoCliente.NombreUsuario} se ha desconectado");
                                                }

                                            }
                                            catch (ObjectDisposedException)
                                            {
                                                clientes.Remove(nuevoCliente);
                                            }
                                        }
                                    }
                                    break;


                                case "#list":
                                    sw.WriteLine("Usuarios conectados: ");
                                    lock (l)
                                    {
                                        foreach (Cliente c in clientes)
                                        {
                                            sw.WriteLine($"{c.NombreUsuario}");
                                        }
                                    }

                                    break;

                                default:
                                    lock (l)
                                    {
                                        for (int i = clientes.Count - 1; i >= 0; i--)
                                        {

                                            try
                                            {
                                                if (clientes[i].Sw != nuevoCliente.Sw)
                                                {
                                                    clientes[i].Sw.WriteLine($"{nuevoCliente.NombreUsuario}@{nuevoCliente.IP}:{msg}");
                                                }

                                            }
                                            catch (ObjectDisposedException)
                                            {
                                                clientes.Remove(nuevoCliente);
                                            }
                                        }


                                    }
                                    break;

                            }
                        }
                        catch (IOException)
                        {
                            clienteConectado = false;

                        }

                    }



                }
            }

        }


    }
}
