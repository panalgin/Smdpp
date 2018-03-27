﻿using Smdpp.Logic;
using System.Collections.Generic;

namespace Smdpp
{
    public static class EventSink
    {
        public delegate void OnDevToolsRequested();
        public delegate void OnOpenGerberRequested();
        public delegate void OnImportSvgRequested();
        public delegate void OnCloseRequested();
        public delegate void OnGerberParsed(GerberTask task);
        public delegate void OnSvgParsed(SvgTask task);
        public delegate void OnSavePackageRequested();
        public delegate void OnListPackagesRequested();

        public static event OnImportSvgRequested ImportSvgRequested;
        public static event OnDevToolsRequested DevToolsRequested;
        public static event OnOpenGerberRequested OpenGerberReqeusted;
        public static event OnCloseRequested CloseRequested;
        public static event OnGerberParsed GerberParsed;
        public static event OnSvgParsed SvgParsed;
        public static event OnSavePackageRequested SavePackageRequested;
        public static event OnListPackagesRequested ListPackagesRequested;

        public static void InvokeImportGerberRequested()
        {
            ImportSvgRequested?.Invoke();
        }

        public static void InvokeSvgParsed(SvgTask task)
        {
            SvgParsed?.Invoke(task);
        }

        public static void InvokeGerberParsed(GerberTask task)
        {
            GerberParsed?.Invoke(task);
        }

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

        public static void InvokeSavePackageRequested()
        {
            SavePackageRequested?.Invoke();
        }

        public static void InvokeListPackagesRequested()
        {
            ListPackagesRequested?.Invoke();
        }
    }
}