﻿using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace f1optimizer
{
    class ConfigReader
    {
        #region Fields

        private MongoClient m_client;
        private MongoServer m_server;
        private MongoDatabase m_database;

        #endregion Fields

        #region Constructor

        public ConfigReader(string databaseName)
        {
            m_client = new MongoClient();
            m_server = m_client.GetServer();
            m_database = m_server.GetDatabase(databaseName);
        }

        #endregion

        #region Public Methods

        public Dictionary<string, string> GetGlobalConfig()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var configs = m_database.GetCollection("config");

            var query = from c in configs.AsQueryable<BsonDocument>() select c;

            foreach (var record in query)
            {
                result.Add(record["key"].AsString, record["value"].AsString);
            }

            return result;
        }

        public List<Driver> GetDrivers()
        {
            List<Driver> result = new List<Driver>();

            var drivers = m_database.GetCollection<DriverDB>("drivers");

            var query = from c in drivers.AsQueryable<DriverDB>() select c;

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            foreach (var driver in query)
            {
                result.Add(new Driver(driver.name, driver.team, Convert.ToDouble(driver.skill, provider), Convert.ToDouble(driver.aggression, provider)));
            }

            return result;
        }

        public List<string> GetStrategies()
        {
            List<string> result = new List<string>();

            var strategies = m_database.GetCollection("strategies");

            var query = from c in strategies.AsQueryable<BsonDocument>() select c;

            foreach (var str in query)
            {
                result.Add(str["sequence"].AsString);
            }

            return result;
        }

        #endregion
    }
}