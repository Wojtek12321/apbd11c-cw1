using System;
using System.ComponentModel;
using Kontenery;

public class Program
{
    public static void Main()
    {

        //stworzenie kontenera danego typu
        var kontener1 = new KontenerNaPlyny(50000, 20000, 3000, 5000, "TAK");
        var kontener2 = new KontenerNaGaz(60000, 25000, 3200, 5500, 2000);
        var kontener3 = new KontenerChlodniczy(40000, 18000, 2900, 4800, "Jajka", 20);

        

        //zaladowanie ladunku do danego kontenera 
        kontener1.Zaladuj(1880);
        kontener2.Zaladuj(180);
        kontener3.Zaladuj(100, "Czekolada");

        
        //zaladowanie kontenera na statek

        var kontenerowiec1 = new Statek(123, 30, 50, 10000);
        var kontenerowiec2 = new Statek(321, 20, 50, 20000);
        var kontener4 = new KontenerChlodniczy(60000, 17000, 2100, 3400, "Maslo", 10);
        kontenerowiec1.ZaladujKontener(kontener4);


        //zaladowanie listy kontenerow na statek

        kontenerowiec1.ZaladujListeKontenerow(new List<Kontener> { kontener1, kontener2, kontener3 });


        //wyladowanie kontenera ze statku

        kontenerowiec1.WyladujKontener(kontener4);

        //rozladowanie kontenera
        kontener1.Oproznij();
        kontener2.Oproznij();
        kontener3.Oproznij();

        //zastapienie kontenera na statku o danym numerze innym kontenerem

        kontenerowiec1.ZastapKontener("KON-C-3", kontener4);


        //mozliwosc przeniesienia kontenera miedzy dwoma statkami

        kontenerowiec1.PrzeniesKontener("KON-C-3", kontenerowiec1, kontenerowiec2);

        
        //wypisanie informacji o danym kontenerze

        Console.WriteLine(kontener1);

        //wypisanie informacji o danym statku i jego ladunku
        kontenerowiec1.PrintInfo();

      
       
    }
}
