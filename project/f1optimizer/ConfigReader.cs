using System;
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
        private Dictionary<string, string> m_configParams;

        #endregion Fields

        #region Constructor

        public ConfigReader(string databaseName)
        {
            m_client = new MongoClient();
            m_server = m_client.GetServer();
            m_database = m_server.GetDatabase(databaseName);
            GetGlobalConfig();
        }

        #endregion

        #region Public Methods

        public int GetInt(string key)
        {
            return Convert.ToInt32(m_configParams[key], 10);
        }

        public string GetString(string key)
        {
            return m_configParams[key];
        }

        public double GetDouble(string key)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return Convert.ToDouble(m_configParams[key], provider);
        }

        public bool GetBool(string key)
        {
            return m_configParams[key] == "true";
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

        public List<Strategy> GetStrategies()
        {
            List<Strategy> result = new List<Strategy>();

            var strategies = m_database.GetCollection("strategies");

            var query = from c in strategies.AsQueryable<BsonDocument>() select c;

            foreach (var str in query)
            {
                Strategy strategy = new Strategy();
                strategy.Name = str["name"].AsString;
                strategy.Description = str["description"].AsString;
                result.Add(strategy);
            }

            return result;
        }

        #endregion

        #region Private Methods

        public void GetGlobalConfig()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            var configs = m_database.GetCollection("config");

            var query = from c in configs.AsQueryable<BsonDocument>() select c;

            foreach (var record in query)
            {
                result.Add(record["key"].AsString, record["value"].AsString);
            }
            m_configParams = result;
        }


        #endregion
    }
}
