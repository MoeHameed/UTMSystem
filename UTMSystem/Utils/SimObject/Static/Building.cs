using Newtonsoft.Json;

namespace Utils.SimObject.Static
{
    public class Building : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public Building() : base(3, 3, 3, new GeoCoordinate(51.245298, -114.880075, 1))
        {
        }
    }
}
