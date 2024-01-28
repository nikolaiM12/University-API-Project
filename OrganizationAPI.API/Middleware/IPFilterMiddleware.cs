using OrganizationAPI.Domain.Abstractions.Constants;
using System.Net;

namespace OrganizationAPI.API.Middleware
{
    public class IPFilterMiddleware
    {
        private readonly RequestDelegate next;

        public IPFilterMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string forwardedFor = context.Request.Headers["X-Forwarded-For"];

            string remoteIpAddress = string.IsNullOrEmpty(forwardedFor) ? context.Connection.RemoteIpAddress?.ToString() : forwardedFor;

            string ipv4Loopback = IPAddress.Parse(remoteIpAddress).MapToIPv4().ToString();

            if (remoteIpAddress != AllowedIPConstants.allowedIP && ipv4Loopback != AllowedIPConstants.allowedIP)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access denied. Your IP is not allowed.");
            }
            else
            {
                await next(context);
            }
        }
    }
}
