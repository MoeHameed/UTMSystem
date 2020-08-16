using Newtonsoft.Json;

namespace Utils.SimObject.Static
{
    public class Terrain : Utils.SimObject.SimObject
    {
        [JsonConstructor]
        public Terrain() : base(0, 0, 1, new GeoCoordinate(0, 0, 0))
        {
        }
    }
}
