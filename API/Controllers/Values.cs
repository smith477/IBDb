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
        private readonly ISession session;
        private readonly ICluster cluster;

        public ValuesController()
        {
            cluster = Cluster.Builder()
                     .AddContactPoints("127.0.0.1")
                     .Build(); ;
            session = cluster.Connect("ibdb");
        }  

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var valuesTable = new Table<Test>(session);

            IEnumerable<Test> values = (from test in valuesTable select test).Execute();
            return values;
        }

        [HttpGet("{id}")]
        public Values GetValue(Guid id)
        {
            var valuesTable = new Table<Values>(session);

            var values = valuesTable.Where(u => u.Id == id)
                .AllowFiltering()
                .FirstOrDefault()
                .Execute();
            return values;
        }

        [HttpPut("{id}")]
        public void UpdateValue(Guid id, Values val)
        {
            var valuesTable = new Table<Values>(session);

            valuesTable.Where(u => u.Id == id)
                        .Select(u => new Values { Value_int = val.Value_int, Value_string = val.Value_string })
                        .Update()
                        .Execute();
        }

        [HttpDelete("{id}")]
        public void DeleteValue(Guid id)
        {
            var valuesTable = new Table<Values>(session);

            valuesTable.Where(u => u.Id == id)
                        .Delete()
                        .Execute();
        }
    }
}
