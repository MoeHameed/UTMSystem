using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Utils.SimObject;

namespace MappingSystem
{
    class DatabaseManager
    {
        private readonly IMongoCollection<BsonDocument> _gridCollection;
        private readonly IMongoCollection<BsonDocument> _objectCollection;

        private const string DbConnectionString = "mongodb://localhost:27017/";
        private const string DbName = "Test";
        private const string GridCollectionName = "Grid";
        private const string ObjectCollectionName = "Objects";

        public DatabaseManager()
        {
            var mongoClient = new MongoClient(DbConnectionString);
            mongoClient.DropDatabase(DbName);
            var db = mongoClient.GetDatabase(DbName);
            _gridCollection = db.GetCollection<BsonDocument>(GridCollectionName);
            _objectCollection = db.GetCollection<BsonDocument>(ObjectCollectionName);
        }

        /// <summary>
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>True if new, False if replaced</returns>
        public bool InsertCell(Cell cell)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("X", cell.X) & Builders<BsonDocument>.Filter.Eq("Y", cell.Y) & Builders<BsonDocument>.Filter.Eq("Z", cell.Z);
            var count = _gridCollection.Find(filter).CountDocuments();

            switch (count)
            {
                case 0:
                    _gridCollection.InsertOne(cell.ToBsonDocument());
                    return true;
                case 1:
                    _gridCollection.ReplaceOne(filter, cell.ToBsonDocument());
                    return false;
                default:
                    throw  new ApplicationException("MORE THAN 1 CELL WITH SAME X Y Z");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="simObjectCells"></param>
        /// <returns>True if new, False if replaced</returns>
        public bool InsertSimObjectCells(SimObjectCells simObjectCells)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("SimObject._id", simObjectCells.SimObject.Id);
            var count = _objectCollection.Find(filter).CountDocuments();

            switch (count)
            {
                case 0:
                    _objectCollection.InsertOne(simObjectCells.ToBsonDocument());
                    return true;
                case 1:
                    _objectCollection.ReplaceOne(filter, simObjectCells.ToBsonDocument());
                    return false;
                default:
                    throw new ApplicationException("MORE THAN 1 OBJECT WITH SAME ID");
            }
        }

        public SimObjectCells? GetSimObjectCells(int objectId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("SimObject._id", objectId);
            var docs = _objectCollection.Find(filter);

            if (docs.CountDocuments() == 0) return null;

            var doc = docs.First();

            var simObj = new SimObject(doc["SimObject"]["_id"].AsInt32, doc["SimObject"]["SizeX"].AsInt32, doc["SimObject"]["SizeY"].AsInt32, doc["SimObject"]["SizeZ"].AsInt32);
            var cellsList = doc["CellsList"].AsBsonArray.Select(val => new Tuple<int, int, int>(val[0].AsInt32, val[1].AsInt32, val[2].AsInt32)).ToList();

            return new SimObjectCells(simObj, cellsList);
        } 
    }
}
