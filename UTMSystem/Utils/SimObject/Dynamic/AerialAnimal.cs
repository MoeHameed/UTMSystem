using Newtonsoft.Json;

namespace Utils.SimObject.Dynamic
{
    public class AerialAnimal : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public AerialAnimal(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft) : base(sizeX, sizeY, sizeZ, topLeft)
        {
        }
    }
}
