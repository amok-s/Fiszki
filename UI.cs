using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    internal class UI
    {
        public List<Command> ListOfCommands = new List<Command>();

        public UI()
        {
            InitializeMainMenu();
        }

        private void InitializeMainMenu()
        {
            var komDodaj = new Command("dodaj", "aby dodać fiszki");
            var komFiszki = new Command("fiszki", "aby przeglądać fiszki oraz je usuwać");
            var komNauka = new Command("nauka", "aby zacząć naukę");
            var komKoniec = new Command("koniec", "aby zapisać i zakończyć program");
            ListOfCommands.Add(komDodaj);
            ListOfCommands.Add(komFiszki);
            ListOfCommands.Add(komNauka);
            ListOfCommands.Add(komKoniec);
        }   // Initialize main menu commands

        public void TitleCard()
        {
            string title = @"                                                                                           
                                                                                           
FFFFFFFFFFFFFFFFFFFFFF  iiii                                    kkkkkkkk             iiii  
F::::::::::::::::::::F i::::i                                   k::::::k            i::::i 
F::::::::::::::::::::F  iiii                                    k::::::k             iiii  
FF::::::FFFFFFFFF::::F                                          k::::::k                   
  F:::::F       FFFFFFiiiiiii     ssssssssss   zzzzzzzzzzzzzzzzz k:::::k    kkkkkkkiiiiiii 
  F:::::F             i:::::i   ss::::::::::s  z:::::::::::::::z k:::::k   k:::::k i:::::i 
  F::::::FFFFFFFFFF    i::::i ss:::::::::::::s z::::::::::::::z  k:::::k  k:::::k   i::::i 
  F:::::::::::::::F    i::::i s::::::ssss:::::szzzzzzzz::::::z   k:::::k k:::::k    i::::i 
  F:::::::::::::::F    i::::i  s:::::s  ssssss       z::::::z    k::::::k:::::k     i::::i 
  F::::::FFFFFFFFFF    i::::i    s::::::s           z::::::z     k:::::::::::k      i::::i 
  F:::::F              i::::i       s::::::s       z::::::z      k:::::::::::k      i::::i 
  F:::::F              i::::i ssssss   s:::::s    z::::::z       k::::::k:::::k     i::::i 
FF:::::::FF           i::::::is:::::ssss::::::s  z::::::zzzzzzzzk::::::k k:::::k   i::::::i
F::::::::FF           i::::::is::::::::::::::s  z::::::::::::::zk::::::k  k:::::k  i::::::i
F::::::::FF           i::::::i s:::::::::::ss  z:::::::::::::::zk::::::k   k:::::k i::::::i
FFFFFFFFFFF           iiiiiiii  sssssssssss    zzzzzzzzzzzzzzzzzkkkkkkkk    kkkkkkkiiiiiiii
                                                                                           
                                                                       by Arkadiusz Stanek";

            Console.WriteLine(title);
            Console.WriteLine();
            Console.WriteLine("Aby kontynuować wciśnij dowolny przycisk.");
            Console.ReadKey(true);
            Console.Clear();
        }                      // title card with app name and creator
        public string showCommands()
        {
            var report = new StringBuilder();
            foreach (var item in ListOfCommands)
            {
                report.AppendLine($"{item.commandName.ToUpper()}\t\t{item.commandDescription}.");
            }
            return report.ToString();
        }        

        public void CommandsMenu()
        {
            Console.WriteLine("Aby przejść dalej wpisz komendę i wciśnij ENTER:");
            Console.WriteLine();

            Console.WriteLine(showCommands());
        } // showing main menu commands

        public void MainScreen()   // main app screen, that shows initial message box
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("FISZKI");
            Console.WriteLine("Program pomaga tworzyć i zapamiętywać fiszki.");
            Console.WriteLine("Każdą fiszkę możesz ocenić w skali 1-5 w zależności od tego, jak dobrze ją zapamiętałeś_aś.");
            Console.WriteLine("Fiszki zapamiętane słabo będą się pojawiać częściej w rotacji.");
            Console.WriteLine("----------------------------------------------------------");
        }

        #region ReadCommands
        public void ReadCommandAdd()
        {
            Console.Clear();
            MainScreen();

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
                    var isDuplicate = Program.allFiszki.FirstOrDefault(x => x.NativePhrase == a);

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
            Program.allFiszki.Add(nowaFiszka);

            DataSerializer dataSerializer = new DataSerializer();
            dataSerializer.BinarySerialize(nowaFiszka, @$"data\data{Program.allFiszki.Count - 1}.save");


            Console.Clear();
            MainScreen();


            Console.WriteLine();
            Console.WriteLine("Dodana fiszka to:");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(nowaFiszka.ShowFiszka());
            Console.WriteLine("----------------------------------------------------------");

        }

        public void ReadCommandFiszki()
        {
            if (Program.allFiszki.Count == 0)
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

                    Console.WriteLine(Program.allFiszki[r].ShowFiszka());
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine($"(Fiszka {r + 1} z {Program.allFiszki.Count})");
                    var i = Program.allFiszki[r].MemoScore;
                    i = Math.Round(i, 2);
                    Console.WriteLine($"Poziom zapamiętania: {i}");

                    var komenda = Console.ReadKey(true);

                    switch (komenda.KeyChar)
                    {
                        case 'm':
                            if (r + 1 > Program.allFiszki.Count - 1)
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
                                r = Program.allFiszki.Count - 1;
                                break;
                            }
                            else
                            {
                                r = r - 1;
                                break;
                            }

                        case 'd':
                            Program.allFiszki.RemoveAt(r);

                            File.Delete(@$"data\data{Program.allFiszki.Count}.save");
                            DataSerializer dataSerializer = new DataSerializer();
                            int f = 0;
                            foreach (var Fiszka in Program.allFiszki)
                            {
                                dataSerializer.BinarySerialize(Fiszka, @$"data\data{f}.save");
                                f++;
                            }

                            if (Program.allFiszki.Count == 0)
                            {
                                przegladFiszek = true;
                                break;
                            }
                            else
                            {
                                if (r == Program.allFiszki.Count)
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
                MainScreen();
                CommandsMenu();


            }
        }

        public void ReadCommandStudy()
        {
            if (Program.allFiszki.Count == 0)
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
                        if (Program.allFiszki.Count == 1)
                        {
                            weightedGenerator = true;
                        }
                        else
                        {
                            Random random = new Random();

                            double rnd = random.Next(Program.allFiszki.Count) * (Program.allFiszki.Count * Math.Pow(random.NextDouble(), 1.2));
                            r = (Convert.ToInt32((Math.Round(rnd)) - 1));

                            if (r >= 0 && r < Program.allFiszki.Count)
                            {
                                if (r != poprzedniaFiszka) weightedGenerator = true;
                            }
                        }
                    }
                    poprzedniaFiszka = r;
                    Console.WriteLine(Program.allFiszki[r].NativePhrase.ToUpper());

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
                                            Program.allFiszki[r].allScores.Add(1D);
                                            break;
                                        case '2':
                                            Program.allFiszki[r].allScores.Add(2D);
                                            break;
                                        case '3':
                                            Program.allFiszki[r].allScores.Add(3D);
                                            break;
                                        case '4':
                                            Program.allFiszki[r].allScores.Add(4D);
                                            break;
                                        case '5':
                                            Program.allFiszki[r].allScores.Add(5D);
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
                                Program.allFiszki.OrderBy(x => x.MemoScore).ToList();


                                DataSerializer dataSerializer = new DataSerializer();
                                int f = 0;
                                foreach (var Fiszka in Program.allFiszki)
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
                            Console.WriteLine(Program.allFiszki[r].NativePhraseExample);
                            komenda = Console.ReadKey(true);

                            switch (komenda.KeyChar)
                            {
                                case 'm':
                                case 'n':
                                    Console.WriteLine();
                                    Console.WriteLine(Program.allFiszki[r].TranslatedPhrase.ToUpper());
                                    Console.WriteLine(Program.allFiszki[r].TranslatedPhraseExample);
                                    Console.WriteLine();
                                    Console.WriteLine("Oceń zapamiętanie w skali 1-5 wciskając odpowiednią cyfrę.");
                                    Ocenianie();
                                    break;
                            }

                            break;

                        case 'n':
                            Console.WriteLine(Program.allFiszki[r].NativePhraseExample);
                            Console.WriteLine();
                            Console.WriteLine(Program.allFiszki[r].TranslatedPhrase.ToUpper());
                            Console.WriteLine(Program.allFiszki[r].TranslatedPhraseExample);
                            Console.WriteLine();
                            Console.WriteLine("Oceń zapamiętanie w skali 1-5 wciskając odpowiednią cyfrę.");
                            Ocenianie();
                            break;

                    }

                    if (komenda.KeyChar == 'q') koniecNauki = true;
                }
                Console.Clear();
                MainScreen();
                CommandsMenu();
            }
        } 
        #endregion // what happens after typing each command
    }
}
