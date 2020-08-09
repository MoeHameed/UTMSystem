using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Dynamic
{
    public class AerialVehicle : SimObject
    {
        [JsonConstructor]
        public AerialVehicle() : base(new GeoCoordinate(51.242310, -114.882625, 50), 2, 2, 2)
        {
        }
    }
}
