using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SõidukiteLiidesePuhtejev
{
    public class Jalgrattas : ISoiduk
    {
        public double Vahemaa { get; set; }

        public Jalgrattas(double vahemaa)
        {
            Vahemaa = vahemaa;
        }

        public double ArvutaKulu()
        {
            return 0;
        }

        public double ArvutaVahemaa()
        {
            return Vahemaa;
        }

        public override string ToString()
        {
            return $"Jalgratas | Vahemaa: {ArvutaVahemaa():F2} km | Kulu: {ArvutaKulu():F2}€";
        }
    }
}
