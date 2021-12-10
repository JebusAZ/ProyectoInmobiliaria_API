using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Logic
{
    public class MongoLogic
    {


        public MongoClient mongoClient;
        public IMongoDatabase database;


        public MongoLogic()
        {

            mongoClient = new MongoClient("mongodb://localhost:27017");
            //mongoClient = new MongoClient("mongodb://dba-root:mongoadmin@inmobiliariatuksa.eastus.cloudapp.azure.com:27017");

            database = mongoClient.GetDatabase("dbInmobiliaria");

        }


        public bool Insert(Log log)
        {

            var col = database.GetCollection<BsonDocument>("Logs");

            col.InsertOne(log.ToBsonDocument());

            return true;
        }


    }
}
