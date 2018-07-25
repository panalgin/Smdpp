using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Smdpp.Logic.Callbacks
{
    public class GetAvailablePackageNamesCallback : BaseScriptCallback
    {
        [JsonProperty(PropertyName = "packages")]
        public List<Contracts.Package> Packages { get; set; }

        public GetAvailablePackageNamesCallback()
        { 
            using (SmdppEntities context = new SmdppEntities())
            {
                this.Packages = context.Packages.Select(q => new Contracts.Package() { ID = q.ID, Name = q.Name }).ToList();
            }

            this.Message = "We've returned all the packages.";
            this.Success = true;
        }
    }
}