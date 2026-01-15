using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2
{
    internal class Cliente
    {
        private string nombreUsuario;
        private int ip;
        private StreamWriter sw;


        public string NombreUsuario
        {
            set
            {
                nombreUsuario = value;
            }
            get
            {
                return nombreUsuario;
            }
        }

        public int Ip
        {
            set
            {
                ip = value;
            }
            get
            {
                return ip;
            }
        }

        public StreamWriter Sw
        {
            set
            {
                sw = value;
            }
            get
            {
                return sw;
            }
        }

        public Cliente(string nombreUsuario, int ip, StreamWriter sw)
        {
            NombreUsuario = nombreUsuario;
            Ip = ip;
            Sw = sw;
        }
    }
}
