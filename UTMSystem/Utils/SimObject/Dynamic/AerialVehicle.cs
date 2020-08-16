using Newtonsoft.Json;

namespace Utils.SimObject.Dynamic
{
    public class AerialVehicle : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public AerialVehicle(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft) : base(sizeX, sizeY, sizeZ, topLeft)
        {
        }
    }
}
