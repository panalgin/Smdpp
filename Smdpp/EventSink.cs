namespace Smdpp
{
    public static class EventSink
    {
        public delegate void OnDevToolsRequested();
        public delegate void OnOpenGerberRequested();

        public static event OnDevToolsRequested DevToolsRequested;
        public static event OnOpenGerberRequested OpenGerberReqeusted;

        public static void InvokeDevToolsRequested()
        {
            DevToolsRequested?.Invoke();
        }

        public static void InvokeOpenGerberRequested()
        {
            OpenGerberReqeusted?.Invoke();
        }
    }
}