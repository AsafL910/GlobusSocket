using System.Diagnostics;

namespace Globus.PositionProvider 
{
    public static class MyTimer 
    {
        public static Aircraft mockAircraft;
        public static Stopwatch stopwatch = new Stopwatch();

        public static void StartTimer(Aircraft aircraft)
        {
            mockAircraft = aircraft;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public static void StopTimer(Aircraft aircraft) {
            Console.WriteLine($"expected: {mockAircraft.CallSign}, recieved: {aircraft.CallSign}");

            if (mockAircraft.CallSign.Equals(aircraft.CallSign)) {
                stopwatch.Stop();
                Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds}");
            }
        }
    }
}