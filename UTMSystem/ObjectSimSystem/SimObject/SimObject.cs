using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject
{
    public class SimObject
    {
        public int Id { get; set; }
        public GeoCoordinate TopLeft { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; } 
        public int SizeZ { get; set; } 

        private static int _nextId = -1;

        [JsonConstructor]
        public SimObject(GeoCoordinate topLeft, int sizeX, int sizeY, int sizeZ)
        {
            Id = ++_nextId;
            TopLeft = topLeft;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

    }
}
