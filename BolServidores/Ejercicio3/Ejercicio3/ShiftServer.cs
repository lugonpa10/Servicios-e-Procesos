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
        public int puertoReferencia = 135;
        private Socket s;
        public bool puertoOcupado = true;
        int puertoMax = IPEndPoint.MaxPort;
        public bool ServeRunning { set; get; } = true;
        public bool clienteConectado = true;

        public void ReadNames(string rutaArchivo)
        {
            try
            {
                string contenido = "";
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea = "";
                    while ((linea = sr.ReadLine()) != null)
                    {
                        contenido += linea;


                    }
                }
                users = contenido.ToLower().Split(";");
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

                string archivoCola = "waitqueue.txt";
                try
                {
                    string linea = "";
                    using (StreamReader sr4 = new StreamReader(archivoCola))
                    {
                        while ((linea = sr4.ReadLine()) != null)
                        {
                            waitQueue.Add(linea);
                        }
                    }
                    
                }
                catch (IOException)
                {
                    Console.WriteLine("Error con el archivo");
                }


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
                        string nombreUsuario = sr.ReadLine()?.Trim();
                        if (nombreUsuario == null || (!users.Contains(nombreUsuario) && nombreUsuario != "admin"))
                        {
                            clienteConectado = false;

                        }
                        else if (nombreUsuario != null && users.Contains(nombreUsuario))
                        {

                            sw.WriteLine($"Hola {nombreUsuario}, introduce un comando");
                            string comando = sr.ReadLine()?.Trim();

                            switch (comando)
                            {
                                case "list":
                                    list(sw);

                                    break;

                                case "add":
                                    add(nombreUsuario, sw, sr);
                                    break;
                            }


                        }
                        else if (nombreUsuario != null && nombreUsuario == "admin")
                        {
                            string? comando = "";
                            string otroComando = "";
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
                            string entrada = sr.ReadLine().Trim();

                            if (!int.TryParse(entrada, out int pinUsuario))
                            {
                                sw.WriteLine("El formato de pin no es valido");
                                clienteConectado = false;

                            }
                            else if (pinCorrecto != pinUsuario)
                            {
                                sw.WriteLine("Contrasenha incorrecta");
                                clienteConectado = false;
                            }

                            else
                            {
                                sw.WriteLine("Introduce un comando");
                                comando = sr.ReadLine()?.Trim();

                                while (comando != null && clienteConectado)
                                {


                                    switch (comando)
                                    {
                                        case "list":
                                            list(sw);
                                            break;

                                        case "add":
                                            add(nombreUsuario, sw, sr);
                                            break;

                                        case "exit":
                                            sw.WriteLine("Saliendo del servidor...");
                                            clienteConectado = false;
                                            break;

                                        case "shutdown":
                                            clienteConectado = false;
                                            string archivoCola = "waitqueue.txt";
                                            try
                                            {
                                                using (StreamWriter sw3 = new StreamWriter(archivoCola))
                                                {
                                                    foreach (string nombre in waitQueue)
                                                    {
                                                        sw3.WriteLine(nombre);
                                                    }
                                                }
                                                sw.WriteLine("Archivo guardado correctamente");
                                            }
                                            catch (IOException)
                                            {
                                                sw.WriteLine("Ocurrio un error con el archivo");
                                            }


                                            stopServer();

                                            break;
                                        default:

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
                                                    try
                                                    {
                                                        using (StreamWriter sw2 = new StreamWriter(rutaArchivo))
                                                        {
                                                            sw2.WriteLine(pin);
                                                        }
                                                        sw.WriteLine("Se ha guardado el pin correctamente");
                                                    }
                                                    catch (FileNotFoundException)
                                                    {
                                                        sw.WriteLine("No se encontro el archivo");
                                                    }
                                                    catch (IOException)
                                                    {
                                                        sw.WriteLine("Ocurrio un error con el archivo");

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                sw.WriteLine("Comando no valido");
                                            }
                                            break;

                                    }

                                    if (clienteConectado)
                                    {
                                        sw.WriteLine("Introduce otro comando");
                                        otroComando = sr.ReadLine();
                                    }
                                    if (otroComando == null)
                                    {
                                        clienteConectado = false;
                                    }
                                    else
                                    {
                                        comando = otroComando;
                                    }

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

        public void add(string nombreUsuario, StreamWriter sw, StreamReader sr)
        {
            sw.WriteLine("Dime el nombre del usuario para sumarlo a la lista");
            nombreUsuario = sr.ReadLine().Trim();
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
            else
            {
                sw.WriteLine($"{nombreUsuario} ya esta en la cola");
            }




        }

        public void stopServer()
        {
            Console.WriteLine("Deteniendo el servidor");
            ServeRunning = false;
            s.Close();

        }


    }
}
