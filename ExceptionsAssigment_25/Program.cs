using System;
using System.IO;

namespace ExceptionsAssignment_25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = PromptNonEmpty("Anna tiedoston nimi (polku sallittu): ");

            //Tarkistetaan, että onko tiedosto on olemassa
            if (File.Exists(fileName))
            {
                string content = ReadFile(fileName);
                Console.WriteLine("\nTiedoston sisältö:");
                Console.WriteLine(content);
            }
            //Muutoin luodaan uusi
            else
            {
                Console.WriteLine("Tiedostoa ei löytynyt.");

                bool create = AskYesNo("Haluatko luoda uuden tiedoston? (yes/no): ");
                if (create)
                {
                    //RISKIKOHTA: arvo voi olla null, joten hyvä tehdä null tarkastus ennen, kuin kirjoitetaan tiedostoon
                    string? contentToWrite = Prompt("Anna teksti, joka tallennetaan uuteen tiedostoon: ");

                  
                    bool wasWriteSuccess = WriteFile(fileName, contentToWrite);

                    //Ilmoitus käyttäjälle, jos ei onnistu

                    Console.WriteLine($"Tiedosto '{fileName}' luotu ja tallennettu.");
                }
                else
                {
                    Console.WriteLine("Tiedostoa ei luotu.");
                }
            }

         
            Console.WriteLine("Ohjelma päättyi.");
        }

        
        /// <summary>
        /// Näyttää viestin sekä palauttaa käyttäjän vastauksen
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string? Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static string PromptNonEmpty(string message)
        {
            while (true)
            {
                string? input = Prompt(message);
                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Virhe: Arvo ei voi olla tyhjä. Yritä uudelleen.");
            }
        }

        private static bool AskYesNo(string message)
        {
            while (true)
            {
                //Trim poistaa ylimääräiset tyhjät merkit
                string answer = (Prompt(message) ?? "").Trim().ToLowerInvariant();

                //toinen tapa tehdä if lauseita, jos ei halua käyttää &&/||
                if (answer is "y" or "yes" or "k" or "kyllä") 
                    return true;
                if (answer is "n" or "no" or "ei") 
                    return false;

                Console.WriteLine("Anna vastaus muodossa yes/no (y/n, kyllä/ei).");
            }
        }

        /// <summary>
        /// Lukee tiedoston polusta
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ReadFile(string path)
        {
            //RISKIKOHTA: Tämä voi heittää virheitä: FileNotFound, UnauthorizedAccess, IOException
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Kirjoittaa tiedostoon
        /// </summary>
        /// <param name="path">Mihin kirjoitetaan</param>
        /// <param name="content">Mitä kirjoitetaan</param>
        private static bool WriteFile(string path, string content)
        {
            //RISKIKOHTA: Tämä voi heittää virheitä: DirectoryNotFound, UnauthorizedAccess, IOException
            File.WriteAllText(path, content);

            return true;
        }
    }
}
