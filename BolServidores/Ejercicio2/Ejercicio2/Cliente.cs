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
    }
}
