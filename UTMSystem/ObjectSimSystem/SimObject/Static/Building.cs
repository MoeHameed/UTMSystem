using ExtendedGeoCoordinate;
using Newtonsoft.Json;

namespace ObjectSimSystem.SimObject.Static
{
    public class Building : SimObject
    {
        [JsonConstructor]
        public Building() : base(new GeoCoordinate(51.245298, -114.880075, 0), 3, 3, 3)
        {
        }
    }
}
