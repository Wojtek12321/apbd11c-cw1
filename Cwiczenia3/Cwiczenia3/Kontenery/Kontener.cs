using System;
using System.Collections.Generic;

namespace Kontenery
{
        public class Kontener : IHazardNotifier
    {
        
    public int Masa { get; protected set; }
        public int WagaKontenera { get; protected set; }
        public int Glebokosc { get; }
        public int Wysokosc { get; }
        public string NumerSeryjny { get; }
        public int MaxLadownosc { get; }
        private static int counter = 1;
        public bool IsHazardous { get; }

        public Kontener(string type, int wysokosc, int glebokosc, int masa, int maxLadownosc, bool isHazardous = false)
        {
            NumerSeryjny = $"KON-{type}-{counter++}";
            Wysokosc = wysokosc;
            Glebokosc = glebokosc;
            Masa = masa;
            MaxLadownosc = maxLadownosc;
            WagaKontenera = 0;
            IsHazardous = isHazardous;
        }
        public virtual void Oproznij()
        {
            Masa = 0; 
        }

        public virtual void Zaladuj(int MasaZaladunku)
        {
            if (Masa + MasaZaladunku > MaxLadownosc)
                throw new OverfillException("Maksymalna pojemnosc kontenera zostala przekroczona");
            Masa += MasaZaladunku;
        }
        
        public void NotifyHazard(string message)
        {
        Console.WriteLine($"Niebezpieczna sytuacja w {NumerSeryjny}: {message}");
        }

        public override string ToString()
        {
            return $"{NumerSeryjny}: Wysokosc={Wysokosc}cm, Głębokosc={Glebokosc}cm,Masa={Masa}kg, MaxLadownosc={MaxLadownosc}kg";
        }
    }
}