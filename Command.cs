using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    public class Command
    {
        public string commandName { get; set; }
        public string commandDescription { get; set; }

        public Command(string commandName, string commandDescription)
        {
            this.commandName = commandName;
            this.commandDescription = commandDescription;
        }
    }
}
