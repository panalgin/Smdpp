using Smdpp.Contracts;
using Smdpp.Logic;
using System;

namespace Smdpp
{
    public static class ComponentManager
    {
        public static bool Add(AddComponentInfo contract)
        {
            bool result = false;

            try

            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    context.Components.Add(new Component()
                    {
                        PackageID = contract.PackageID,
                        Value = contract.Value
                    });

                    context.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                result = false;
                Logger.Enqueue(ex);
            }

            return result;
        }
    }
}