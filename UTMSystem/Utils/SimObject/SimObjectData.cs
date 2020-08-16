using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Utils.SimObject.Dynamic;
using Utils.SimObject.Static;

namespace Utils.SimObject
{
    public class SimObjectData
    {
        public long Timestamp { get; set; }
        public Terrain Terrain { get; set; }
        public List<Building> Buildings { get; set; }
        public List<Foliage> Foliage { get; set; }
        public List<AerialAnimal> AerialAnimals { get; set; }
        public List<GroundAnimal> GroundAnimals { get; set; }
        public List<AerialVehicle> AerialVehicles { get; set; }

        [JsonConstructor]
        public SimObjectData(long timestamp, Terrain terrain, List<Building> buildings, List<Foliage> foliage, List<AerialAnimal> aerialAnimals, List<GroundAnimal> groundAnimals, List<AerialVehicle> aerialVehicles)
        {
            Timestamp = timestamp;
            Terrain = terrain;
            Buildings = buildings;
            Foliage = foliage;
            AerialAnimals = aerialAnimals;
            GroundAnimals = groundAnimals;
            AerialVehicles = aerialVehicles;
        }

        public SimObjectData()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Timestamp:\t" + Timestamp);

            sb.AppendLine("Terrain:\n" + Terrain);

            sb.AppendLine("Buildings:");
            foreach (var v in Buildings)
                sb.AppendLine(v.ToString());

            sb.AppendLine("Foliage:");
            foreach (var v in Foliage)
                sb.AppendLine(v.ToString());

            sb.AppendLine("Aerial Animals:");
            foreach (var v in AerialAnimals)
                sb.AppendLine(v.ToString());

            sb.AppendLine("Aerial Vehicles:");
            foreach (var v in AerialVehicles)
                sb.AppendLine(v.ToString());

            sb.AppendLine("Ground Animals:");
            foreach (var v in GroundAnimals)
                sb.AppendLine(v.ToString());

            return sb.ToString();
        }
    }
}
