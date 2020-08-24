using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using Utils;
using Utils.SimObject;

namespace MappingSystem
{
    // TODO: Remove if not needed
    /// <summary>
    /// Used to keep track of a cell and associated object. Potentially used by visualization system.
    /// </summary>
    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int ObjectId { get; set; }

        public Cell(int x, int y, int z, int objectId)
        {
            X = x;
            Y = y;
            Z = z;
            ObjectId = objectId;
        }
    }

    /// <summary>
    /// Used to keep track of object and its cells
    /// </summary>
    public struct SimObjectCells
    {
        public SimObject SimObject { get; set; }
        public List<Tuple<int, int, int>> CellsList { get; set; }

        [BsonConstructor]
        public SimObjectCells(SimObject simObject, List<Tuple<int, int, int>> cellsList)
        {
            SimObject = simObject;
            CellsList = cellsList;
        }
    }

    /// <summary>
    /// Each cell is 1m x 1m x 1m
    /// Context: When looking down from top
    /// Top left = top left corner
    /// Size = num of blocks
    /// X = Horizontal
    /// Y = Vertical
    /// Z = Height building upwards
    /// 51.245600,-114.885100--------0.0056/391m=0.00001432225-----------51.245600,-114.879500
    /// |										 	   						|
    /// |										       						|
    /// 0.0039/434m=0.000008986175			     		       	   			0.0039/434m=0.000008986175	
    /// |									           						|
    /// |										       						|	
    /// 51.241700,-114.885100--------0.0056/391m=0.00001432225-----------51.241700,-114.879500
    /// height = terrain height (5m) + 122m agl = 127m
    /// </summary>
    public static class GridSystem
    {
        public const int NumCellX = 391;
        public const int NumCellY = 434;
        public const int NumCellZ = 127;

        public static List<Tuple<int, int, int>> ObjectToCells(GeoCoordinate topLeft, int sizeX, int sizeY, int sizeZ)
        {
            var list = new List<Tuple<int, int, int>>();

            var (item1, item2, item3) = GeoCoordToCell(topLeft);

            for (var i = 0; i < sizeX; i++)
                for (var j = 0; j < sizeY; j++)
                    for (var k = 0; k < sizeZ; k++)
                        list.Add(new Tuple<int, int, int>(item1 + i, item2 + j, item3 + k));

            return list;
        }

        public static Tuple<int, int, int> GeoCoordToCell(GeoCoordinate coordinate)
        {
            // Calculate x coord based on longitude
            var xMultiplier = 0;
            if (Math.Abs(coordinate.Longitude - Constants.MinLon) < 0.000001)
                xMultiplier = 1;
            else if (coordinate.Longitude >= Constants.MaxLon)
                xMultiplier = NumCellX;
            else
                while (Constants.MinLon + (xMultiplier * Constants.LonDiff) < coordinate.Longitude)
                    xMultiplier++;
            var x = xMultiplier - 1;
            if(x < 0) throw new ApplicationException("X IS LESS THAN 0");
            
            // Calculate y coord based on latitude
            var yMultiplier = 0;
            if (Math.Abs(coordinate.Latitude - Constants.MinLat) < 0.000001)
                yMultiplier = 1;
            else if (coordinate.Latitude >= Constants.MaxLat)
                yMultiplier = NumCellY;
            else
                while (Constants.MinLat + (yMultiplier * Constants.LatDiff) < coordinate.Latitude)
                    yMultiplier++;
            var y = yMultiplier - 1;
            if (y < 0) throw new ApplicationException("Y IS LESS THAN 0");

            // Get z coord based on altitude
            int z;
            if (coordinate.Altitude >= NumCellZ)
                z = NumCellZ;
            else
                z = (int) coordinate.Altitude;
            if (z < 0) throw new ApplicationException("Z IS LESS THAN 0");

            return new Tuple<int, int, int>(x, y, z);
        }

        public static List<Tuple<int, int, int>> TerrainCells(int height)
        {
            var list = new List<Tuple<int, int, int>>();

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    for (var k = 0; k < height; k++)
                    {
                        list.Add(new Tuple<int, int, int>(i, j, k));
                    }
                }
            }

            return list;
        }
    }
}
