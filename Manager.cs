using ProjektObiektowka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Manager : Człowiek
    {
        public int id;
         public List<Pracownik> pracownicy;

        public Manager(int id, string imie, string nazwisko, string telefon, string email, int pensja) : base(imie, nazwisko, telefon, email, pensja)
        {
            this.id = id;
            pracownicy = new List<Pracownik>();
        }

         public void DodajPracownika(Pracownik pracownik)
        {
            pracownicy.Add(pracownik);
        }

         public void UsunPracownika(Pracownik pracownik)
        {
            pracownicy.Remove(pracownik);
        }

        public void WyswietlPracownikow()
        {
            foreach (Pracownik pracownik in pracownicy)
            {
                Console.WriteLine(pracownik);
            }
        }

         public void DodajZadanie(Pracownik pracownik, string zadanie, int id)
        {
            pracownik.DodajZadanie(zadanie,id);
        }

         List<Pracownik> Pracownicy
        {
            get { return pracownicy; }
        }        

    }
}
