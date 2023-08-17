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
        }   // Main menu commands

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
        }

        public void MainMenu()
        {

            
        }
    }
}
