using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SõidukiteLiidesePuhtejev
{
    public class Auto : ISoiduk
    {
        public double Teepikkus { get; set; }

        public double Kutusekulu { get; set; }

        public Auto(double kutuseKulu, double teepikkus)
        {
            Kutusekulu = kutuseKulu;
            Teepikkus = teepikkus;
        }

        public double ArvutaKulu()
        {
            return Kutusekulu * Teepikkus;
        }

        public double ArvutaVahemaa()
        {
            return Teepikkus;
        }

        public override string ToString()
        {
            return $"Auto | Vahemaa: {ArvutaVahemaa():F2} km | Kulu: {ArvutaKulu():F2} €";
        }
    }
}
