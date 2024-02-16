using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Program
    {
        static int AllIdProjekty;
        static int AllIdManagerowie;
        static int AllIdPracownicy;
        static int AllIdZadania;
        static int AllIdZasoby;
        public static (List<Projekt>, List<Manager>, List<Pracownik>, List<Zadanie>, List<Zasób>) ImportData()
        {
            string[] lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\IdsData.csv");
            string[] data0 = lines[0].Split(',');
            AllIdProjekty = int.Parse(data0[0]);
            AllIdManagerowie = int.Parse(data0[1]);
            AllIdPracownicy = int.Parse(data0[2]);
            AllIdZadania = int.Parse(data0[3]);
            AllIdZasoby = int.Parse(data0[4]);

            List<Zasób> zasoby = new List<Zasób>();
            lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\zasoby.csv");
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                string nazwa = data[1];
                string producent = data[2];
                string typ = data[3];
                int ilosc = int.Parse(data[4]);
                Zasób zasob = new Zasób(id, nazwa, producent, typ, ilosc);
                zasoby.Add(zasob);
            }

            List<Zadanie> zadania = new List<Zadanie>();
            lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\zadania.csv");
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                string nazwa = data[1];
                Zadanie zadanie = new Zadanie(id, nazwa);
                for(int za = 2; za < data.Length; za++){
                    foreach(var item in zasoby){
                        if(item.id == int.Parse(data[za])){
                            zadanie.Zasoby.Add(item);
                            break;
                        }
                    }
                }
                zadania.Add(zadanie);
            }

            List<Pracownik> pracownicy = new List<Pracownik>();
            lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\pracownicy.csv");
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                string imie = data[1];
                string nazwisko = data[2];
                string telefon = data[3];
                string email = data[4];
                string stanowisko = data[5];
                int pensja = int.Parse(data[6]);
                Pracownik pracownik = new Pracownik(id, imie, nazwisko, telefon, email, stanowisko, pensja);
                pracownicy.Add(pracownik);
            }
            
            List<Manager> managerowie = new List<Manager>();
            lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\managerowie.csv");
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                string imie = data[1];
                string nazwisko = data[2];
                string telefon = data[3];
                string email = data[4];
                int pensja = int.Parse(data[5]);
                Manager manager = new Manager(id, imie, nazwisko, telefon, email, pensja);
                for(int za = 6; za < data.Length; za++){
                    foreach(var item in pracownicy){
                        if(item.id == int.Parse(data[za])){
                            manager.pracownicy.Add(item);
                            break;
                        }
                    }
                }
                managerowie.Add(manager);
            }

            List<Projekt> projekty = new List<Projekt>();
            lines = System.IO.File.ReadAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\projekty.csv");
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                string nazwa = data[1];
                string opis = data[2];
                DateTime dataRozpoczecia = DateTime.Parse(data[3]);
                DateTime dataZakonczenia = DateTime.Parse(data[4]);
                Projekt projekt = new Projekt(id, nazwa, opis, dataRozpoczecia, dataZakonczenia);
                for(int zee = 0; zee < managerowie.Count; zee++){
                    if(managerowie[zee].id == int.Parse(data[5])){
                        projekt.manager = managerowie[zee];
                        break;
                    }
                }
                for(int za = 6; za < data.Length; za++){
                    foreach(var item in zadania){
                        if(item.id == int.Parse(data[za])){
                            projekt.Zadania.Add(item);
                            break;
                        }
                    }
                }
                projekty.Add(projekt);
            }

            return (projekty, managerowie, pracownicy, zadania, zasoby);
        }

        public static void ExportData(List<Projekt> projekty, List<Manager> managerowie, List<Pracownik> pracownicy, List<Zadanie> zadania, List<Zasób> zasoby)
        {
            string[] lines = new string[1];
            lines[0] = AllIdProjekty + "," + AllIdManagerowie + "," + AllIdPracownicy + "," + AllIdZadania + "," + AllIdZasoby;
            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\IdsData.csv", lines);
            lines = new string[projekty.Count];
            for (int i = 0; i < projekty.Count; i++)
            {
                string tempZadania = "";
                for(int z = 0; z < projekty[i].Zadania.Count; z++){
                    tempZadania += projekty[i].Zadania[z].id + (z == projekty[i].Zadania.Count - 1 ? "" : ",");
                }
                lines[i] = projekty[i].id + "," + projekty[i].Nazwa + "," + projekty[i].Opis + "," + projekty[i].DataRozpoczecia + "," + projekty[i].DeadLine
                 + "," + projekty[i].Manager.id + "," + tempZadania;
            }

            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\projekty.csv", lines);

            lines = new string[managerowie.Count];
            for (int i = 0; i < managerowie.Count; i++)
            {
                string tempPracownicy = "";
                for(int z = 0; z < managerowie[i].pracownicy.Count; z++){
                    tempPracownicy += managerowie[i].pracownicy[z].id + (z == managerowie[i].pracownicy.Count - 1 ? "" : ",");
                }
                lines[i] = managerowie[i].id + "," + managerowie[i].Imie + "," + managerowie[i].Nazwisko + "," + managerowie[i].Telefon + "," + managerowie[i].Email + "," + managerowie[i].Pensja + "," + tempPracownicy;
            }
          
            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\managerowie.csv", lines);

            lines = new string[pracownicy.Count];
            for (int i = 0; i < pracownicy.Count; i++)
            {
                lines[i] = pracownicy[i].id + "," + pracownicy[i].Imie + "," + pracownicy[i].Nazwisko + "," + pracownicy[i].Telefon + "," + pracownicy[i].Email + "," + pracownicy[i].Stanowisko + "," + pracownicy[i].Pensja;
            }
            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\pracownicy.csv", lines);

            lines = new string[zadania.Count];
            for (int i = 0; i < zadania.Count; i++)
            {
                string tempZasoby = "";
                for(int z = 0; z < zadania[i].Zasoby.Count; z++){
                    tempZasoby += zadania[i].Zasoby[z].id + (z == zadania[i].Zasoby.Count - 1 ? "" : ",");
                }
                lines[i] = zadania[i].id + "," + zadania[i].Nazwa + "," + tempZasoby;
            }
            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\zadania.csv", lines);

            lines = new string[zasoby.Count];
            for (int i = 0; i < zasoby.Count; i++)
            {
                lines[i] = zasoby[i].id + "," + zasoby[i].Nazwa + "," + zasoby[i].Producent + "," + zasoby[i].Typ + "," + zasoby[i].Ilość;
            }
            System.IO.File.WriteAllLines(@"W:\Offline\Projekt programowanie obiektowe\Projekt\ProjektObiektowka\PlikiCSV\zasoby.csv", lines);
        }
        
    static void Main(string[] args)
        {
            bool Zakoncz = false;
            int wybor = 0;
            List<Projekt> projekty = new List<Projekt>();
            List<Manager> managerowie = new List<Manager>();
            List<Pracownik> pracownicy = new List<Pracownik>();
            List<Zadanie> zadania = new List<Zadanie>();
            List<Zasób> zasoby = new List<Zasób>();

            try {
                (projekty, managerowie, pracownicy, zadania, zasoby) = ImportData();
            } catch (Exception ) {
                Console.WriteLine("Pliki .csv nie istnieją!");
                Console.WriteLine();
            }
            //Ogólny test prototypu programu
            /* Projekt projekt = UtwórzProjekt();
             Console.WriteLine(projekt.ToString());//test
             Manager manager = UtwórzManagera();
             projekt.PrzypiszManagera(manager);
             Console.WriteLine(manager.ToString());//test
             Pracownik pracownik = UtwórzPracownika();
             manager.DodajPracownika(pracownik);
             Console.WriteLine(pracownik.ToString());//test
             Console.WriteLine("Podaj zadanie dla pracownika: ");
             string zadanie = Console.ReadLine();
             manager.DodajZadanie(pracownik, zadanie);
             pracownik.DodajZadanie(zadanie);
             projekt.DodajZadanie(new Zadanie(zadanie));
             projekt.WyswietlZadania();
             manager.WyswietlPracownikow();

         }
         static Projekt UtwórzProjekt()
         {
             Console.WriteLine("Podaj nazwę projektu: ");
             string nazwa = Console.ReadLine();
             Console.WriteLine("Podaj opis projektu: ");
             string opis = Console.ReadLine();
             Console.WriteLine("Podaj datę rozpoczęcia projektu (format yyyy-mm-dd): ");
             DateTime dataRozpoczecia = DateTime.Parse(Console.ReadLine());
             Console.WriteLine("Podaj datę zakończenia projektu (format yyyy-mm-dd): ");
             DateTime dataZakonczenia = DateTime.Parse(Console.ReadLine());

             return new Projekt(nazwa, opis, dataRozpoczecia, dataZakonczenia);
         }
         static Manager UtwórzManagera()
         {
             Console.WriteLine("Podaj imię managera: ");
             string imie = Console.ReadLine();
             Console.WriteLine("Podaj nazwisko managera: ");
             string nazwisko = Console.ReadLine();
             Console.WriteLine("Podaj telefon managera: ");
             string telefon = Console.ReadLine();
             Console.WriteLine("Podaj email managera: ");
             string email = Console.ReadLine();
             Console.WriteLine("Podaj pensję managera: ");
             int pensja = int.Parse(Console.ReadLine());

             return new Manager(imie, nazwisko, telefon, email, pensja);
         }
         static Pracownik UtwórzPracownika()
         {
             Console.WriteLine("Podaj imię pracownika: ");
             string imie = Console.ReadLine();
             Console.WriteLine("Podaj nazwisko pracownika: ");
             string nazwisko = Console.ReadLine();
             Console.WriteLine("Podaj telefon pracownika: ");
             string telefon = Console.ReadLine();
             Console.WriteLine("Podaj email pracownika: ");
             string email = Console.ReadLine();
             Console.WriteLine("Podaj stanowisko pracownika: ");
             string stanowisko = Console.ReadLine();
             Console.WriteLine("Podaj pensję pracownika: ");
             int pensja = int.Parse(Console.ReadLine());

             return new Pracownik(imie, nazwisko, telefon, email, stanowisko, pensja);
         }
         */

            do
            {
                Console.WriteLine("1. Dodaj projekt");
                Console.WriteLine("2. Dodaj managera");
                Console.WriteLine("3. Dodaj pracownika");
                Console.WriteLine("4. Dodaj zadanie");
                Console.WriteLine("5. Dodaj zasób do zadania");
                Console.WriteLine("6. Wyświetl projekty");
                Console.WriteLine("7. Wyświetl managerów");
                Console.WriteLine("8. Wyświetl pracowników");
                Console.WriteLine("9. Wyświetl zadania wraz z zasobami");
                //Console.WriteLine("10. Wyświetl zasoby");
                Console.WriteLine("10. Eksportuj dane");
                Console.WriteLine("11. Wyjdź");
                wybor = int.Parse(Console.ReadLine());
                switch (wybor)
                {
                    case 1:
                        Console.WriteLine("Podaj nazwę projektu: ");
                        string nazwa = Console.ReadLine();
                        Console.WriteLine("Podaj opis projektu: ");
                        string opis = Console.ReadLine();
                        Console.WriteLine("Podaj datę rozpoczęcia projektu (format yyyy-mm-dd): ");
                        DateTime dataRozpoczecia = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Podaj datę zakończenia projektu (format yyyy-mm-dd): ");
                        DateTime dataZakonczenia = DateTime.Parse(Console.ReadLine());
                        Projekt projekt = new Projekt(AllIdProjekty,nazwa, opis, dataRozpoczecia, dataZakonczenia);
                        AllIdProjekty++;
                        projekty.Add(projekt);
                        break;
                    case 2:
                        Console.WriteLine("Podaj imię managera: ");
                        string imie = Console.ReadLine();
                        Console.WriteLine("Podaj nazwisko managera: ");
                        string nazwisko = Console.ReadLine();
                        Console.WriteLine("Podaj telefon managera: ");
                        string telefon = Console.ReadLine();
                        Console.WriteLine("Podaj email managera: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("Podaj pensję managera: ");
                        int pensja = int.Parse(Console.ReadLine());
                        Manager manager = new Manager(AllIdManagerowie,imie, nazwisko, telefon, email, pensja);
                        Console.WriteLine("Wybierz projekt: ");
                        AllIdManagerowie++;
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        int wyborProjektu = int.Parse(Console.ReadLine());
                        projekty[wyborProjektu].PrzypiszManagera(manager);
                        Console.WriteLine("Dodano managera do projektu: " + projekty[wyborProjektu].Nazwa);
                        managerowie.Add(manager);
                        break;
                    case 3:
                        Console.WriteLine("Podaj imię pracownika: ");
                        imie = Console.ReadLine();
                        Console.WriteLine("Podaj nazwisko pracownika: ");
                        nazwisko = Console.ReadLine();
                        Console.WriteLine("Podaj telefon pracownika: ");
                        telefon = Console.ReadLine();
                        Console.WriteLine("Podaj email pracownika: ");
                        email = Console.ReadLine();
                        Console.WriteLine("Podaj stanowisko pracownika: ");
                        string stanowisko = Console.ReadLine();
                        Console.WriteLine("Podaj pensję pracownika: ");
                        pensja = int.Parse(Console.ReadLine());
                        Pracownik pracownik = new Pracownik(AllIdPracownicy,imie, nazwisko, telefon, email, stanowisko, pensja);
                        pracownicy.Add(pracownik);
                        AllIdPracownicy++;
                        Console.WriteLine("Wybierz projekt: ");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        projekty[wyborProjektu].Manager.DodajPracownika(pracownik);
                        Console.WriteLine("Dodano pracownika do projektu: " + projekty[wyborProjektu].Nazwa);
                        pracownicy.Add(pracownik);
                        break;
                    case 4:
                        Console.WriteLine("Podaj zadanie dla pracownika: ");
                        string zadanie = Console.ReadLine();
                        Console.WriteLine("Wybierz projekt: ");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        Console.WriteLine("Wybierz pracownika: ");
                        for (int i = 0; i < projekty[wyborProjektu].Manager.pracownicy.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[wyborProjektu].Manager.pracownicy[i].Imie + " " + projekty[wyborProjektu].Manager.pracownicy[i].Nazwisko);
                        }
                        int wyborPracownika = int.Parse(Console.ReadLine());
                        //projekty[wyborProjektu].Manager.DodajZadanie(projekty[wyborProjektu].Manager.pracownicy[wyborPracownika], zadanie);
                        Zadanie tempZadanie0 = new Zadanie(AllIdZadania,zadanie);
                        projekty[wyborProjektu].manager.DodajZadanie(projekty[wyborProjektu].manager.pracownicy[wyborPracownika], zadanie,AllIdZadania);
                        AllIdZadania++;
                        projekty[wyborProjektu].DodajZadanie(tempZadanie0); // shrek
                        zadania.Add(tempZadanie0);
                        break;
                    case 5:
                        Console.WriteLine("1.Materiały | 2.Trwałe |3.Zasób nieokreślony");
                        int wyborRodzajuZasobu = int.Parse(Console.ReadLine());
                        if (wyborRodzajuZasobu == 1)
                        {
                            Console.WriteLine("Podaj nazwę zasobu: ");
                            nazwa = Console.ReadLine();
                            Console.WriteLine("Podaj producenta zasobu: ");
                            string producent = Console.ReadLine();
                            Console.WriteLine("Podaj typ zasobu: ");
                            string typ = Console.ReadLine();
                            Console.WriteLine("Podaj ilość zasobu: ");
                            int ilosc = int.Parse(Console.ReadLine());
                            Console.WriteLine("Podaj czas posiadania zasobu: ");
                            String czasPosiadania = Console.ReadLine();
                            Console.WriteLine("Wybierz projekt: ");
                            for (int i = 0; i < projekty.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[i].Nazwa);
                            }
                            wyborProjektu = int.Parse(Console.ReadLine());
                            Console.WriteLine("Wybierz zadanie: ");
                            for (int i = 0; i < projekty[wyborProjektu].Zadania.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[wyborProjektu].Zadania[i].Nazwa);
                            }
                            int wyborZadania = int.Parse(Console.ReadLine());
                            Zasób tempZasoby = new Materiały(AllIdZasoby,nazwa, producent, typ, ilosc, czasPosiadania);
                            projekty[wyborProjektu].Zadania[wyborZadania].Zasoby.Add(tempZasoby);
                            zasoby.Add(tempZasoby);
                            AllIdZasoby++;
                        }
                        else if (wyborRodzajuZasobu == 2)
                        {
                            Console.WriteLine("Podaj nazwę zasobu: ");
                            nazwa = Console.ReadLine();
                            Console.WriteLine("Podaj producenta zasobu: ");
                            string producent = Console.ReadLine();
                            Console.WriteLine("Podaj typ zasobu: ");
                            string typ = Console.ReadLine();
                            Console.WriteLine("Podaj ilość zasobu: ");
                            int ilosc = int.Parse(Console.ReadLine());
                            Console.WriteLine("Podaj ile razy użyty zasób: ");
                            int ileRazyUżyty = int.Parse(Console.ReadLine());
                            Console.WriteLine("Wybierz projekt: ");
                            for (int i = 0; i < projekty.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[i].Nazwa);
                            }
                            wyborProjektu = int.Parse(Console.ReadLine());
                            Console.WriteLine("Wybierz zadanie: ");
                            for (int i = 0; i < projekty[wyborProjektu].Zadania.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[wyborProjektu].Zadania[i].Nazwa);
                            }
                            Zasób tempZasób = new Trwałe(AllIdZasoby,nazwa, producent, typ, ilosc, ileRazyUżyty);
                            int wyborZadania = int.Parse(Console.ReadLine());
                            projekty[wyborProjektu].Zadania[wyborZadania].Zasoby.Add(tempZasób);
                            zasoby.Add(tempZasób);
                            AllIdZasoby++;
                        }
                        else
                        {
                            Console.WriteLine("Podaj nazwę zasobu: ");
                            nazwa = Console.ReadLine();
                            Console.WriteLine("Podaj producenta zasobu: ");
                            string producent = Console.ReadLine();
                            Console.WriteLine("Podaj typ zasobu: ");
                            string typ = Console.ReadLine();
                            Console.WriteLine("Podaj ilość zasobu: ");
                            int ilosc = int.Parse(Console.ReadLine());
                            Console.WriteLine("Wybierz projekt: ");
                            for (int i = 0; i < projekty.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[i].Nazwa);
                            }
                            wyborProjektu = int.Parse(Console.ReadLine());
                            Console.WriteLine("Wybierz zadanie: ");
                            for (int i = 0; i < projekty[wyborProjektu].Zadania.Count; i++)
                            {
                                Console.WriteLine(i + ". " + projekty[wyborProjektu].Zadania[i].Nazwa);
                            }
                            int wyborZadania = int.Parse(Console.ReadLine());
                            projekty[wyborProjektu].Zadania[wyborZadania].Zasoby.Add(new Zasób(AllIdZasoby,nazwa, producent, typ, ilosc));
                            AllIdZasoby++;
                            zasoby.Add(new Zasób(nazwa, producent, typ, ilosc));
                        }

                        break;
                    case 6://wyswietl projekty
                        Console.WriteLine("Format: Nazwa, Opis, DataRozpoczecia, DataZakończenia");
                        foreach (Projekt p in projekty)
                        {
                            Console.WriteLine(p.ToString());
                        }
                        break;
                    case 7://wyswietl managerow
                        Console.WriteLine("Wybierz projekt: ");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        Console.WriteLine("Format: Imie, Nazwisko, Telefon, Email, Pensja");
                        foreach (Projekt p in projekty)
                        {
                            if(wyborProjektu == p.id){
                                Console.WriteLine(p.Manager.ToString());
                                break;
                            }
                        }
                        break;
                    case 8://wyswietl pracownikow
                        Console.WriteLine("Wybierz projekt: ");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        Console.WriteLine("Format: Imie, Nazwisko, Telefon, Email, Pensja, Stanowisko");
                        foreach (Projekt p in projekty)
                        {
                            foreach (Pracownik pracownikk in p.manager.pracownicy)
                            {
                                if(wyborProjektu == p.id){
                                    Console.WriteLine(pracownikk.ToString());
                                    break;
                                }
                            }
                        }
                        break;
                    case 9: //wyswietl zadania wraz z zasobami
                        Console.WriteLine("wybierz projekt:");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        Console.WriteLine("Format zadanie: Nazwa");
                        Console.WriteLine("Format zasób: Nazwa, Producent, Typ, Ilość, CzasPosiadania/IleRazyUżyty");
                        foreach (Projekt p in projekty)
                        {
                            if(wyborProjektu == p.id){
                                p.WyswietlZadania();
                                break;
                            }
                        }
                        break;
                    /*case 10:
                        Console.WriteLine("Wybierz projekt: ");
                        for (int i = 0; i < projekty.Count; i++)
                        {
                            Console.WriteLine(i + ". " + projekty[i].Nazwa);
                        }
                        wyborProjektu = int.Parse(Console.ReadLine());
                        foreach (Zadanie z in projekty[wyborProjektu].Zadania)
                        {
                            foreach (Zasób zasob in z.Zasoby)
                            {
                                Console.WriteLine(zasob.ToString());
                            }
                        }
                        break;*/
                    case 10:
                        ExportData(projekty, managerowie, pracownicy, zadania, zasoby);
                        break;
                    case 11:
                        Zakoncz = true;
                        break;
                }
            } while (!Zakoncz);
        }
    } 
    
}