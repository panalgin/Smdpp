//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Smdpp
{
    using System;
    using System.Collections.Generic;
    
    public partial class FeederSlot
    {
        public int ID { get; set; }
        public int Width { get; set; }
        public decimal PickupX { get; set; }
        public Nullable<decimal> Depth { get; set; }
        public decimal PickupY { get; set; }
        public Nullable<int> CurrentPartID { get; set; }
        public Nullable<int> SuggestedPartID { get; set; }
    
        public virtual Component ConnectedPart { get; set; }
        public virtual Component SuggestedPart { get; set; }
    }
}
