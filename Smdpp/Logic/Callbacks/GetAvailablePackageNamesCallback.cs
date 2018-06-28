using System.Collections.Generic;
using System.Linq;

namespace Smdpp.Logic.Callbacks
{
    public class GetAvailablePackageNamesCallback : BaseScriptCallback
    {
        public string[] Names { get; set; }

        public GetAvailablePackageNamesCallback()
        { 
            using (SmdppEntities context = new SmdppEntities())
            {
                this.Names = context.Packages.Select(q => q.Name).ToArray();
            }

            this.Message = "We've returned all the package names.";
            this.Success = true;
        }
    }
}