using ChessMaster;
using ChessMaster.DataModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ChessMasterController : ControllerBase
    {
        private IMaster _chessService;

        public ChessMasterController(IMaster chessService)
        {
            _chessService = chessService;
        }


        [HttpGet("/chessmaster/generatedatamodel/{username}")]
        public ActionResult<bool> GenerateDataModel()
        {
            Task.Run(() => _chessService.CheckDataModel("mislavmislav"));
            return true;
        }

        [HttpGet("/chessmaster/getstatus/{username}")]
        public DataStatus GetStatus(string username)
        {
            var status = _chessService.GetStatus(username);
            return status;
        }
    }
}