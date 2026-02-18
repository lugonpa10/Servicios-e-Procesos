using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace examen_14
{
    internal class FlyServer
    {
        List<FlyRunner> clients = new List<FlyRunner>();
        bool serverRunning = true;
        Socket s;
        static object l = new object();
        private int GetPort()
        {
            string archivo = "puertos33.txt";
            string programData = Environment.GetEnvironmentVariable("programdata");
            string rutaArchivo = programData + "\\" + archivo;
            int puertoPrincipal = 0;
            int puertoSecundario = 0;

            try
            {

                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    if ((linea = sr.ReadLine()) != null)
                    {
                        string[] puertos = linea.Split(' ');
                        if (!int.TryParse(puertos[0], out puertoPrincipal) ||
                            !int.TryParse(puertos[1], out puertoSecundario) ||
                            puertoPrincipal > IPEndPoint.MaxPort || puertoPrincipal < IPEndPoint.MinPort ||
                          puertoSecundario > IPEndPoint.MaxPort || puertoSecundario < IPEndPoint.MinPort)
                        {
                            ProblemaPuertos(rutaArchivo);
                            return -1;
                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                ProblemaPuertos(rutaArchivo);

                return -1;
            }
            catch (IOException)
            {
                ProblemaPuertos(rutaArchivo);


                return -1;
            }

            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ie = new IPEndPoint(IPAddress.Any, puertoPrincipal);
                s.Bind(ie);
                s.Listen(10);
                Console.WriteLine($"{puertoPrincipal}");

                return puertoPrincipal;
            }
            catch (SocketException e) when (e.ErrorCode == 10048)
            {
                s?.Close();
                try
                {
                    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ie = new IPEndPoint(IPAddress.Any, puertoSecundario);
                    s.Bind(ie);
                    s.Listen(10);
                    Console.WriteLine($"{puertoSecundario}");
                    return puertoSecundario;
                }
                catch (SocketException)
                {
                    s?.Close();
                    return -1;

                }

            }

        }

        public int InitServer()
        {
            int resultado = GetPort();
            if (resultado == -1)
            {
                StopServer();
                return -1;

            }

            while (serverRunning)
            {
                Socket client = s.Accept();
                Thread hilos = new Thread(() => FlyRunnerThread(client));
                hilos.Start();
            }
            return 0;


        }

        private void FlyRunnerThread(Socket sClient)
        {
            bool clienteConectado = true;
            FlyRunner fr = null;
            try
            {
                using (sClient)
                {
                    IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;
                    Console.WriteLine("Cliente conectado");
                    Encoding codificacion = Console.OutputEncoding;
                    using (NetworkStream ns = new NetworkStream(sClient))
                    using (StreamReader sr = new StreamReader(ns))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        sw.AutoFlush = true;
                         fr = new FlyRunner(sw);
                        lock (l)
                        {
                            clients.Add(fr);
                        }

                        sw.WriteLine("Fly");
                        string comando = sr.ReadLine()?.Trim();
                        while ((comando != null) && clienteConectado)
                        {
                            switch (comando)
                            {
                                case "fsw":
                                    Random rd = new Random();
                                    int resultado = rd.Next(1, 4);
                                    if (resultado == 1)
                                    {
                                        fr.KilledFlies++;
                                        fr.Sw.WriteLine($"Killed {fr.KilledFlies} fly/flies!!");
                                    }
                                    else if (resultado == 2)
                                    {
                                        fr.Bites++;
                                        fr.Sw.WriteLine($"You have been bitten. Number of bites: {fr.Bites}.");
                                    }
                                    else
                                    {
                                        lock (l)
                                        {
                                            List<FlyRunner> victimas = new List<FlyRunner>();
                                            foreach (FlyRunner c in clients)
                                            {
                                                if (c != fr)
                                                {
                                                    victimas.Add(c);

                                                }
                                            }
                                            if (victimas.Count > 0)
                                            {

                                                Random rd2 = new Random();

                                                int indice = rd2.Next(0, victimas.Count);
                                                FlyRunner elegido = victimas[indice];
                                                elegido.Bites++;
                                                elegido.Sw.WriteLine("Other fliy bites you !!");

                                            }
                                        }

                                    }

                                    break;
                                case "quit":
                                    lock (l)
                                    {

                                        fr.Sw.WriteLine($"Mordiscos: {fr.Bites} Moscas: {fr.KilledFlies}");
                                        foreach (FlyRunner c in clients)
                                        {
                                            if (c != fr)
                                            {
                                                c.Sw.WriteLine($"Someone leaves with {fr.Bites} bites and {fr.KilledFlies} flies killed.");
                                            }
                                        }
                                        clients.Remove(fr);
                                    }
                                    clienteConectado = false;
                                    break;
                                default:
                                    fr.Bites += 2;
                                    fr.Sw.WriteLine($"Big mistake, you were bitten twice.Number of bites: {fr.Bites}.");
                                    lock (l)
                                    {
                                        List<FlyRunner> aleatorios = new List<FlyRunner>();
                                        foreach (FlyRunner f in clients)
                                        {
                                            aleatorios.Add(f);
                                        }
                                        Random rd3 = new Random();
                                        int indice = rd3.Next(0, aleatorios.Count);
                                        FlyRunner elegido = aleatorios[indice];
                                        elegido.Bites--;
                                        elegido.Sw.WriteLine("I get you one bite");


                                    }
                                    break;

                            }
                            if (clienteConectado)
                            {
                                comando = sr.ReadLine()?.Trim();
                            }
                        }



                    }
                }

            }
            catch (IOException)
            {
                clienteConectado = false;
                lock (l)
                {
                    clients.Remove(fr);
                    foreach (FlyRunner c in clients)
                    {
                        c.Sw.WriteLine($"Someone leaves with {fr.Bites} bites and {fr.KilledFlies} flies killed.");
                    }
                }

            }
        }



        private void ProblemaPuertos(string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine("135 31416");
                Random rd = new Random();
                for (int i = 0; i < 8; i++)
                {
                    sw.WriteLine(rd.Next(1024, IPEndPoint.MaxPort + 1));
                }
            }
        }

        private void StopServer()
        {
            Console.WriteLine("Deteniendo el servidor");
            serverRunning = false;
            s?.Close();


        }
    }
}
