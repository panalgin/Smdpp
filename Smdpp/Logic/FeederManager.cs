using Smdpp.Contract;
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
                              join c in context.Components on f.CurrentPartID equals c.ID into cParts
                              from c1 in cParts.DefaultIfEmpty() 
                                  join p1 in context.Packages on c1.PackageID equals p1.ID
                              join s in context.Components on f.SuggestedPartID equals s.ID into sParts
                              from s1 in sParts.DefaultIfEmpty()
                                  join p2 in context.Packages on s1.PackageID equals p2.ID
                              select new FeederStateContract()
                              {
                                  Slot = new FeederSlotContract()
                                  {
                                      ID = f.ID,
                                      Depth = f.Depth,
                                      PickupX = f.PickupX,
                                      PickupY = f.PickupY,
                                      Width = f.Width,
                                  },
                                  CurrentPart = new ComponentContract()
                                  {
                                      ID = c1.ID,
                                      Name = c1.Name,
                                      //PackageID = c1.PackageID,
                                      Package = new PackageContract()
                                      {
                                          ID = p1.ID,
                                          Name = p1.Name,
                                          Data = p1.Data,
                                      }
                                  },
                                  SuggestedPart = new ComponentContract()
                                  {
                                      
                                  }

                              }).ToList();

                result.All(delegate (FeederStateContract contract)
                {
                    //var connecte

                    return true;
                });

                return null;
            }
        }
    }
}
