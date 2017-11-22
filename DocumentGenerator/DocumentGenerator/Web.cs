using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGenerator{
    public class Web
    {
        private string _Nombre, _Firma;
        private List<double> _Values;

        public Web(string Nombre, string Firma, List<double> values)
        {
            this.Nombre = Nombre;
            this.Firma = Firma;
            this.Values = values;
        }

        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Firma { get => _Firma; set => _Firma = value; }
        public List<double> Values { get => _Values; set => _Values = value; }

        public string generarFirma()
        {
            string cadena = "";
            generarNgramas(7,ref cadena);
            generarNgramas(6, ref cadena);
            generarNgramas(5, ref cadena);
            generarNgramas(4, ref cadena);
            generarNgramas(3, ref cadena);
            return cadena;
        }

        private void generarNgramas(int ngrama,ref string cadena)
        {
            for (int i = 0; i < Firma.Length - ngrama; i++)
            {
                cadena += Firma.Substring(i, ngrama)+" ";
            }
        }

        public override string ToString()
        {
            string cadena = "{\"Nombre\":\"" + Nombre + "\",\"Firma\":\"" + generarFirma() + "\"}";
            return cadena;
        }

    }
}
