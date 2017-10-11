using System;
namespace AKQA.CodeChallenge.Models
{
    /// <summary>
    /// The structure of successful data reponse, In the real application with the complicated reponse we may wrap the content in a sub object and meta in separate object
    /// </summary>
    public class SuccessResponse : ApiResponse
    {
        public double Number
        {
            get;
            set;
        }

        public string Read
        {
            get;
            set;
       }
        public SuccessResponse(double number, string read)
        {
            base.Success = true;
            this.Read = read;
            this.Number = number;
        }
    }
}
