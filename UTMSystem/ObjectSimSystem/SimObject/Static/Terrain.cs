using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Static
{
    public class Terrain : SimObject
    {
        [JsonConstructor]
        public Terrain() : base(new GeoCoordinate(0, 0, 0), 0, 0, 5)
        {
        }
    }
}
