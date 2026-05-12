namespace SõidukiteLiidesePuhtejev
{
    internal class Program
    {
        static void Main()
        {
            List<ISoiduk> soidukid = new List<ISoiduk>();

            string fail = "andmed.txt";

            if (!File.Exists(fail))
            {
                Console.WriteLine("Faili ei leitud");
                return;
            }

            string[] read = File.ReadAllLines(fail);

            foreach (string rida in read)
            {
                string[] osad = rida.Split(',');

                try
                {
                    if (osad[0] == "Auto")
                    {
                        double kulu = double.Parse(osad[1]);
                        double vahemaa = double.Parse(osad[2]);

                        soidukid.Add(new Auto(kulu, vahemaa));
                    }
                    else if (osad[0] == "Jalgratas")
                    {
                        double vahemaa = double.Parse(osad[1]);

                        soidukid.Add(new Jalgrattas(vahemaa));
                    }
                    else if (osad[0] == "Buss")
                    {
                        double kulu = double.Parse(osad[1]);
                        double vahemaa = double.Parse(osad[2]);
                        int reisijad = int.Parse(osad[3]);

                        soidukid.Add(new Buss(kulu, vahemaa, reisijad));
                    }
                    else
                    {
                        Console.WriteLine($"Tundmatu sõiduk: {osad[0]}");
                    }
                }
                catch
                {
                    Console.WriteLine($"Viga rea töötlemisel: {rida}");
                }
            }

            Console.WriteLine("=== SÕIDUKID ===");

            double koguKulu = 0;

            foreach (ISoiduk s in soidukid)
            {
                Console.WriteLine(s.ToString());

                koguKulu += s.ArvutaKulu();
            }

            Console.WriteLine();
            Console.WriteLine($"Kõikide sõidukite kogukulu: {koguKulu:F2} €");
        }
    }
}
