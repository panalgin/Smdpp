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

            if (Validate(contract))
            {
                try
                {
                    using (SmdppEntities context = new SmdppEntities())
                    {
                        var existing = context.Components.FirstOrDefault(q => q.PackageID == contract.PackageID && q.Value.ToLower() == contract.Value.ToLower());

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
                catch (Exception ex)
                {
                    message = ex.Message;
                    Logger.Enqueue(ex);
                }
            }
            else
                message = "Girdiğiniz değerler yanlış.";

            return new AddComponentCallback()
            {
                Success = result,
                Message = message
            };
        }

        static bool Validate(AddComponentInfo contract)
        {
            if (contract.PackageID <= 0)
                return false;
            else if (contract.Value.Length < 1)
                return false;
            else
                return true;
        }
    }
}