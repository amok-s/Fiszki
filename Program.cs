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

        ekranGlowny();
        ekranKomend();

        while (!endApp)
        {
            string? komenda;
            komenda = Console.ReadLine().ToLower();


            switch (komenda)
            {
                case "dodaj":
                    komendaDodaj();
                    Console.WriteLine("Aby dodać kolejną fiszkę wpisz 'dodaj', albo skorzystaj z dowolnej innej komendy:");
                    Console.WriteLine();
                    Console.WriteLine(showKomendy());

                    string showKomendy()
                    {
                        var report = new StringBuilder();
                        foreach (var item in UI.ListOfCommands)
                        {
                            int n = UI.ListOfCommands.IndexOf(item);
                            if (n > 0)
                                report.AppendLine($"{item.nazwaKomendy.ToUpper()}\t\t{item.definicjaKomendy}.");
                        }
                        return report.ToString();
                    }
                    Console.WriteLine();
                    break;

                case "nauka":
                    komendaNauka();
                    break;

                case "koniec":

                    endApp = true;
                    break;

                case "fiszki":
                    komendaFiszki();
                    break;
                default:
                    Console.WriteLine("\r\nWpisano nieprawdiłową frazę! ");
                    Console.WriteLine("Aby dodać fiszkę wpisz 'dodaj', aby zacząć się uczyć wpisz 'nauka'.");
                    Console.WriteLine("Aby zakończyć aplikację wpisz 'koniec'.");
                    break;
            }
            if (komenda == "koniec") endApp = true;
        }   // Main menu

    }

    static void komendaDodaj()
    {
        Console.Clear();
        ekranGlowny();

        bool aNotEmpty = false;
        bool cNotEmpty = false;
        string a, b, c, d;
        a = string.Empty;
        c = string.Empty;

        while (!aNotEmpty)
        {
            Console.WriteLine("\r\nPodaj słówko/frazę w pierwszym języku:");
            a = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(a))
            {
                Console.WriteLine("Słowo lub fraza nie mogą być puste!");
            }
            else
            {
                var isDuplicate = allFiszki.FirstOrDefault(x => x.NativePhrase == a);

                if (isDuplicate != null)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Fiszka z frazą {a} już istnieje!");
                }
                else
                {
                    aNotEmpty = true;
                }
            }
            
        }

        Console.WriteLine($"\r\nDodaj przykład użycia \"{a}\" w zdaniu:");
        b = Console.ReadLine();

        while (!cNotEmpty)
        {
            Console.WriteLine($"\r\n\"{a}\" w drugim języku to:");
            c = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(c))
            {
                Console.WriteLine("Słowo lub fraza nie mogą być puste!");
            }
            else
            {
                cNotEmpty = true;
            }

        }

        Console.WriteLine("\r\nPrzetłumaczone użycie w zdaniu to:");
        d = Console.ReadLine();

        var nowaFiszka = new Fiszka(a, b, c, d);

        nowaFiszka.allScores.Add(2.05);
        allFiszki.Add(nowaFiszka);

        DataSerializer dataSerializer = new DataSerializer();
        dataSerializer.BinarySerialize(nowaFiszka, @$"data\data{allFiszki.Count - 1}.save");


        Console.Clear();
        ekranGlowny();


        Console.WriteLine();
        Console.WriteLine("Dodana fiszka to:");
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine(nowaFiszka.ShowFiszka());
        Console.WriteLine("----------------------------------------------------------");

    }

    static void komendaFiszki()
    {
        if (allFiszki.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Nie masz jeszcze żadnych fiszek!");
            Console.WriteLine("Aby dodać fiszkę wpisz 'dodaj'");
            Console.WriteLine();
        }

        else
        {
            bool przegladFiszek = false;

            int r = 0;

            while (!przegladFiszek)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"tryb przeglądania fiszek".ToUpper());
                Console.WriteLine("Aby przejść do następnej fiszki wciśnij 'm',");
                Console.WriteLine("aby pokazać wcześniejszą wciśnij 'n'.");
                Console.WriteLine("Aby usunąć daną fiszkę wciśnij 'd'");
                Console.WriteLine("Aby wyjść z obecnego trybu wciśnij 'q'.");
                Console.WriteLine("--------------------------------------\r\n");

                Console.WriteLine(allFiszki[r].ShowFiszka());
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"(Fiszka {r + 1} z {allFiszki.Count})");
                var i = allFiszki[r].MemoScore;
                i = Math.Round(i, 2);
                Console.WriteLine($"Poziom zapamiętania: {i}");

                var komenda = Console.ReadKey(true);

                switch (komenda.KeyChar)
                {
                    case 'm':
                        if (r + 1 > allFiszki.Count - 1)
                        {
                            r = 0;
                            break;
                        }
                        else
                        {
                            r = r + 1;
                            break;
                        }

                    case 'n':
                        if (r - 1 < 0)
                        {
                            r = allFiszki.Count - 1;
                            break;
                        }
                        else
                        {
                            r = r - 1;
                            break;
                        }

                    case 'd':
                        allFiszki.RemoveAt(r);

                        File.Delete(@$"data\data{allFiszki.Count}.save");
                        DataSerializer dataSerializer = new DataSerializer();
                        int f = 0;
                        foreach (var Fiszka in allFiszki)
                        {
                            dataSerializer.BinarySerialize(Fiszka, @$"data\data{f}.save");
                            f++;
                        }

                         if (allFiszki.Count == 0)
                            {
                                przegladFiszek = true;
                                break;
                            }
                         else
                        {
                            if (r == allFiszki.Count)
                            {
                                r = r - 1;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                         

                    case 'q':
                        przegladFiszek = true;
                        break;
                }

            }

            Console.Clear();
            ekranGlowny();
            ekranKomend();


        }
    }

    static void komendaNauka()
    {
        if (allFiszki.Count == 0)
        {
            Console.WriteLine("\nNie masz jeszcze żadnych fiszek!");
            Console.WriteLine("Aby dodać fiszkę wpisz 'dodaj'");
            Console.WriteLine();
        }

        else
        {
            bool koniecNauki = false;
            int poprzedniaFiszka = 0;

            while (!koniecNauki)
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"tryb nauki".ToUpper());
                Console.WriteLine("Aby wyświelić podpowiedź wciśnij 'm',");
                Console.WriteLine("aby wyświetlić całą fiszkę wciśnij 'n'.");
                Console.WriteLine("Po odsłonięciu fiszki oceń jej poziom zapamiętania w skali 1-5 gdzie:");
                Console.WriteLine("1 = w ogóle nie zapamiętana, 5 = doskonale zapamiętana.");
                Console.WriteLine("Aby wyjść z nauki w jej trakcie wciśnij 'q'.");
                Console.WriteLine("--------------------------------------\r\n");

                int r = 0;
                bool weightedGenerator = false;

                while (!weightedGenerator)
                {
                    if (allFiszki.Count == 1)
                    {
                        weightedGenerator = true;
                    }
                    else
                    {
                        Random random = new Random();

                        double rnd = random.Next(allFiszki.Count) * (allFiszki.Count * Math.Pow(random.NextDouble(), 1.2));
                        r = (Convert.ToInt32((Math.Round(rnd)) - 1));

                        if (r >= 0 && r < allFiszki.Count)
                        {
                            if (r != poprzedniaFiszka) weightedGenerator = true;
                        }
                    }
                }
                poprzedniaFiszka = r;
                Console.WriteLine(allFiszki[r].NativePhrase.ToUpper());

                var komenda = Console.ReadKey(true);

                void Ocenianie()
                {

                    bool koniecOceniania = false;

                    while (!koniecOceniania)
                    {
                        var UserInput = Console.ReadKey(true);

                        if (UserInput.KeyChar == 'q')
                        {
                            koniecOceniania = true;
                            koniecNauki = true;
                        }

                        else
                        {
                            if (char.IsDigit(UserInput.KeyChar))
                            {
                                switch (UserInput.KeyChar)
                                {
                                    case '1':
                                        allFiszki[r].allScores.Add(1D);
                                        break;
                                    case '2':
                                        allFiszki[r].allScores.Add(2D);
                                        break;
                                    case '3':
                                        allFiszki[r].allScores.Add(3D);
                                        break;
                                    case '4':
                                        allFiszki[r].allScores.Add(4D);
                                        break;
                                    case '5':
                                        allFiszki[r].allScores.Add(5D);
                                        break;
                                    default:
                                        Console.WriteLine("Wciśnij cyfrę w przedziale 1-5!");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Nie wprowadzono cyfry!");

                            }
                            allFiszki.OrderBy(x => x.MemoScore).ToList();


                            DataSerializer dataSerializer = new DataSerializer();
                            int f = 0;
                            foreach (var Fiszka in allFiszki)
                            {
                                dataSerializer.BinarySerialize(Fiszka, @$"data\data{f}.save");
                                f++;
                            }

                            if (UserInput.KeyChar == '1' || UserInput.KeyChar == '2') koniecOceniania = true;
                            if (UserInput.KeyChar == '3' || UserInput.KeyChar == '4' || UserInput.KeyChar == '5') koniecOceniania = true;
                        }
                    }
                }


                switch (komenda.KeyChar)
                {

                    case 'm':
                        Console.WriteLine(allFiszki[r].NativePhraseExample);
                        komenda = Console.ReadKey(true);

                        switch (komenda.KeyChar)
                        {
                            case 'm':
                            case 'n':
                                Console.WriteLine();
                                Console.WriteLine(allFiszki[r].TranslatedPhrase.ToUpper());
                                Console.WriteLine(allFiszki[r].TranslatedPhraseExample);
                                Console.WriteLine();
                                Console.WriteLine("Oceń zapamiętanie w skali 1-5 wciskając odpowiednią cyfrę.");
                                Ocenianie();
                                break;
                        }

                        break;

                    case 'n':
                        Console.WriteLine(allFiszki[r].NativePhraseExample);
                        Console.WriteLine();
                        Console.WriteLine(allFiszki[r].TranslatedPhrase.ToUpper());
                        Console.WriteLine(allFiszki[r].TranslatedPhraseExample);
                        Console.WriteLine();
                        Console.WriteLine("Oceń zapamiętanie w skali 1-5 wciskając odpowiednią cyfrę.");
                        Ocenianie();
                        break;

                }

                if (komenda.KeyChar == 'q') koniecNauki = true;
            }
            Console.Clear();
            ekranGlowny();
            ekranKomend();
        }
    }

    static void ekranGlowny()
    {
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("FISZKI");
        Console.WriteLine("Program pomaga tworzyć i zapamiętywać fiszki.");
        Console.WriteLine("Każdą fiszkę możesz ocenić w skali 1-5 w zależności od tego, jak dobrze ją zapamiętałeś_aś.");
        Console.WriteLine("Fiszki zapamiętane słabo będą się pojawiać częściej w rotacji.");
        Console.WriteLine("----------------------------------------------------------");
    }

    static void ekranKomend()
    {
        string showKomendy()
        {
            var report = new StringBuilder();
            foreach (var item in UI.ListOfCommands)
            {
                report.AppendLine($"{item.nazwaKomendy.ToUpper()}\t\t{item.definicjaKomendy}.");
            }
            return report.ToString();
        }


        Console.WriteLine("Aby przejść dalej wpisz komendę i wciśnij ENTER:");
        Console.WriteLine();

        Console.WriteLine(showKomendy());
    }

    

  
    


}
