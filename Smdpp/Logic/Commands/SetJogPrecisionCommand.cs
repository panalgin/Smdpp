using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Commands
{
    public class SetJogPrecisionCommand : BaseCommand
    {
        public SetJogPrecisionCommand(int value) 
            : base(Opcode.SetJogPrecision, value.ToString())
        {
        }
    }
}
