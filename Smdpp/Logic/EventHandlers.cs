using Newtonsoft.Json;
using Smdpp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class EventHandlers
    {
        public static void Initialize()
        {
            EventSink.ListPackagesRequested += EventSink_ListPackagesRequested;
        }

        private static void EventSink_ListPackagesRequested()
        {
            var task = Task.Run(() =>
            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    var packages = context.Packages.ToList().Select(q => new PackageContract()
                    {
                        ID = q.ID,
                        Name = q.Name,
                        Data = q.Data
                    }).ToList();

                    var json = JsonConvert.SerializeObject(packages);

                    ScriptRunner.Run(ScriptAction.ListPackagesReplied, Utility.HtmlEncode(json));
                }
            });
        }
    }
}
