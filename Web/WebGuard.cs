using System.Net;
using System.Web.Http;

namespace JessicasAquariumMonitor.Web
{
    public static class WebGuard
    {
        public static void ThrowBadRequestIfNull(object value, string parameterName = @"value")
        {
            if (value == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public static void ThrowBadRequestIfNullOrWhitespace(string value, string parameterName = @"value")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}