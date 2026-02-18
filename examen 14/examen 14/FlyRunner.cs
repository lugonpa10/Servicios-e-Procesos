using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examen_14
{
    internal class FlyRunner
    {
        public StreamWriter Sw { set; get; }
        public int KilledFlies { set; get; }
        public int Bites { set; get; }

        public FlyRunner(StreamWriter sw)
        {
            Sw = sw;
            KilledFlies = 0;
            Bites = 0;
        }
    }
}
