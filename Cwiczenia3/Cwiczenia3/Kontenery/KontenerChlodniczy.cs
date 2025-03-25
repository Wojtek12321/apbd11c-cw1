using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kontenery
{
    public class KontenerChlodniczy : Kontener
    {
        public string Produkt { get; private set; }
        public double Temperatura { get; private set; }
        
        private static readonly Dictionary<string, double> MinimalneTemperatury = new()
        {
            { "Banany", 13.3 },
            { "Czekolada", 18.0 },
            { "Ryby", 2.0 },
            { "Mieso", -15.0 },
            { "Lody", -18.0 },
            { "Pizza", -30.0 },
            { "Ser", 7.2 },
            { "Kielbasy", 5.0 },
            { "Maslo", 20.5 },
            { "Jajka", 19.0 }
        };
        public KontenerChlodniczy(int wysokość, int głębokość, int masa, int maxŁadowność, string produkt, double temperatura) : base("C", wysokość, głębokość, masa, maxŁadowność)
        {
            Produkt = produkt;
            Temperatura = temperatura;
            
            if (Temperatura < MinimalneTemperatury[Produkt])
            {
                NotifyHazard("Temperatura kontenera jest za niska");
            }
        }

        public void Zaladuj(int MasaZaladunku, string NowyProdukt)
        {
            if (Produkt == NowyProdukt)
            {
                if (Masa+MasaZaladunku > MaxLadownosc)
                {
                    NotifyHazard("Przekroczono limit ladownosci");
                }
                else
                {
                    Masa += MasaZaladunku;
                }
            }
            else
            {
                NotifyHazard("Mozna przechowywac produkty tylko jednego rodzaju");
            }
        }

        

        
        public override string ToString()
        {
            return $"{NumerSeryjny}: Wysokosc={Wysokosc}cm, Głębokosc={Glebokosc}cm,Masa={Masa}kg, MaxLadownosc={MaxLadownosc}kg, Produkt={Produkt}, Temperatura={Temperatura}c";
        }
    }
}