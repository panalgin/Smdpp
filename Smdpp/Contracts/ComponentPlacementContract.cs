using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Contracts
{
    public class ComponentPlacementContract
    {
        public List<PackageContract> Packages { get; set; }
        public List<ComponentContract> Components { get; set; }
    }
}
