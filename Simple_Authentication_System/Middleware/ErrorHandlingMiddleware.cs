using Newtonsoft.Json;
using Serilog;
using Simple_Authentication_System_Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Simple_Authentication_System_Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ApiResponse<object>(error.Message ?? "An error occured", error?.Message, 500);
                switch (error)
                {
                    //case ApiException e:
                    //    // custom application error
                    //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //    break;
                    case ValidationException e:
                        // custom application error
                        responseModel.Status = response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Msg = error.Message;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        responseModel.Status = response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Msg = error.Message;


                        break;
                    case UnauthorizedAccessException e:
                        responseModel.Status = response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        responseModel.Msg = error.Message;


                        break;
                    default:
                        // unhandled error
                        responseModel.Status = response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //responseModel.Msg = e.Message;

                        Log.Fatal(error, "An error has occurred ");
                        break;
                }
                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
