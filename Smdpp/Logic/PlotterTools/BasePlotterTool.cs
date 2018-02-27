using System;

namespace Smdpp.Logic
{
    public enum PadType
    {
        NonConductor,
        SMDPadCuDef,
        ComponentPad,
        OtherPad,
        Material,
        ComponentDrill,
        OtherDrill,
    }

    public enum PlotterToolType
    {
        Rectangle,
        Circle,
        Square,
        PPad,
    }

    public class BasePlotterTool
    {
        protected string Data { get; set; }
        public string Name { get; internal set; }
        public PlotterToolType ToolType { get; internal set; }
        public PadType PadType { get; set; }

        public BasePlotterTool()
        {

        }

        protected void ParseToolType()
        {
            
        }

        protected void ParsePadType()
        {

        }

        public virtual void ParseProperties() { }
    }
}