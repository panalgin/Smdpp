using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Commands
{
    public class BaseCommand
    {
        public Opcode Opcode { get; set; }
        public string Value { get; set; }

        public BaseCommand()
        {

        }

        public BaseCommand(Opcode code, string value)
        {
            this.Opcode = code;
            this.Value = value;
        }

        public virtual string ToString()
        {
            return null;
        }
    }
}
