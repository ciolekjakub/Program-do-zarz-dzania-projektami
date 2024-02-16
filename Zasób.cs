using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Zasób
    {
        public int id;
        private string nazwa;
        private string producent;
        private string typ;
        private int ilość;
        private int ilosc;

        public Zasób(string nazwa, string producent, string typ, int ilosc)
        {
            this.nazwa = nazwa;
            this.producent = producent;
            this.typ = typ;
            this.ilosc = ilosc;
        }

        public Zasób(int id, string nazwa, string producent, string typ, int ilość)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.producent = producent;
            this.typ = typ;
            this.ilość = ilość;
        }

        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; }
        }

        public string Producent
        {
            get { return producent; }
            set { producent = value; }
        }

        public string Typ
        {
            get { return typ; }
            set { typ = value; }
        }

        public int Ilość
        {
            get { return ilość; }
            set { ilość = value; }
        }
        public override string ToString()
        {
            return "Zasób: " + nazwa + " | " + producent + " | " + typ + " | " + ilość +"|";
        }
    }
}
