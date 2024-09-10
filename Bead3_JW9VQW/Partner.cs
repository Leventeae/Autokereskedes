using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokereskedes.Classes
{
    public class Partner
    {
        public string Identifier { get; set; }

        public Partner(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return $"{Identifier}";
        }
    }
}