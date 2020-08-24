using Newtonsoft.Json;

namespace Utils.SimObject.Static
{
    public class Foliage : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public Foliage(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft) : base(sizeX, sizeY, sizeZ, topLeft)
        {
        }
    }
}
