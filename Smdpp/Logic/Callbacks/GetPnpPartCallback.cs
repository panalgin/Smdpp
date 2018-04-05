using Newtonsoft.Json;
using Smdpp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic.Callbacks
{
    public sealed class GetPnpPartCallback : BaseScriptCallback
    {
        [JsonProperty(PropertyName = "package")]
        public PackageContract Package { get; set; }

        [JsonProperty(PropertyName = "component")]
        public ComponentContract Component { get; set; }

        public GetPnpPartCallback(Package package, Component component)
        {
            this.Package = new PackageContract()
            {
                ID = package.ID,
                Name = package.Name,
                Data = package.Data
            };

            if (component != null)
            {
                this.Component = new ComponentContract()
                {
                    ID = component.ID,
                    Name = component.Name
                };
            }
        }
    }
}
