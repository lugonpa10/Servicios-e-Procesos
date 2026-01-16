using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    public class Cliente
    {

        public string NombreUsuario { set; get; }
        public IPAddress IP { set; get; }

        public StreamWriter Sw { set; get; }



        public Cliente(string nombreUsuario, IPAddress ip, StreamWriter sw)
        {
            NombreUsuario = nombreUsuario;
            IP = ip;
            Sw = sw;
        }

    }
}
