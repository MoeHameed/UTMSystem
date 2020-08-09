using Newtonsoft.Json;
using ObjectSimSystem.SimObject.Dynamic;
using ObjectSimSystem.SimObject.Static;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace ObjectSimSystem
{
    public class ObjectSimSystem
    {
        public long Timestamp => DateTimeOffset.Now.ToUnixTimeMilliseconds();
        public Terrain Terrain { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Foliage> Foliage { get; set; }
        public List<AerialAnimal> AerialAnimals { get; set; }
        public List<GroundAnimal> GroundAnimals { get; set; }
        public List<AerialVehicle> AerialVehicles { get; set; }

        private readonly IPEndPoint _ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7755);
        private readonly Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public ObjectSimSystem()
        {
            InitStaticObjects();
            Console.WriteLine("Initialized All Static Objects");

            InitDynamicObjects();
            Console.WriteLine("Initialized All Dynamic Objects");

            var loopTimer = new Timer(1000);
            loopTimer.Elapsed += SendObjectDataUdp;
            loopTimer.AutoReset = true;
            loopTimer.Enabled = true;
        }

        private void InitStaticObjects()
        {
            Terrain = new Terrain();
            Console.WriteLine("Initialized Terrain \t Object ID: " + Terrain.Id);

            Buildings = new List<Building> {new Building()};
            Console.WriteLine("Initialized Building \t Object ID: " + Buildings[0].Id);

            Foliage = new List<Foliage> {new Foliage()};
            Console.WriteLine("Initialized Foliage \t Object ID: " + Foliage[0].Id);
        }

        private void InitDynamicObjects()
        {
            AerialAnimals = new List<AerialAnimal> {new AerialAnimal()};
            Console.WriteLine("Initialized Aerial Animal \t Object ID: " + AerialAnimals[0].Id);

            GroundAnimals = new List<GroundAnimal> {new GroundAnimal()};
            Console.WriteLine("Initialized Ground Animal \t Object ID: " + GroundAnimals[0].Id);

            AerialVehicles = new List<AerialVehicle> {new AerialVehicle()};
            Console.WriteLine("Initialized Aerial Vehicle \t Object ID: " + AerialVehicles[0].Id);
        }

        private void SendObjectDataUdp(object sender, ElapsedEventArgs e)
        {
            var jsonStr = JsonConvert.SerializeObject(this);
            Console.WriteLine("Sending object data to " + _ipEndPoint.Address + ":" + _ipEndPoint.Port + " ...");

            _socket.SendTo(Encoding.ASCII.GetBytes(jsonStr), _ipEndPoint);
        }
    }
}
