using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace SõidukiteLiidesePuhtejev
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            List<ISoiduk> sõidukid = new List<ISoiduk>();
            Console.WriteLine("--- Sõidukite haldussüsteem ---");

            string failTee = "soidukid.txt";

            if (File.Exists(failTee))
            {
                string[] read = File.ReadAllLines(failTee);
                foreach (string rida in read)
                {
                    if (string.IsNullOrWhiteSpace(rida)) continue;

                    string[] osad = rida.Split(';');
                    try
                    {
                        switch (osad[0].ToLower())
                        {
                            case "auto":
                                sõidukid.Add(new Auto(double.Parse(osad[1]), double.Parse(osad[2])));
                                break;
                            case "jalgratas":
                                sõidukid.Add(new Jalgrattas(double.Parse(osad[1])));
                                break;
                            case "buss":
                                sõidukid.Add(new Buss(double.Parse(osad[1]), double.Parse(osad[2]), int.Parse(osad[3])));
                                break;
                            case "mootorratas":
                                sõidukid.Add(new Mootorratas(double.Parse(osad[1]), double.Parse(osad[2])));
                                break;
                        }
                        Console.WriteLine($"Loetud failist: {osad[0]}");
                    }
                    catch
                    {
                        Console.WriteLine($"Vigane rida failis: {rida}");
                    }
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Vali sõiduki tüüp: 1 - Auto, 2 - Jalgratas, 3 - Buss, 4 - Mootorratas, 0 - Lõpeta");
                    string valik = Console.ReadLine();
                    if (valik == "0") break;

                    switch (valik)
                    {
                        case "1":
                            Console.Write("Sisesta kulu 1 km kohta: ");
                            double autoKulu = double.Parse(Console.ReadLine());
                            Console.Write("Sisesta teepikkus (km): ");
                            double autoKm = double.Parse(Console.ReadLine());

                            sõidukid.Add(new Auto(autoKulu, autoKm));
                            break;

                        case "2":
                            Console.Write("Sisesta läbitud vahemaa (km): ");
                            double ratasKm = double.Parse(Console.ReadLine());

                            sõidukid.Add(new Jalgrattas(ratasKm));
                            break;

                        case "3":
                            Console.Write("Sisesta kulu 1 km kohta: ");
                            double bussKulu = double.Parse(Console.ReadLine());
                            Console.Write("Sisesta vahemaa (km): ");
                            double bussKm = double.Parse(Console.ReadLine());
                            Console.Write("Sisesta reisijate arv: ");
                            int reisijad = int.Parse(Console.ReadLine());

                            sõidukid.Add(new Buss(bussKulu, bussKm, reisijad));
                            break;

                        case "4":
                            Console.Write("Sisesta kulu 1 km kohta: ");
                            double motoKulu = double.Parse(Console.ReadLine());
                            Console.Write("Sisesta vahemaa (km): ");
                            double motoKm = double.Parse(Console.ReadLine());

                            sõidukid.Add(new Mootorratas(motoKulu, motoKm));
                            break;

                        default:
                            Console.WriteLine("Vale valik, proovi uuesti.");
                            continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Viga: Palun sisesta ainult numbreid!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Tekkis ootamatu viga: {ex.Message}");
                }
            }

            Console.WriteLine("--- TULEMUSED ---");
            double kokkuKulu = 0;

            foreach (ISoiduk s in sõidukid)
            {
                Console.WriteLine(s.ToString());
                kokkuKulu += s.ArvutaKulu();
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Kogu kulu kõikide sõidukite peale: {kokkuKulu:F2}€");

            using (StreamWriter sw = new StreamWriter(failTee))
            {
                foreach (ISoiduk s in sõidukid)
                {
                    if (s is Auto a)
                        sw.WriteLine($"auto;{a.Kutusekulu};{a.Teepikkus}");
                    else if (s is Jalgrattas j)
                        sw.WriteLine($"jalgratas;{j.Vahemaa}");
                    else if (s is Buss b)
                        sw.WriteLine($"buss;{b.KutuseKulu};{b.Vahemaa};{b.Reisijad}");
                    else if (s is Mootorratas m)
                        sw.WriteLine($"mootorratas;{m.Kutusekulu};{m.Vahemaa}");
                }
            }
            Console.WriteLine($"Salvestatud faili: {failTee}");

            Console.WriteLine("Programmi lõpetamiseks vajuta suvalist klahvi...");
            Console.ReadKey();
        }
    }
}