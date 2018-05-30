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

        public static Task<List<FeederSlotContract>> GetFeederSlots()
        {
            using (var context = new SmdppEntities())
            {
                var result = context.FeederSlots.OrderBy(q => q.PickupX)
                        .Select(q => new FeederSlotContract()
                        {
                            ID = q.ID,
                            Width = q.Width,
                            Depth = q.Depth,
                            PickupX = q.PickupX,
                            PickupY = q.PickupY
                        }).ToList();

                return Task.FromResult(result);
            }
        }

        public static Task<List<FeederState>> GetFeederStates()
        {
            using (var context = new SmdppEntities())
            {
                try
                {
                    var result = (from f in context.FeederSlots
                                  join c in context.Components on f.CurrentPartID equals c.ID into cParts
                                  from c1 in cParts.DefaultIfEmpty()
                                  join s in context.Components on f.SuggestedPartID equals s.ID into sParts
                                  from s1 in sParts.DefaultIfEmpty()
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
                                      },
                                      SuggestedPart = s1 == null ? null : new ComponentContract()
                                      {
                                          ID = s1.ID,
                                          Name = s1.Name,
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

        /// <summary>
        /// Selects the best appropriate slot for the given pnp part. Currently
        /// it only looks for on which slot the same component is attached. Later this can
        /// suggest empty slots
        /// </summary>
        /// <param name="part">Pick and place part details which to look for its best place</param>
        /// <returns></returns>
        public static Task<FeederSlotContract> GetAppropriateSlotFor(PnpPart part)
        {
            using (var context = new SmdppEntities()) {
                Package package = context.Packages.FirstOrDefault(q => q.ID == part.PackageID);

                if (package != null) {
                    FeederSlotContract slot = context.FeederSlots.Where(q => q.CurrentPartID != null && q.ConnectedPart.Name == part.Value).Select(y => new FeederSlotContract()
                    {
                        ID = y.ID,
                        Depth = y.Depth,
                        PickupX = y.PickupX,
                        PickupY = y.PickupY,
                        Width = y.Width
                    }).FirstOrDefault();


                    return Task.FromResult(slot);
                }
                else
                    return null;
            }
        }
    }
}