using Newtonsoft.Json;
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
        Unspecified,
        Rectangle,
        Circle,
        Square,
        PPad,
    }

    public class BasePlotterTool
    {
        private string guid = Guid.NewGuid().ToString();

        [JsonIgnore]
        protected string Data { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string ID { get { return guid; } }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }

        [JsonProperty(PropertyName = "toolType")]
        public PlotterToolType ToolType { get; internal set; }

        [JsonIgnore]
        public PadType PadType { get; set; }

        public BasePlotterTool()
        {

        }

        public BasePlotterTool(string data)
        {
            this.Data = data;

            this.ParseName();
            this.ParseToolType();
            this.ParsePadType();
            this.ParseProperties();
        }

        protected void ParseName()
        {
            this.Name = this.Data.Split(',')[0];
        }

        protected void ParseToolType()
        {
            string[] parameters = this.Data.Split(',');

            string typeIdentifier = parameters[1];

            switch (typeIdentifier)
            {
                case "RECT": this.ToolType = PlotterToolType.Rectangle; break;
                case "CIRCLE": this.ToolType = PlotterToolType.Circle; break;
                case "SQUARE": this.ToolType = PlotterToolType.Square; break;
            }
        }

        protected void ParsePadType()
        {

        }

        public virtual void ParseProperties() { }
    }
}