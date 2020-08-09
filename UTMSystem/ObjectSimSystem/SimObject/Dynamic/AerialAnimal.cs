using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Dynamic
{
    public class AerialAnimal : SimObject
    {
        [JsonConstructor]
        public AerialAnimal() : base(new GeoCoordinate(51.244372, -114.884342, 40), 2,1,1)
        {
        }
    }
}
