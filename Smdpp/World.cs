﻿using Smdpp.Logic.Hardware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smdpp
{
    public static class World
    {
        public static List<string> BaudRates = new List<string>()
        {
            "300",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200",
        };

        public static List<Image> CameraImages = new List<Image>();
        public static BaseControlBoard ControlBoard { get; set; }
        public static List<PnpJob> Jobs { get; set; }

        public static void Initialize()
        {
            ControlBoard = new ChipKit_Max32();
            Jobs = new List<PnpJob>();
            EventSink.PnpFileParsed += EventSink_PnpFileParsed;
        }

        private static void EventSink_PnpFileParsed(PnpJob task)
        {
            Jobs.Clear();
            Jobs.Add(task);
        }

        public static PnpJob GetCurrentJob() => Jobs.FirstOrDefault();
    }
}
