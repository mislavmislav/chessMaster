using Api.Dto;
using ChessMaster;
using Jil;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
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
        public HttpResponseMessage GetStatus(string username)
        {
            var status = _chessService.GetStatus(username);
            return ReturnResult(status);
        }

        protected HttpResponseMessage ReturnResult<T>(T result)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(
                    JSON.SerializeDynamic(
                        new ServiceResult<T>(result)), 
                    Encoding.UTF8, 
                    "application/json"),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}