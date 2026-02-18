using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4
{
    internal class ServidorAhorcado
    {
        public int Port { set; get; } = 31416;
        public bool ServerRunning = true;
        private Socket s;
        public bool puertoOcupado = true;

        public void InitServer()
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, Port);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {

            }

        }

    }
}
