using Microsoft.AspNetCore.Mvc;
using RClient;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessController : ControllerBase
    {
        // GET api/chess
        //  method will return state of scraping
        [HttpGet]
        public ActionResult<bool> Get()
        {
            var client = ClientFactory.GetClient("rediscache");
            return client.CheckStatus();
        }

        //[HttpGet("{id}")]
        //// GET api/chess
        ////  method will return state of scraping
        //[HttpGet]
        //public ActionResult<string> Get(string id)
        //{
        //    var chessMaster = ChessMasterFactory.GetChessMaster();
        //}
    }
      
}