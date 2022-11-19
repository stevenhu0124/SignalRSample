using Microsoft.Owin;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalRServer.APIs
{
    internal class ApiServerAlive : OwinMiddleware
    {
        public ApiServerAlive(OwinMiddleware next): base(next)
        {
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Invoke(IOwinContext context)
        {
            try
            {
                var response = context.Response;
                response.ContentType = "text/xml";
                response.StatusCode = 200;
                return response.WriteAsync("Alive");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}
