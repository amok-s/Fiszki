namespace Fiszki;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel.Design;

internal class Program
{

    public static List<Fiszka> allFiszki = new List<Fiszka>();
    static Random rnd = new Random();
    static UI UI = new UI();

    static void Main(string[] args)
    {
        #region LoadingFiszkasFromAFile

        Directory.CreateDirectory(@"data");

        DataSerializer dataSerializer = new DataSerializer();
        Fiszka p = null;

        for (int f = 0; File.Exists(@$"data\data{f}.save"); f++)
        {
            p = dataSerializer.BinaryDeserialize(@$"data\data{f}.save") as Fiszka;
            allFiszki.Add(p);
        }
        

        allFiszki.OrderBy(x => x.MemoScore).ToList();
        #endregion

        UI.TitleCard();

        bool endApp = false;

        UI.MainScreen();
        UI.CommandsMenu();

        while (!endApp)
        {
            string? userInput;
            userInput = Console.ReadLine().ToLower();


            switch (userInput)
            {
                case "dodaj":
                    UI.ReadCommandAdd();
                    Console.WriteLine("Aby dodać kolejną fiszkę wpisz 'dodaj', albo skorzystaj z dowolnej innej komendy:");
                    Console.WriteLine();
                    Console.WriteLine(UI.showCommands());
                    Console.WriteLine();
                    break;

                case "nauka":
                    UI.ReadCommandStudy();
                    break;

                case "koniec":

                    endApp = true;
                    break;

                case "fiszki":
                    UI.ReadCommandFiszki();
                    break;
                default:
                    Console.WriteLine("\r\nWpisano nieprawdiłową frazę! ");
                    Console.WriteLine("Aby dodać fiszkę wpisz 'dodaj', aby zacząć się uczyć wpisz 'nauka'.");
                    Console.WriteLine("Aby zakończyć aplikację wpisz 'koniec'.");
                    break;
            }
            if (userInput == "koniec") endApp = true;
        }   // user input control

    }

    

   

    

    

    

    

  
    


}
