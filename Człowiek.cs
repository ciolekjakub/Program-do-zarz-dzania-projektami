using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    public class Człowiek
    {
        string imie;
        string nazwisko;
        string telefon;
        string email;
        int pensja;

        public Człowiek(string imie, string nazwisko, string telefon, string email, int pensja)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.telefon = telefon;
            this.email = email;
            this.pensja = pensja;
        }

        public override string ToString()
        {
            return imie + "|" + nazwisko + "|" + telefon + "|" + email + "|" + pensja + "|";
        }

        public string Imie
        {
            get { return imie; }
        }

        public string Nazwisko
        {
            get { return nazwisko; }
        }

        public string Telefon
        {
            get { return telefon; }
        }

        public string Email
        {
            get { return email; }
        }

        public int Pensja
        {
            get { return pensja; }
        }

    }
}
