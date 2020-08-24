using Newtonsoft.Json;

namespace Utils.SimObject.Static
{
    public class Building : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public Building(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft) : base(sizeX, sizeY, sizeZ, topLeft)
        {
        }
    }
}
