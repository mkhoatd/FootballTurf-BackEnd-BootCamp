using Microsoft.AspNetCore.Http;

namespace WebApi.Repository.Service
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null, dynamic? data = null)
        {
            this.statusCode = statusCode;
            this.message = message ?? GetDefaultMessageForStatusCode(statusCode);
            success = statusCode == StatusCodes.Status200OK;
            this.data = data;
        }

        public int statusCode { get; set; }

        public string message { get; set; }

        public bool success { get; set; }

        public dynamic? data { get; set; }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "A bad request, you have made",

                StatusCodes.Status401Unauthorized => "Authorized, you are not",

                StatusCodes.Status404NotFound => "Resource found, it was not",

                StatusCodes.Status500InternalServerError => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change ",

                StatusCodes.Status200OK => "Call API success",

                _ => ""
            };
        }
    }
}
