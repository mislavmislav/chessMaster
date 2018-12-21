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
        public DataStatus GenerateDataModel(string username)
        {
            return _chessService.GenerateDataModel(username);
        }

        [HttpGet("/chessmaster/getstatus/{username}")]
        public DataStatus GetStatus(string username)
        {
            return _chessService.GetStatus(username);
        }
    }
}