using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class ChatRoom
    {
        public bool serverRunning = true;
        public int Port { set; get; } = 31416;
        public int[] puertosAlternativos = { 135, 135, 135, 2324 };
        public bool puertoOcupado = true;
        private Socket s;
        private int i;
     

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

                    }catch(SocketException e) when (e.ErrorCode == 10048)
                    {
                        puertoOcupado = true;
                        ie.Port = puertosAlternativos[i];
                        i++;
                    }
                }
                Console.WriteLine($"El puerto {ie.Port} esta libre");
                s.Listen(10);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Puertos ocupados");
            }catch(SocketException e) when (e.ErrorCode == 10048)
            {
                Console.WriteLine($"Puerto ");
            }
            catch (SocketException)
            {
                Console.WriteLine("Fin del servidor");
            }


        }



    }
}
