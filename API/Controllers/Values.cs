using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {

        Cassandra.Cluster cluster = Cluster.Builder()
                    .AddContactPoints("127.0.0.1")
                    .Build();

        [HttpGet]
        public IEnumerable<Values> Get()
        {
            Cassandra.ISession session = cluster.Connect("ibdb");

            var valuesTable = new Table<Values>(session);
            MappingConfiguration.Global.Define(
                          new Map<Values>()
                             .TableName("ibdb.values")
                             .PartitionKey(u => u.Id)
                             .Column(u => u.Id, cm => cm.WithName("id"))
                             .Column(u => u.Value_int, cm => cm.WithName("value_int"))
                             .Column(u => u.Value_string, cm => cm.WithName("value_string"))
                             );

            IEnumerable<Values> values = (from value in valuesTable select value).Execute();
            return values;


        }
        [HttpGet("{id}")]
        public Values GetId(Guid id)
        {
            Cassandra.ISession session = cluster.Connect("ibdb");
            var valuesTable = new Table<Values>(session);
            MappingConfiguration.Global.Define(
                          new Map<Values>()
                             .TableName("ibdb.values")
                             .PartitionKey(u => u.Id)
                             .Column(u => u.Id, cm => cm.WithName("id"))
                             .Column(u => u.Value_int, cm => cm.WithName("value_int"))
                             .Column(u => u.Value_string, cm => cm.WithName("value_string"))
                             );

            var values = (from value in valuesTable where value.Id == id select value)
                .AllowFiltering()
                .FirstOrDefault()
                .Execute();
            return values;
        }

        [HttpPut("{id}")]
        public void UpdateValue(Guid id)
        {
            Cassandra.ISession session = cluster.Connect("ibdb");
            var valuesTable = new Table<Values>(session);
            MappingConfiguration.Global.Define(
                          new Map<Values>()
                             .TableName("ibdb.values")
                             .PartitionKey(u => u.Id)
                             .Column(u => u.Id, cm => cm.WithName("id"))
                             .Column(u => u.Value_int, cm => cm.WithName("value_int"))
                             .Column(u => u.Value_string, cm => cm.WithName("value_string"))
                             );
            valuesTable.Where(u => u.Id == id)
                        .Select(u => new Values { Value_int = 15, Value_string = "nesto44" })
                        .Update()
                        .Execute();
        }

        [HttpDelete("{id}")]
        public void DeleteValue(Guid id)
        {
            Cassandra.ISession session = cluster.Connect("ibdb");
            var valuesTable = new Table<Values>(session);
            MappingConfiguration.Global.Define(
                          new Map<Values>()
                             .TableName("ibdb.values")
                             .PartitionKey(u => u.Id)
                             .Column(u => u.Id, cm => cm.WithName("id"))
                             );
            valuesTable.Where(u => u.Id == id)
                        .Delete()
                        .Execute();
        }
    }
}
