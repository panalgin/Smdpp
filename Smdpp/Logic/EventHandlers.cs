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
            EventSink.FeedersRequested += EventSink_FeedersRequested;
            EventSink.FeederStatesRequested += EventSink_FeederStatesRequested;
        }

        private static void EventSink_FeederStatesRequested()
        {
            var task = Task.Run(async () =>
            {
                var result = await FeederManager.GetCurrentFeederSlots();

                var json = JsonConvert.SerializeObject(result);

                ScriptRunner.Run(ScriptAction.ListFeederStatesReply, Utility.HtmlEncode(json));
            });
        }

        private static void EventSink_FeedersRequested()
        {
            var task = Task.Run(() =>
            {
                using(SmdppEntities context = new SmdppEntities())
                {
                    var slots = context.FeederSlots.OrderBy(q => q.PickupX)
                    .Select(q => new FeederSlotContract()
                    {
                        ID = q.ID,
                        Width = q.Width,
                        Depth = q.Depth,
                        PickupX = q.PickupX,
                        PickupY = q.PickupY
                    }).ToList();

                    var json = JsonConvert.SerializeObject(slots);

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
                    var packages = context.Packages.ToList().Select(q => new Package()
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
    }
}
