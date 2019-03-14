using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class BistellArtikelsController : ApiController
    {
        // GET: api/BistellArtikels
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BistellArtikels/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BistellArtikels
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BistellArtikels/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BistellArtikels/5
        public void Delete(int id)
        {
        }
    }
}
