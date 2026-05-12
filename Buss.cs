using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SõidukiteLiidesePuhtejev
{
    internal class Buss : ISoiduk
    {
        public double KutuseKulu { get; set; }
        public double Vahemaa { get; set; }
        public int Reisijad { get; set; }

        public Buss(double kutuseKulu, double vahemaa, int reisijad)
        {
            KutuseKulu = kutuseKulu;
            Vahemaa = vahemaa;
            Reisijad = reisijad;
        }

        public double ArvutaKulu()
        {
            double koguKulu = KutuseKulu * Vahemaa;
            return koguKulu / Reisijad;
        }

        public double ArvutaVahemaa()
        {
            return Vahemaa;
        }

        public override string ToString()
        {
            return $"Buss | Vahemaa: {ArvutaVahemaa():F2} km | Reisijad: {Reisijad} | Kulu inimese kohta: {ArvutaKulu():F2} €";
        }
    }
}
