namespace Smdpp
{
    public static class EventSink
    {
        public delegate void OnDevToolsRequested();

        public static event OnDevToolsRequested DevToolsRequested;

        public static void InvokeDevToolsRequested()
        {
            DevToolsRequested?.Invoke();
        }
    }
}