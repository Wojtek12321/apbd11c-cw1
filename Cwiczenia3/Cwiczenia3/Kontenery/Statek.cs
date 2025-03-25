using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kontenery
{
    internal class Statek
    {
        private List<Kontener> ListaKontenerow = new List<Kontener>();
        public int ID { get; }
        public int MaxPredkosc { get; }
        public int MaxLiczbaKontenerow { get; }
        public int MaxWaga { get; }
        

        public Statek(int id, int maxpredkosc, int maxliczbakontenerow, int maxwaga)
        {
            ID = id;
            MaxPredkosc = maxpredkosc;
            MaxLiczbaKontenerow = maxliczbakontenerow;
            MaxWaga = maxwaga;
        }
        

        public void ZaladujKontener(Kontener kontener)
        {
            ListaKontenerow.Add(kontener);
        }

        public void ZaladujListeKontenerow(List<Kontener> ListaKontenerow)
        {
            foreach (var kontener in ListaKontenerow)
            {
                ZaladujKontener(kontener);
            }
        }

        public void WyladujKontener(Kontener kontener)
        {
            ListaKontenerow.Remove(kontener);
        }


        public void ZastapKontener(string NumerSeryjny, Kontener NowyKontener)
        {
            var ObecnyKontener = ListaKontenerow.FirstOrDefault(c => c.NumerSeryjny == NumerSeryjny);
            if (ObecnyKontener != null)
            {
                ListaKontenerow.Remove(ObecnyKontener);
                ListaKontenerow.Add(NowyKontener);
                Console.WriteLine($"Kontener {ObecnyKontener} zostal zastapiony kontenerem {NowyKontener.NumerSeryjny}.");
            }
            else
            {
                Console.WriteLine($"Kontener o numerze seryjnym {NumerSeryjny} nie został znaleziony na statku");
            }
        }
        

        public void PrzeniesKontener(string NumerSeryjny, Statek Pierwszy, Statek Drugi)
        {
            var ObecnyKontener = ListaKontenerow.FirstOrDefault(c => c.NumerSeryjny == NumerSeryjny);
            if (ObecnyKontener != null)
            {
                Pierwszy.ListaKontenerow.Remove(ObecnyKontener);
                Drugi.ListaKontenerow.Add(ObecnyKontener);
              
            }
            else
            {
                Console.WriteLine($"Kontener o numerze seryjnym {NumerSeryjny} nie został znaleziony na statku");
            }
        }
        
        public void PrintInfo()
            {
                Console.WriteLine($"Statek {ID} (MaxPredkosc: {MaxPredkosc} wezlow, Kontenery: {ListaKontenerow.Count}, MaxLiczbaKontenerow: {MaxLiczbaKontenerow}), MaxWaga: {MaxWaga}kg");
                foreach (var c in ListaKontenerow)
                    Console.WriteLine(c);
            }
        
    }
}
