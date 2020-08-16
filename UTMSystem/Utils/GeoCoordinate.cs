using Newtonsoft.Json;

namespace Utils
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        [JsonConstructor]
        public GeoCoordinate(double latitude, double longitude, double altitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public override string ToString()
        {
            return "Lat: " + Latitude + "\tLon: " + Longitude + "\tAlt: " + Altitude;
        }
    }
}
