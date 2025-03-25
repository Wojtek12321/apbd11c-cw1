using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontenery
{
    public class KontenerNaGaz : Kontener
    {
        public int Cisnienie { get; }
        public KontenerNaGaz(int wysokość, int głębokość, int masa, int maxŁadowność, int cisnienie) : base("G", wysokość, głębokość, masa, maxŁadowność)
        {
            Cisnienie = cisnienie;
        }

        public override void Oproznij()
        {
            Masa = (int)(Masa * 0.05);
        }

        public override string ToString()
        {
            return $"{NumerSeryjny}: Wysokosc={Wysokosc}cm, Głębokosc={Glebokosc}cm,Masa={Masa}kg, MaxLadownosc={MaxLadownosc}kg, Cisnienie={Cisnienie}atmosfer";
        }

    }
}
