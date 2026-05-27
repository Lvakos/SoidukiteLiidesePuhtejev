using System;

namespace SõidukiteLiidesePuhtejev
{
    public class Mootorratas : ISoiduk
    {
        public double Vahemaa { get; set; }
        public double Kutusekulu { get; set; }

        public Mootorratas(double kutuseKulu, double vahemaa)
        {
            Kutusekulu = kutuseKulu;
            Vahemaa = vahemaa;
        }

        public double ArvutaKulu()
        {
            return Kutusekulu * Vahemaa;
        }

        public double ArvutaVahemaa()
        {
            return Vahemaa;
        }

        public override string ToString()
        {
            return $"Mootorratas | Vahemaa: {ArvutaVahemaa():F2} km | Kulu: {ArvutaKulu():F2} €";
        }
    }
}