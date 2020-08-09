using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Dynamic
{
    public class GroundAnimal : SimObject
    {
        [JsonConstructor]
        public GroundAnimal() : base(new GeoCoordinate(51.243653, -114.880286, 0), 1, 1, 1)
        {
        }
    }
}
