using Smdpp.Contracts;
using Smdpp.Logic;
using Smdpp.Logic.Callbacks;
using System;
using System.Linq;

namespace Smdpp
{
    public static class ComponentManager
    {
        public static AddComponentCallback Add(AddComponentInfo contract)
        {
            bool result = false;
            string message = "";

            try

            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    var existing = context.Components.FirstOrDefault(q => q.PackageID == contract.PackageID && q.Value == contract.Value);

                    if (existing == null)
                    {
                        context.Components.Add(new Component()
                        {
                            PackageID = contract.PackageID,
                            Value = contract.Value
                        });

                        context.SaveChanges();
                        result = true;
                        message = "Successfully added new component";
                    }
                    else
                    {
                        result = false;
                        message = "Benzer bir bileşen zaten girilmiş.";
                    }
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
                Logger.Enqueue(ex);
            }

            return new AddComponentCallback()
            {
                Success = result,
                Message = message
            };
        }
    }
}