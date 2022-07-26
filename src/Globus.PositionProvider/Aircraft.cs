namespace Globus.PositionProvider
{
    public class Aircraft
    {
        public string CallSign { get; set; }

        public Position Position { get; set; }

        public double TrueTrack { get; set; }

        public double Altitude { get; set; }
    }

    public class Position
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
