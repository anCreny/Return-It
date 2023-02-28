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
        if (username != null)
        {
            foreach (User u in users)
            {
                if (u.Username == username) throw new Exception("A user with this username already exists!");
            }
            await next.Invoke(context);
        }
       
    }
}