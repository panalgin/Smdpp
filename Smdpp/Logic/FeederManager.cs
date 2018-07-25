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

        public static Task<List<Contracts.FeederSlot>> GetFeederSlots()
        {
            using (var context = new SmdppEntities())
            {
                var result = context.FeederSlots.OrderBy(q => q.PickupX)
                        .Select(q => new Contracts.FeederSlot()
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
                                      Slot = new Contracts.FeederSlot()
                                      {
                                          ID = f.ID,
                                          Depth = f.Depth,
                                          PickupX = f.PickupX,
                                          PickupY = f.PickupY,
                                          Width = f.Width,
                                      },
                                      CurrentPart = c1 == null ? null : new Contracts.Component()
                                      {
                                          ID = c1.ID,
                                          Value = c1.Value,
                                          Package = new Contracts.Package()
                                          {
                                              ID = c1.PackageID,
                                              Name = c1.Package.Name
                                          }
                                      },
                                      SuggestedPart = s1 == null ? null : new Contracts.Component()
                                      {
                                          ID = s1.ID,
                                          Value = s1.Value,
                                          Package = new Contracts.Package()
                                          {
                                              ID = s1.PackageID,
                                              Name = s1.Package.Name
                                          }
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
        public static Task<Contracts.FeederSlot> GetAppropriateSlotFor(PnpPart part)
        {
            using (var context = new SmdppEntities()) {
                Package package = context.Packages.FirstOrDefault(q => q.ID == part.PackageID);

                if (package != null) {
                    Contracts.FeederSlot slot = context.FeederSlots.Where(q => q.CurrentPartID != null && q.ConnectedPart.Value == part.Value).Select(y => new Contracts.FeederSlot()
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