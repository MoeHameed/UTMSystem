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

        private const int UpdateS = 1;

        private Random random = new Random();

        public ObjectSimSystem()
        {
            InitStaticObjects();
            Console.WriteLine("Initialized All Static Objects");

            InitDynamicObjects();
            Console.WriteLine("Initialized All Dynamic Objects");

            var loopTimer = new Timer(UpdateS * 100);
            loopTimer.Elapsed += SendObjectDataUdp;
            loopTimer.AutoReset = true;
            loopTimer.Enabled = true;
        }

        private void InitStaticObjects()
        {
            SimObjectData.Terrain = new Terrain();
            Console.WriteLine("Initialized Terrain \t Object ID: " + SimObjectData.Terrain.Id);

            SimObjectData.Buildings = new List<Building>
            {
                new Building( 3, 3, 3, new GeoCoordinate(51.245298, -114.880075, 1)),
                new Building( 20, 11, 7, new GeoCoordinate(51.244803, -114.880005, 1))
            };
            foreach (var building in SimObjectData.Buildings)
                Console.WriteLine("Initialized Building \t Object ID: " + building.Id);

            SimObjectData.Foliage = new List<Foliage>();
            for (int i = 0; i < 30; i++)
            {
                var coord = GetRandomCoord();
                var height = random.Next(30, 48);
                SimObjectData.Foliage.Add(new Foliage(5, 5, height, new GeoCoordinate(coord.Item1, coord.Item2, 1)));
            }
            foreach (var foliage in SimObjectData.Foliage)
                Console.WriteLine("Initialized Foliage \t Object ID: " + foliage.Id);
        }

        private void InitDynamicObjects()
        {
            SimObjectData.AerialAnimals = new List<AerialAnimal>
            {
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244372, -114.884342, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244415, -114.884275, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244507, -114.884205, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244599, -114.884135, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244456, -114.884471, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244572, -114.884557, 55)),
                new AerialAnimal(2, 1, 1, new GeoCoordinate(51.244684, -114.884643, 55)),
            };
            foreach (var aerialAnimal in SimObjectData.AerialAnimals)
                Console.WriteLine("Initialized Aerial Animal \t Object ID: " + aerialAnimal.Id);

            SimObjectData.GroundAnimals = new List<GroundAnimal> {new GroundAnimal(1, 1, 1, new GeoCoordinate(51.243653, -114.880286, 1)) };
            Console.WriteLine("Initialized Ground Animal \t Object ID: " + SimObjectData.GroundAnimals[0].Id);

            SimObjectData.AerialVehicles = new List<AerialVehicle> {new AerialVehicle(9, 12, 4, new GeoCoordinate(51.242310, -114.882625, 100)) };
            Console.WriteLine("Initialized Aerial Vehicle \t Object ID: " + SimObjectData.AerialVehicles[0].Id);
        }

        private void SendObjectDataUdp(object sender, ElapsedEventArgs e)
        {
            // Update all objects by "simulating them"
            UpdateAerialAnimals();
            UpdateAerialVehicle();

            SimObjectData.Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var jsonStr = JsonConvert.SerializeObject(SimObjectData);
            Console.WriteLine("Sending object data to " + _ipEndPoint.Address + ":" + _ipEndPoint.Port + " ...");

            _socket.SendTo(Encoding.ASCII.GetBytes(jsonStr), _ipEndPoint);
        }

        private void UpdateAerialAnimals()
        {
            var oldCoord1 = SimObjectData.AerialAnimals[0].TopLeft;
            const double metersS1 = Constants.LatDiff * 1;
            var newLat1 = oldCoord1.Latitude - (metersS1 * UpdateS);

            if (newLat1 - (Constants.LatDiff * 5) <= Constants.MinLat)
            {
                SimObjectData.AerialAnimals[0].TopLeft = new GeoCoordinate(51.244372, -114.884342, 40);
                SimObjectData.AerialAnimals[1].TopLeft = new GeoCoordinate(51.244415, -114.884275, 40);
                SimObjectData.AerialAnimals[2].TopLeft = new GeoCoordinate(51.244507, -114.884205, 40);
                SimObjectData.AerialAnimals[3].TopLeft = new GeoCoordinate(51.244599, -114.884135, 40);
                SimObjectData.AerialAnimals[4].TopLeft = new GeoCoordinate(51.244456, -114.884471, 40);
                SimObjectData.AerialAnimals[5].TopLeft = new GeoCoordinate(51.244572, -114.884557, 40);
                SimObjectData.AerialAnimals[6].TopLeft = new GeoCoordinate(51.244684, -114.884643, 40);
            }
            else
            {
                foreach (var aerialAnimal in SimObjectData.AerialAnimals)
                {
                    var oldCoord = aerialAnimal.TopLeft;
                    const double metersS = Constants.LatDiff * 1;
                    var newLat = oldCoord.Latitude - (metersS * UpdateS);
                    aerialAnimal.TopLeft = new GeoCoordinate(newLat, oldCoord.Longitude, oldCoord.Altitude);
                }
            }
        }

        private void UpdateAerialVehicle()
        {
            var oldCoord = SimObjectData.AerialVehicles[0].TopLeft;
            const double metersS = Constants.LonDiff * 6;
            var newLon = oldCoord.Longitude + (metersS * UpdateS);

            if (newLon > Constants.MaxLon)
                SimObjectData.AerialVehicles[0].TopLeft = new GeoCoordinate(51.242310, Constants.MinLon + (Constants.LonDiff * 3), 100);
            else
                SimObjectData.AerialVehicles[0].TopLeft = new GeoCoordinate(oldCoord.Latitude, newLon, oldCoord.Altitude);

        }

        private (double, double) GetRandomCoord()
        {
            var lat = random.NextDouble() * (Constants.MaxLat - Constants.LatDiff - Constants.MinLat) + Constants.MinLat + Constants.LatDiff;
            var lon = random.NextDouble() * (Constants.MaxLon + Constants.LonDiff - Constants.MinLon) + Constants.MinLon - Constants.LonDiff;

            return (lat, lon);
        }
    }
}
