using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Commands
{
    public enum Opcode
    {
        None = 0x00,
        Hello = 0x01,
        SetJogPrecision = 0x02,
    }
}
