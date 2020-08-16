using System.Text;
using Newtonsoft.Json;

namespace Utils.SimObject
{
    public class SimObject
    {
        public int Id { get; set; }
        public GeoCoordinate TopLeft { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; } 
        public int SizeZ { get; set; } 

        private static int _nextId = -1;

        public SimObject(int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft = null)
        {
            Id = ++_nextId;
            TopLeft = topLeft;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        [JsonConstructor]
        public SimObject(int id, int sizeX, int sizeY, int sizeZ, GeoCoordinate topLeft = null)
        {
            Id = id;
            TopLeft = topLeft;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Id:\t" + Id);
            sb.Append("\tTopLeft:\t" + TopLeft);
            sb.Append("\tSizeX:\t" + SizeX);
            sb.Append("\tSizeY:\t" + SizeY);
            sb.Append("\tSizeZ:\t" + SizeZ);

            return sb.ToString();
        }
    }
}
