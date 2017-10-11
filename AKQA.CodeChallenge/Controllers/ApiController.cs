using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AKAQ.CodeChallenge.Business;
using AKQA.CodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AKQA.CodeChallenge.Controllers
{
    [Route("/api/v1/converter")]
    public class ApiController : Controller
    {
        /// <summary>
        /// This is the instance of NumberConverterService will be injected by IoC. see the config service in startup.cs for detail. This using default IoC Container shipped in .NET core.
        /// </summary>
        readonly INumberConverterService service;

        public ApiController(INumberConverterService service) {
            this.service = service;
        }

        [HttpGet]
        // GET: /<controller>/
        // This method is not support, just 
        public IActionResult Get()
        {
            return Json(new { Success = true, Read= "One hundress....."});
        }

        [HttpPost]
        public IActionResult Post([FromBody]NumberConvertRequest request )
		{

            //If input is not a valid number, or client not send a valid message, API will return bad request error
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ObjectResult(new ErrorReponse("Input invalid"));
            }

            try
            {
                var read = this.service.ConvertToString(request.InputNumber.Value);
                return new OkObjectResult(new SuccessResponse(request.InputNumber.Value, read));
            }
            catch(Exception ex) {
                //Always catch exception to make sure we handle all unhanddled exception inside service class.
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ObjectResult(new ErrorReponse("Internal error occured: " + ex.Message));
            }
		
		}

    }
}
