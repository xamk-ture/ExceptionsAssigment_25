using System;
using System.IO;

namespace ExceptionsAssigment_25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Anna tiedoston nimi (polku sallittu): ");
            string? fileName = Console.ReadLine();

           
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Virhe: Tiedoston nimi ei voi olla tyhjä.");
                return;
            }

            if (File.Exists(fileName))
            {
                // Voi kaatua esim. käyttöoikeuksiin/lockkiin/huonoon polkuun
                string content = File.ReadAllText(fileName);
                Console.WriteLine("\nTiedoston sisältö:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Tiedostoa ei löytynyt.");
                Console.Write("Haluatko luoda uuden tiedoston? (yes/no): ");
                string? choice = Console.ReadLine()?.Trim().ToLower();

                if (choice == "yes")
                {
                    Console.Write("Anna teksti, joka tallennetaan uuteen tiedostoon: ");
                    string contentToWrite = Console.ReadLine() ?? string.Empty;

                    // Voi kaatua esim. jos polku ei ole olemassa / ei oikeuksia / levy täynnä
                    File.WriteAllText(fileName, contentToWrite);
                    Console.WriteLine($"Tiedosto '{fileName}' luotu ja tallennettu.");
                }
                else
                {
                    Console.WriteLine("Tiedostoa ei luotu.");
                }
            }

            Console.WriteLine("Valmis.");
        }
    }
}
