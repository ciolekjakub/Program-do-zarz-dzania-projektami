using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Projekt
    {
        public int id;
        public Manager manager;
        public List<Zadanie> zadania;
        string nazwa;
        string opis;
        DateTime dataRozpoczecia;
        DateTime deadLine;
        
        public Projekt(int id, string nazwa, string opis, DateTime dataRozpoczecia, DateTime deadLine)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.opis = opis;
            this.dataRozpoczecia = dataRozpoczecia;
            this.deadLine = deadLine;
            zadania = new List<Zadanie>();
        }

        public void DodajZadanie(Zadanie zadanie)
        {
            zadania.Add(zadanie);
        }

        /*public void UsunZadanie(Zadanie zadanie)
        {
            zadania.Remove(zadanie);
        }*/

        public void PrzypiszManagera(Manager manager)
        {
            this.manager = manager;
        }

        public void WyswietlZadania()
        {
            foreach (Zadanie zadanie in zadania)
            {
                Console.WriteLine(zadanie);
            }
        }

        public override string ToString()
        {
            return "Projekt: " + nazwa + " | " + opis + " | " + dataRozpoczecia + " | " + deadLine;
        }
        public string Nazwa
        {
            get { return nazwa; }
        }
        public Manager Manager
        {
            get { return manager; }
        }
        public List<Zadanie> Zadania
        {
            get { return zadania; }
        }
        public DateTime DataRozpoczecia
        {
            get { return dataRozpoczecia; }
        }
        public DateTime DeadLine
        {
            get { return deadLine; }
        }
        public string Opis
        {
            get { return opis; }
        }
        
    }
}
