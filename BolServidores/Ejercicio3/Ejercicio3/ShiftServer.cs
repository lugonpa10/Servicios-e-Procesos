using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3
{
    internal class ShiftServer
    {
        public string[] users;
        public List<string> waitQueue = new List<string>();
        public int Port { set; get; } = 31416;
        public int puertoReferencia = 1024;
        private Socket s;
        public bool puertoOcupado = true;
        int puertoMax = IPEndPoint.MaxPort;
        public bool ServeRunning { set; get; } = true;
        public bool clienteConectado = true;

        public void ReadNames(string rutaArchivo)
        {
            try
            {
                string linea = "";
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        users = sr.ReadToEnd().ToLower().Split(";");



                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error con el archivo");
            }
        }

        public int ReadPin(string rutaArchivo)
        {
            try
            {
                string linea = "";
                string pinTexto;
                int pinNumeros;

                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    linea = sr.ReadLine();
                    if (linea == null || linea.Length != 4)
                    {
                        return -1;

                    }
                    else
                    {
                        pinTexto = linea.Substring(0, 4);
                        if (!int.TryParse(pinTexto, out pinNumeros))
                        {
                            return -1;

                        }
                        else
                        {
                            return pinNumeros;

                        }
                    }
                }
            }
            catch (IOException)
            {
                return -1;
            }
        }

        public void Init()
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

                        ie.Port = puertoReferencia;
                        if (puertoReferencia > puertoMax)
                        {
                            Console.WriteLine("Todos los puertos están ocupados");
                        }
                        Port = puertoReferencia;
                        puertoReferencia++;

                    }
                }
                Console.WriteLine($"El puerto {ie.Port} esta libre");

                string userProfile = Environment.GetEnvironmentVariable("userprofile");
                string archivo = "usuarios.txt";
                string rutaArchivo = userProfile + "\\" + archivo;
                ReadNames(rutaArchivo);




                s.Listen(10);
                while (ServeRunning)
                {
                    Socket client = s.Accept();
                    Thread hilos = new Thread(() => ClientDispatcher(client));
                    hilos.Start();

                }


            }
            catch (SocketException e) when (e.ErrorCode == 10048)
            {
                Console.WriteLine($"El puerto {Port} esta ocupado");

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
                Console.WriteLine($"El cliente se conectó {ieClient.Address} en el puerto {Port}");
                Encoding codificacion = Console.OutputEncoding;
                using (NetworkStream ns = new NetworkStream(sClient))
                using (StreamWriter sw = new StreamWriter(ns))
                using (StreamReader sr = new StreamReader(ns))
                {
                    try
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("Bienvenido al servidor,introduce tu nombre");
                        string nombreUsuario = sr.ReadLine().Trim();
                        if (nombreUsuario == null || (!users.Contains(nombreUsuario) || nombreUsuario != "admin"))
                        {
                            clienteConectado = false;

                        }
                        else if (nombreUsuario != null && users.Contains(nombreUsuario))
                        {
                            string comando = sr.ReadLine().Trim();

                            switch (comando)
                            {
                                case "list":
                                    list(sw);

                                    break;

                                case "add":
                                    add(nombreUsuario, sw);
                                    break;
                            }


                        }
                        else if (nombreUsuario != null && nombreUsuario == "admin")
                        {
                            string comando;
                            int pinCorrecto;
                            string userprofile = Environment.GetEnvironmentVariable("userprofile");
                            string archivo = "pin.txt";
                            string rutaArchivo = userprofile + "\\" + archivo;
                            try
                            {
                                pinCorrecto = ReadPin(rutaArchivo);

                            }
                            catch (UnauthorizedAccessException)
                            {
                                pinCorrecto = 1234;
                            }
                            catch (FileNotFoundException)
                            {
                                pinCorrecto = 1234;
                            }
                            catch (IOException)
                            {
                                pinCorrecto = 1234;
                            }



                            sw.WriteLine("Introduce el pin para poder continuar");
                            int pinUsuario = int.Parse(sr.ReadLine().Trim());
                            if (pinCorrecto != pinUsuario)
                            {
                                clienteConectado = false;
                            }

                            else
                            {
                                comando = sr.ReadLine().Trim();

                                if (comando.StartsWith("del "))
                                {
                                    string[] partes = comando.Split(" ");
                                    if (partes.Length != 2 || !int.TryParse(partes[1], out int pos) || pos < 0 || pos >= waitQueue.Count)
                                    {
                                        sw.WriteLine("delete error");

                                    }
                                    else
                                    {
                                        waitQueue.RemoveAt(pos);
                                        sw.WriteLine($"Se ha eliminado al usuario de la posicion {pos}");
                                    }


                                }
                                else if (comando.StartsWith("chpin "))
                                {
                                    string[] partes = comando.Split(" ");
                                    if ((partes.Length != 2 || !int.TryParse(partes[1], out int pin) || partes[1].Length != 4))
                                    {

                                        sw.WriteLine("Ocurrió un error intentando guardar el pin");

                                    }
                                    else
                                    {
                                        userprofile = Environment.GetEnvironmentVariable("userprofile");
                                        archivo = "pin.txt";
                                        rutaArchivo = userprofile + "\\" + archivo;
                                        using (StreamWriter sw2 = new StreamWriter(rutaArchivo))
                                        {
                                            sw2.WriteLine(pin);
                                        }
                                    }
                                }
                                switch (comando)
                                {
                                    case "list":
                                        list(sw);
                                        break;

                                    case "add":
                                        add(nombreUsuario, sw);
                                        break;

                                    case "exit":
                                        break;

                                    case "shutdown":
                                        break;
                                }
                            }

                        }

                    }
                    catch (IOException)
                    {
                        clienteConectado = false;
                    }

                }


            }
        }

        public void list(StreamWriter sw)
        {
            if (waitQueue.Count == 0)
            {
                sw.WriteLine("No hay nadie en la cola");
            }
            else
            {
                sw.WriteLine("Usuarios en la cola: ");
                foreach (string nombres in waitQueue)
                {
                    sw.WriteLine(nombres);
                }
            }


        }

        public void add(string nombreUsuario, StreamWriter sw)
        {
            bool añadirUsuario = true;

            foreach (string nombres in waitQueue)
            {
                string[] usuarioEnLista = nombres.Split("-");
                if (nombreUsuario == usuarioEnLista[0])
                {
                    añadirUsuario = false;
                }

            }

            if (añadirUsuario)
            {
                string fecha = DateTime.Now.ToString("d");
                string hora = DateTime.Now.ToString("T");
                string concatenacion = nombreUsuario + "-" + fecha + " " + hora;
                waitQueue.Add(concatenacion);
                sw.WriteLine("OK");

            }




        }


    }
}
