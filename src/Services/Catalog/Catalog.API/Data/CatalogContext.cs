using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
  //          string connectionString =
  //@"mongodb://microservice:O2rgu3G9HpWmOiBTg3Wq7ZZNvmxZ9At6NX0tZrKjLm68Vmk8FieDfVG3KD5KZ69sQCFcpx0647E9AvUG8Lv4BA==@microservice.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@microservice@";
  //          MongoClientSettings settings = MongoClientSettings.FromUrl(
  //            new MongoUrl(connectionString)
  //          );
  //          settings.SslSettings =
  //            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
  //          var mongoClient = new MongoClient(settings);


            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
