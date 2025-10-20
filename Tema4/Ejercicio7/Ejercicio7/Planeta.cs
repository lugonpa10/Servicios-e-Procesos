using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio7
{
    public class Planeta : Astro, ITerraformable
    {
        private bool gaseoso;
        private int numSatelites;

        public bool Gaseoso
        {
            set { gaseoso = value; }
            get { return gaseoso; }
        }

        public int NumSatelites
        {
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                numSatelites = value;
            }
            get { return numSatelites; }

        }

        public Planeta(string nombre, double radio, bool gaseoso, int numSatelites)
            : base(nombre, radio)
        {
            Gaseoso= gaseoso;
            NumSatelites = numSatelites;
        }



        public Planeta() : this("", 1, false, 0)
        {

        }

        public bool EsHabitable()
        {
            return !gaseoso && Radio >= 2000 && Radio <= 8000;
           
        }


        public override string ToString()
        {
            return string.Format("Nombre:{0,10}\nSatelites:{1,4}\nRadio:{2:F2}",Nombre,NumSatelites,Radio);
        }

        public static Planeta operator ++ ( Planeta  p)
        {
            p.NumSatelites++;

            return p;
        }

        public static Planeta operator -- (Planeta p)
        {
            if (p.NumSatelites ==0)
            {
                throw new ArgumentOutOfRangeException("La cantidad no puede ser menor");
            }
            p.NumSatelites--;


            return p;
        }




    }
}
