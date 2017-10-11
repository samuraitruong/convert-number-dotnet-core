using System;
namespace AKQA.CodeChallenge.Models
{
    public class ErrorReponse : ApiResponse
    {
        public string Message
        {
            get;
            set;
        }
        public ErrorReponse(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
