using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Saleforce.Permissions.Api.Entities;
using Saleforce.Permissions.Api.Persistence.EntityFramework;

namespace Saleforce.Permissions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PermissionsDbContext _dbContext;

        public ValuesController(PermissionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Delivery>> Get()
        {
            var domModel = new DeliveryDomainModel
            {
                DeliveryId = Guid.NewGuid().ToString(),
                LoadDate = DateTimeOffset.Now
            };

            var delvry = new Delivery
            {
                Id = domModel.DeliveryId,
                Data = JsonConvert.SerializeObject(domModel)
            };

            var y = new DeliveryApproval
            {
                Id = Guid.NewGuid().ToString(),
                Delivery = delvry
            };
            _dbContext.DeliveryApprovals.Add(y);
            _dbContext.SaveChanges();

            try
            {
                var abc = _dbContext.DeliveryApprovals.First();


                var delivery = _dbContext
                    .Deliveries
                    .Include(x => x.DeliveryApproval)
                    .ToList();

                var reslt = delivery
                    .Where(d => d.DomainModel.LoadDate >= DateTimeOffset.Now.AddDays(-1))
                    .ToList();

                return reslt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
