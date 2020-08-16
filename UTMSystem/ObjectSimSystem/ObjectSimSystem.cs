using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using Utils;
using Utils.SimObject;
using Utils.SimObject.Dynamic;
using Utils.SimObject.Static;

namespace ObjectSimSystem
{
    public class ObjectSimSystem
    {
        public SimObjectData SimObjectData = new SimObjectData();

        private readonly IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7755);
        private readonly Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public ObjectSimSystem()
        {
            InitStaticObjects();
            Console.WriteLine("Initialized All Static Objects");

            InitDynamicObjects();
            Console.WriteLine("Initialized All Dynamic Objects");

            var loopTimer = new Timer(2000);
            loopTimer.Elapsed += SendObjectDataUdp;
            loopTimer.AutoReset = true;
            loopTimer.Enabled = true;
        }

        private void InitStaticObjects()
        {
            SimObjectData.Terrain = new Terrain();
            Console.WriteLine("Initialized Terrain \t Object ID: " + SimObjectData.Terrain.Id);

            SimObjectData.Buildings = new List<Building> {new Building()};
            Console.WriteLine("Initialized Building \t Object ID: " + SimObjectData.Buildings[0].Id);

            SimObjectData.Foliage = new List<Foliage> {new Foliage()};
            Console.WriteLine("Initialized Foliage \t Object ID: " + SimObjectData.Foliage[0].Id);
        }

        private void InitDynamicObjects()
        {
            SimObjectData.AerialAnimals = new List<AerialAnimal> { new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244372, -114.884342, 40)) };
            Console.WriteLine("Initialized Aerial Animal \t Object ID: " + SimObjectData.AerialAnimals[0].Id);

            SimObjectData.GroundAnimals = new List<GroundAnimal> {new GroundAnimal(1, 1, 1, new GeoCoordinate(51.243653, -114.880286, 1)) };
            Console.WriteLine("Initialized Ground Animal \t Object ID: " + SimObjectData.GroundAnimals[0].Id);

            SimObjectData.AerialVehicles = new List<AerialVehicle> {new AerialVehicle(2, 2, 2, new GeoCoordinate(51.242310, -114.882625, 50)) };
            Console.WriteLine("Initialized Aerial Vehicle \t Object ID: " + SimObjectData.AerialVehicles[0].Id);
        }

        private void SendObjectDataUdp(object sender, ElapsedEventArgs e)
        {
            // Update objects geo locations
            var oldCoord = SimObjectData.AerialAnimals[0].TopLeft;
            SimObjectData.AerialAnimals[0].TopLeft = new GeoCoordinate(oldCoord.Latitude + 0.000009, oldCoord.Longitude, oldCoord.Altitude);

            SimObjectData.Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var jsonStr = JsonConvert.SerializeObject(SimObjectData);
            Console.WriteLine("Sending object data to " + _ipEndPoint.Address + ":" + _ipEndPoint.Port + " ...");

            _socket.SendTo(Encoding.ASCII.GetBytes(jsonStr), _ipEndPoint);
        }
    }
}
