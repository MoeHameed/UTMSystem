using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utils.SimObject;

namespace MappingSystem
{
    public class MappingSystem
    {
        private const int Port = 7755;
        private readonly UdpClient _udpClient;
        private readonly DatabaseManager _dbManager;
        private bool _processStaticObjects = true;

        public MappingSystem()
        {
            _dbManager = new DatabaseManager();
            Console.WriteLine("Database initialized");

            _udpClient = new UdpClient(Port);
            _udpClient.BeginReceive(ReceiveData, null);
            Console.WriteLine("Listening on port " + Port + " ...");

        }

        public void ReceiveData(IAsyncResult res)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Any, Port);
            var receivedBytes = _udpClient.EndReceive(res, ref ipEndPoint);

            Console.WriteLine("Received data from " + ipEndPoint.Address + ":" + ipEndPoint.Port + " ...");
            ProcessReceivedObjects(Encoding.UTF8.GetString(receivedBytes));

            _udpClient.BeginReceive(ReceiveData, null);
        }

        public void ProcessReceivedObjects(string objectsData)
        {
            var simObjectData = JsonConvert.DeserializeObject<SimObjectData>(objectsData);

            if (_processStaticObjects)
            {
                // Terrain 
                var terrainCells = GridSystem.TerrainCells(simObjectData.Terrain.SizeZ);
                var terrainSimObjCells = new SimObjectCells(simObjectData.Terrain, terrainCells);
                _dbManager.InsertSimObjectCells(terrainSimObjCells);

                // Foliage
                AddObjectToDb(simObjectData.Foliage[0]);

                // Buildings
                AddObjectToDb(simObjectData.Buildings[0]);

                _processStaticObjects = false;
            }

            // Aerial vehicles
            AddObjectToDb(simObjectData.AerialVehicles[0]);

            // Aerial animals
            AddObjectToDb(simObjectData.AerialAnimals[0]);

            // Ground animals
            AddObjectToDb(simObjectData.GroundAnimals[0]);
        }

        private void AddObjectToDb(SimObject obj)
        {
            var cells = GridSystem.ObjectToCells(obj.TopLeft, obj.SizeX, obj.SizeY, obj.SizeZ);
            var simObjCells = new SimObjectCells(obj, cells);
            _dbManager.InsertSimObjectCells(simObjCells);
        }
    }
}
