using System.Collections.Generic;
using System.Linq;
using Application.Domain.User;
using Application.Persistence;
using Cassandra;
using Cassandra.Data.Linq;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            ISession session = SessionManager.GetSession();
            var valuesTable = new Table<User>(session);

            IEnumerable<User> values = (from user in valuesTable select user).Execute();
            return values;
        }

        // [HttpGet("{id}")]
        //     public Values GetValue(Guid id)
        //     {
        //         var valuesTable = new Table<Values>(session);

        //         var values = valuesTable.Where(u => u.Id == id)
        //             .AllowFiltering()
        //             .FirstOrDefault()
        //             .Execute();
        //         return values;
        //     }

        //     [HttpPut("{id}")]
        //     public void UpdateValue(Guid id, Values val)
        //     {
        //         var valuesTable = new Table<Values>(session);

        //         valuesTable.Where(u => u.Id == id)
        //                     .Select(u => new Values { Value_int = val.Value_int, Value_string = val.Value_string })
        //                     .Update()
        //                     .Execute();
        //     }

        //     [HttpDelete("{id}")]
        //     public void DeleteValue(Guid id)
        //     {
        //         var valuesTable = new Table<Values>(session);

        //         valuesTable.Where(u => u.Id == id)
        //                     .Delete()
        //                     .Execute();
        //     }
        // }
    }
}