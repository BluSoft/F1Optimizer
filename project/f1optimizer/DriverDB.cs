using System;
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
    class DriverDB
    {
        public ObjectId Id { get; set; }
        public string name {get;set;}
        public string team { get; set; }
        public string skill { get; set; }
        public string aggression { get; set; }  
    }
}
