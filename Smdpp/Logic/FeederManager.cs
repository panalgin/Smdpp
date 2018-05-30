using Smdpp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class FeederManager
    {
        public static void Initialize()
        {

        }

        public static Task<List<FeederState>> GetCurrentFeederSlots()
        {
            using (var context = new SmdppEntities())
            {
                try
                {
                    var result = (from f in context.FeederSlots
                                  join c in context.Components on f.CurrentPartID equals c.ID into cParts
                                  from c1 in cParts.DefaultIfEmpty()
                                  //join p1 in context.Packages on c1.PackageID equals p1.ID
                                  join s in context.Components on f.SuggestedPartID equals s.ID into sParts
                                  from s1 in sParts.DefaultIfEmpty()
                                  //join p2 in context.Packages on s1.PackageID equals p2.ID
                                  select new FeederState()
                                  {
                                      Slot = new FeederSlotContract()
                                      {
                                          ID = f.ID,
                                          Depth = f.Depth,
                                          PickupX = f.PickupX,
                                          PickupY = f.PickupY,
                                          Width = f.Width,
                                      },
                                      CurrentPart = c1 == null ? null : new ComponentContract()
                                      {
                                          ID = c1.ID,
                                          Name = c1.Name,
                                          /*Package = p1 == null ? null : new PackageContract()
                                          {
                                              ID = p1.ID,
                                              Name = p1.Name,
                                              Data = p1.Data
                                          }*/

                                      },
                                      SuggestedPart = s1 == null ? null : new ComponentContract()
                                      {
                                          ID = s1.ID,
                                          Name = s1.Name,
                                          /*Package = p2 == null ? null : new PackageContract()
                                          {
                                              ID = p2.ID,
                                              Name = p2.Name,
                                              Data = p2.Data
                                          }*/
                                      }

                                  }).ToList();

                    result.All(delegate (FeederState contract)
                    {

                        return true;
                    });

                    return Task.FromResult(result);
                }
                catch (Exception ex)
                {
                    Logger.Enqueue(ex);

                    return null;
                }
            }
        }

        internal static object GetAppropriateSlotFor(PnpPart part)
        {
            throw new NotImplementedException();
        }
    }
}
