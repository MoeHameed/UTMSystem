using Newtonsoft.Json;

namespace Utils.SimObject.Static
{
    public class Foliage : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public Foliage() : base(1,1,5, new GeoCoordinate(51.244156, -114.883293, 1))
        {
        }
    }
}
