using System;
using System.Threading;
using System.Threading.Tasks;
namespace Globus.PositionProvider
{
    public static class Extentions
    {
        public static void Simulate(this Aircraft aircraft) 
        {
            Task.Run(() =>  Simulate(aircraft, (int)Randomizer.RandomDouble(100,300)));
        }
        private static void Simulate(Aircraft aircraft, int knot)
        {
            Console.WriteLine("Starting Simulation");
            
            var sendInterval = 0.1;
            var arrowsMode = false;
            var knotToKmConstant = 0.0005144;


            for (int i = 0; i < 100000; i++)
            {
                //Console.WriteLine($"Aircraft {aircraft.CallSign}; Cycle {i}");
                Thread.Sleep((int)(sendInterval * 1000));

                var KmPassed = sendInterval * knot * knotToKmConstant;

                aircraft.Position.MoveToPostion(KmPassed, aircraft.TrueTrack);

                if (!arrowsMode)
                {
                    aircraft.TrueTrack =
                        (
                        aircraft.TrueTrack + Randomizer.RandomDouble(0, 0.08)
                        ) %
                        360;
                    aircraft.Altitude += (int)(sendInterval * 10);
                }
            }
        }
        public static void MoveToPostion(this Position position, double distanceKm, double bearing)
        {
            // taken from: https://stackoverflow.com/a/46410871/13549    
            // distance in KM, bearing in degrees

            var R = 6378.1; // Radius of the Earth
            var brng = bearing * Math.PI / 180; // Convert bearing to radian
            var lat = position.Latitude * Math.PI / 180; // Current coords to radians
            var lon = position.Longitude * Math.PI / 180;

            // Do the math magic
            lat = Math.Asin(Math.Sin(lat) * Math.Cos(distanceKm / R) + Math.Cos(lat) * Math.Sin(distanceKm / R) * Math.Cos(brng));
            lon += Math.Atan2(Math.Sin(brng) * Math.Sin(distanceKm / R) * Math.Cos(lat), Math.Cos(distanceKm / R) - Math.Sin(lat) * Math.Sin(lat));

            // Coords back to degrees and return
            position.Latitude = lat * 180.0 / Math.PI;
            position.Longitude = lon * 180.0 / Math.PI;
        }
    }
}