using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    public class Command
    {
        public string nazwaKomendy { get; set; }
        public string definicjaKomendy { get; set; }

        public Command(string nazwaKomendy, string definicjaKomendy)
        {
            this.nazwaKomendy = nazwaKomendy;
            this.definicjaKomendy = definicjaKomendy;
        }
    }
}
