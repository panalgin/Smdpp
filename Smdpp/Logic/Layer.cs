namespace Smdpp.Logic
{
    public enum Layer
    {
        Bottom,
        Top
    }

    public static class Layers
    {
        public static Layer Parse(string layer)
        {
            switch(layer)
            {
                case "Üst": return Layer.Top;
                case "Alt": return Layer.Bottom;
            }

            return Layer.Bottom;
        }
    }
}