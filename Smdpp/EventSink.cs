namespace Smdpp
{
    public static class EventSink
    {
        public delegate void OnDevToolsRequested();
        public delegate void OnOpenGerberRequested();
        public delegate void OnCloseRequested();

        public static event OnDevToolsRequested DevToolsRequested;
        public static event OnOpenGerberRequested OpenGerberReqeusted;
        public static event OnCloseRequested CloseRequested;

        public static void InvokeDevToolsRequested()
        {
            DevToolsRequested?.Invoke();
        }

        public static void InvokeOpenGerberRequested()
        {
            OpenGerberReqeusted?.Invoke();
        }

        public static void InvokeCloseRequested()
        {
            CloseRequested?.Invoke();
        }
    }
}