using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontenery
{
    public class KontenerNaPlyny : Kontener
    {
       public string Bezpieczenstwo { get; } 
        public KontenerNaPlyny(int wysokość, int głębokość, int masa, int maxŁadowność, string bezpieczenstwo) : base("P", wysokość, głębokość, masa, maxŁadowność)
        {
            Bezpieczenstwo = bezpieczenstwo;
        }

        public override void Zaladuj(int MasaZaladunku)
        {
            double DopuszczalnyZaladunek = 0;
            if (Bezpieczenstwo == "TAK")
            {
                DopuszczalnyZaladunek = MaxLadownosc * 0.9;
                if (Masa + MasaZaladunku > DopuszczalnyZaladunek)
                {
                    NotifyHazard("Przekroczono limit ladownosci dla bezpiecznego towaru");
                }
                else 
                {
                    Masa += MasaZaladunku;
                }

            }
            else
            {
                DopuszczalnyZaladunek = MaxLadownosc * 0.5;
                if (Masa + MasaZaladunku > DopuszczalnyZaladunek)
                {
                    NotifyHazard("Przekroczono limit ladownosci dla niebezpiecznego towaru");
                }
                else
                {
                    Masa += MasaZaladunku;
                }
            }
        }
        
        public override string ToString()
        {
            return base.ToString() + $", Bezpieczenstwo={Bezpieczenstwo}";
        }
        
       
    }

    
}
