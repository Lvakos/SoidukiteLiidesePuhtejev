using System;
using System.Collections.Generic;
using System.Globalization; // VÄGA OLULINE: Vajalik InvariantCulture jaoks
using System.IO;

namespace SõidukiteLiidesePuhtejev
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<ISoiduk> soidukid = new List<ISoiduk>();
            string fail = "andmed.txt";

            if (!File.Exists(fail))
            {
                Console.WriteLine($"Faili '{fail}' ei leitud. Loon uue faili näidisandmetega...");

                string[] naidisAndmed = new string[]
                {
                    "Auto, 0.15, 120",
                    "Jalgratas, 25",
                    "Buss, 0.80, 200, 40",
                    "Mootorratas, 0.08, 80"
                };

                File.WriteAllLines(fail, naidisAndmed);
                Console.WriteLine("Fail edukalt loodud! Jätkan andmete töötlemist.\n");
            }

            string[] read = File.ReadAllLines(fail);

            foreach (string rida in read)
            {
                if (string.IsNullOrWhiteSpace(rida)) continue;

                string[] osad = rida.Split(',');

                try
                {
                    string tyyp = osad[0].Trim();

                    if (tyyp == "Auto")
                    {
                        // LISATUD: CultureInfo.InvariantCulture tagab, et punktiga arvud loetakse õigesti
                        double kulu = double.Parse(osad[1].Trim(), CultureInfo.InvariantCulture);
                        double vahemaa = double.Parse(osad[2].Trim(), CultureInfo.InvariantCulture);
                        soidukid.Add(new Auto(kulu, vahemaa));
                    }
                    else if (tyyp == "Jalgratas")
                    {
                        double vahemaa = double.Parse(osad[1].Trim(), CultureInfo.InvariantCulture);
                        soidukid.Add(new Jalgrattas(vahemaa));
                    }
                    else if (tyyp == "Buss")
                    {
                        double kulu = double.Parse(osad[1].Trim(), CultureInfo.InvariantCulture);
                        double vahemaa = double.Parse(osad[2].Trim(), CultureInfo.InvariantCulture);
                        int reisijad = int.Parse(osad[3].Trim()); // Täisarvudel pole täppi/koma, invariant pole rangelt vajalik
                        soidukid.Add(new Buss(kulu, vahemaa, reisijad));
                    }
                    else if (tyyp == "Mootorratas")
                    {
                        double kulu = double.Parse(osad[1].Trim(), CultureInfo.InvariantCulture);
                        double vahemaa = double.Parse(osad[2].Trim(), CultureInfo.InvariantCulture);
                        soidukid.Add(new Mootorratas(kulu, vahemaa));
                    }
                    else
                    {
                        Console.WriteLine($"Tundmatu sõidukitüüp real: {rida}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Viga rea töötlemisel ({rida}): {ex.Message}");
                }
            }

            // Tulemuste kuvamine
            Console.WriteLine("=== SÕIDUKID ===");
            double koguKulu = 0;

            foreach (ISoiduk s in soidukid)
            {
                Console.WriteLine(s.ToString());
                koguKulu += s.ArvutaKulu();
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Kõikide sõidukite kogukulu: {koguKulu:F2} €");
            Console.ReadKey();
        }
    }
}