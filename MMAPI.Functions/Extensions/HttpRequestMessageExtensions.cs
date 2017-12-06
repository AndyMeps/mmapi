namespace System.Net.Http
{
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Return a [500] response with standard error message.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static HttpResponseMessage ServerError(this HttpRequestMessage req, string errorMessage = null)
        {
            return req.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage ?? "Something went wrong on the server, this has been logged.");
        }
    }
}
