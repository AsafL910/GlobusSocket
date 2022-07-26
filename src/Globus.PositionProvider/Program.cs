using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace Globus.PositionProvider
{
    public class GetSelfPositionData : WebSocketBehavior
    {
        private static Aircraft aircraft;

        protected override void OnOpen()
        {
            base.OnOpen();
            Init();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            Console.WriteLine("Closed!!");
        }

        private void Init()
        {
            aircraft = new Aircraft { CallSign = "SelfData", Position = new Position { Longitude = Randomizer.RandomDouble(34.4,35.6), Latitude = Randomizer.RandomDouble(30,33) }, TrueTrack = Randomizer.RandomDouble(0,360), Altitude = 0 };
            aircraft.Simulate();
            Console.WriteLine("Simulation Started");

            while (true)
            {
                Sessions.Broadcast(JsonConvert.SerializeObject(aircraft));
                Thread.Sleep(1000/60);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://localhost:4000");
            wssv.AddWebSocketService<GetSelfPositionData>("/selfPosition");
            wssv.Start();
            Console.WriteLine("Server started");
            Console.ReadLine();
            wssv.Stop();
        }
    }
}