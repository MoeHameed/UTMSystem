using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Static
{
    public class Foliage : SimObject
    {
        [JsonConstructor]
        public Foliage() : base(new GeoCoordinate(51.244156, -114.883293, 0), 1,1,5)
        {
        }
    }
}
