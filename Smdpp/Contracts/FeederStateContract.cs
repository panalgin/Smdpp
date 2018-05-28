using Smdpp.Contracts;

namespace Smdpp.Contract
{
    public class FeederStateContract
    {
        public FeederStateContract()
        {

        }

        public FeederSlotContract Slot { get; set; }
        public object CurrentPart { get; set; }
        public ComponentContract SuggestedPart { get; set; }
    }
}