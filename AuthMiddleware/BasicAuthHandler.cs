using System.Text;

namespace AuthMiddleware;

public class BasicAuthHandler
{
    private readonly RequestDelegate _next;
    private readonly string _relm;
    
    public BasicAuthHandler(RequestDelegate next, string relm)
    {
        _next = next;
        _relm = relm;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        var header = context.Request.Headers["Authorization"].ToString();
        var encodedCred = header.Substring(6);
        var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCred));
        var uidPwd = creds.Split(":");
        var uid = uidPwd[0];
        var pwd = uidPwd[1];
        if (uid == "admin" && pwd == "admin")
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }
}