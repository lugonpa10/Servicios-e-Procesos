using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio7
{
    public abstract class Astro
    {
        private string nombre;
        public string Nombre
        {
            set
            {
                nombre = value.ToUpper() ;
            }

            get { return "\""+nombre+"\"" ; }

        }
        private double radio;

        public double Radio
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                radio = value;
            }

            get { return radio; }
        }


        public Astro(string nombre, double radio)
        {
           Nombre = nombre;
            Radio = radio;
        }

        public Astro(): this("Tierra", 6378)
        {
        }
      
        public override bool Equals(object? obj)
        {

            if (obj is Astro otroAstro)
            {
                return Nombre == otroAstro.Nombre;

            }
            else if (obj is String str)
            {
                return Nombre == str;
            }
            return false;



        }




    }

}
