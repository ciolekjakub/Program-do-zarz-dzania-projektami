using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektObiektowka
{
    internal class Materiały : Zasób
    {
        string czasPosiadania;

        public Materiały(int id, string nazwa, string producent, string typ, int ilość, string czasPosiadania) : base(id, nazwa, producent, typ, ilość)
        {
            this.czasPosiadania = czasPosiadania;
        }        
    }
}
