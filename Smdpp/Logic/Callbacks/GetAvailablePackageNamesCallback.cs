using System.Collections.Generic;
using System.Linq;

namespace Smdpp.Logic.Callbacks
{
    public class GetAvailablePackageNamesCallback : BaseScriptCallback
    {
        public List<string> Names { get; set; }

        public GetAvailablePackageNamesCallback()
        {
            this.Names = new List<string>();


            using (SmdppEntities context = new SmdppEntities())
            {
                this.Names = context.Packages.Select(q => q.Name).ToList();
            }

            this.Message = "We've returned all the package names.";
            this.Success = true;
        }
    }
}