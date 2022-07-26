using System;

namespace Globus.PositionProvider
{
    public static class Randomizer
    {
        public static double RandomDouble(double minimum, double maximum)
        {
            return new Random().NextDouble() * (maximum - minimum) + minimum;
        }

        public static int RandomInt(int minimum, int maximum)
        {
            return new Random().Next(minimum, maximum+1);
        }
    }
}