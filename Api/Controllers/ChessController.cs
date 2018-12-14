using System.Web.Http;

namespace Api.Controllers
{
    public class ChessController : ApiController
    {
        // GET: api/Chess
        public string PullData()
        {
            return "Pull started";
        }

        //// GET: api/Chess/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Chess
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Chess/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Chess/5
        //public void Delete(int id)
        //{
        //}
    }
}
