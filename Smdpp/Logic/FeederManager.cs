using Smdpp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class FeederManager
    {
        public static void Initialize()
        {

        }

        public static Task<List<FeederSlotContract>> GetCurrentFeederSlots()
        {
            using (var context = new SmdppEntities())
            {
                var result = (from f in context.FeederSlots
                              select new FeederSlotContract()
                              {
                                  ID = f.ID,
                                  Depth = f.Depth,
                                  PickupX = f.PickupX,
                                  PickupY = f.PickupY,
                                  Width = f.Width
                              }).ToList();

                result.All(delegate (FeederSlotContract contract)
                {
                    var connecte

                    return true;
                });

            }
        }
    }
}
