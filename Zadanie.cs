using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Zadanie
    {
        public int id;
        private string nazwa;
        public List<Zasób> zasoby;
        private string zadanie;

        public Zadanie(int id, string nazwa)
        {
            this.id = id;
            zasoby = new List<Zasób>();
            this.nazwa = nazwa;
        }

        public Zadanie(string zadanie)
        {
            this.zadanie = zadanie;
        }

        public void PrzypiszZasób(Zasób zasób)
        {
            zasoby.Add(zasób);
        }

        public void UsuńZasób(Zasób zasób)
        {
            zasoby.Remove(zasób);
        }

        public override string ToString()
        {
            string zasobyString = "";
            foreach (Zasób zasób in zasoby)
            {
                zasobyString += zasób.ToString() + "\n";
            }
            return "Zadanie: "+ "|" + nazwa + "|" + "\nZasoby: " +"|"+ zasobyString;
        }
        public string Nazwa
        {
            get { return nazwa; }
        }

        public List<Zasób> Zasoby
        {
            get { return zasoby; }
        }
    }
}
