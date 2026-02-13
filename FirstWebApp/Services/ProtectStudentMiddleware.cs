using System.Threading.Tasks;

namespace FirstWebApp.Services
{
    public class ProtectStudentMiddleware
    {
        private readonly RequestDelegate _next;
        public ProtectStudentMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/Students"))
            {
                context.Response.Redirect("/");
                return;
            }
            await _next(context);
            Console.WriteLine("After Request Processing in ProtectStudentMiddleware");
        }
    }
}
