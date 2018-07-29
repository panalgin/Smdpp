using Newtonsoft.Json;
using Smdpp.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Smdpp.Logic
{
    public static class EventHandlers
    {
        public static void Initialize()
        {
            EventSink.ListPackagesRequested += EventSink_ListPackagesRequested;
            EventSink.ListComponentsRequested += EventSink_ListComponentsRequested;
            EventSink.FeedersRequested += EventSink_FeedersRequested;
            EventSink.FeederStatesRequested += EventSink_FeederStatesRequested;
        }

        private static void EventSink_FeederStatesRequested()
        {
            var task = Task.Run(async () =>
            {
                 var result = await FeederManager.GetFeederStates();
                var json = JsonConvert.SerializeObject(result);

                ScriptRunner.Run(ScriptAction.ListFeederStatesReply, Utility.HtmlEncode(json));
            });
        }

        private static void EventSink_FeedersRequested()
        {
            var task = Task.Run(async () =>
            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    var result = await FeederManager.GetFeederSlots();
                    var json = JsonConvert.SerializeObject(result);

                    ScriptRunner.Run(ScriptAction.ListFeedersReply, Utility.HtmlEncode(json));
                }
            });
        }

        private static void EventSink_ListPackagesRequested()
        {
            var task = Task.Run(() =>
            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    var packages = context.Packages.ToList().Select(q => new Contracts.Package()
                    {
                        ID = q.ID,
                        Name = q.Name,
                        Data = q.Data
                    }).ToList();

                    var json = JsonConvert.SerializeObject(packages);

                    ScriptRunner.Run(ScriptAction.ListPackagesReply, Utility.HtmlEncode(json));
                }
            });
        }

        private static void EventSink_ListComponentsRequested()
        {
            var task = Task.Run(() =>
            {
                using (SmdppEntities context = new SmdppEntities())
                {
                    var components = context.Components.Select(q => new Contracts.Component()
                    {
                        ID = q.ID,
                        Value = q.Value,
                        Package = new Contracts.Package()
                        {
                            ID = q.PackageID,
                            Name = q.Package.Name
                        }
                    }).ToList();

                    var json = JsonConvert.SerializeObject(components);
                    ScriptRunner.Run(ScriptAction.ListComponentsReply, Utility.HtmlEncode(json));
                }
            });
        }
    }
}
