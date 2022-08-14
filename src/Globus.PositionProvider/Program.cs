using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Globus.PositionProvider
{
    public class Wd : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs eventArgs) {
            var parsedData = JsonConvert.DeserializeObject<Aircraft>(eventArgs.Data);
            MyTimer.StopTimer(parsedData);
        }
    }
    public class MockSelfData : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs eventArgs) {
            var parsedData = JsonConvert.DeserializeObject<Aircraft>(eventArgs.Data);
            MyTimer.StartTimer(parsedData);
            Sessions.Broadcast(JsonConvert.SerializeObject(parsedData));
        }
    }
    public class GetSelfPositionData : WebSocketBehavior
    {
        private static Aircraft aircraft = new Aircraft { CallSign = "SelfData", Position = new Position { Longitude = Randomizer.RandomDouble(34.4,35.6), Latitude = Randomizer.RandomDouble(30,33) }, TrueTrack = Randomizer.RandomDouble(0,360), Altitude = 0 };

        protected override void OnOpen()
        {
            base.OnOpen();
            Init();
        }

        private void Init()
        {
            aircraft.Simulate();

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
            var wssv = new WebSocketServer("ws://0.0.0.0:4000");
            wssv.AddWebSocketService<GetSelfPositionData>("/selfPosition");
            wssv.AddWebSocketService<MockSelfData>("/mockData");
            wssv.AddWebSocketService<Wd>("/wd");
            wssv.Start();
            Process.GetCurrentProcess().WaitForExit();
            //wssv.Stop();
        }
    }
}