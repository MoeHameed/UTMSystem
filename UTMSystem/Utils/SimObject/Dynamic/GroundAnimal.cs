using Newtonsoft.Json;

namespace Utils.SimObject.Dynamic
{
    public class GroundAnimal : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public GroundAnimal(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft) : base(sizeX, sizeY, sizeZ, topLeft)
        {
        }
    }
}
