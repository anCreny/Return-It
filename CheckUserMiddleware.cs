using Microsoft.EntityFrameworkCore;

public class CheckUserMiddleware
{
    readonly RequestDelegate next;
    UserContext? db;

    public CheckUserMiddleware(RequestDelegate next, UserContext db)
    {
        this.next = next;
        this.db = db;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? username = context.Request.Query["username"];
        context.Items["username"] = username;
        List<User> users = await db!.Users.ToListAsync();
        bool _nameHasBeenFound = false;
        if (username != null)
        {
            foreach (User u in users)
            {
               
                if (u.Username == username)
                {
                    _nameHasBeenFound = true;
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("A user with this username already exists!");
                    
                }
              
            }
        }
        if (!_nameHasBeenFound) await next.Invoke(context);
       
    }
}