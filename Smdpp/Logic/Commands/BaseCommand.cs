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

        public override string ToString()
        {
            return string.Format("0x{0:X2},{1}", (int)this.Opcode, this.Value);
        }
    }
}
