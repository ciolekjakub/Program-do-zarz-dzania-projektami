using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Pracownik : Człowiek
    {
        public int id;
        string stanowisko;

        public Pracownik(int id, string imie, string nazwisko, string telefon, string email, string stanowisko, int pensja) : base(imie, nazwisko, telefon, email, pensja)
        {
            this.id = id;
            this.stanowisko = stanowisko;
        }

        public override string ToString()
        {
            return base.ToString() + " " + stanowisko;
        }

        public void DodajZadanie(string zadanie, int id)
        {
            Zadanie noweZadanie = new Zadanie(id, zadanie);
            Zasób zasób = new Trwałe(id, "komputer", "lenovo", "elektronika", 2, 0);
            noweZadanie.PrzypiszZasób(zasób);
        }
        public string Stanowisko
        {
            get { return stanowisko; }
        }
    }
}
