using Microsoft.EntityFrameworkCore;

public class CreateUserMiddleware
{
    readonly RequestDelegate next;
    UserContext? db;

    public CreateUserMiddleware(RequestDelegate next, UserContext db)
    {
        this.next = next;
        this.db = db; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? username = context.Items["username"]!.ToString();
        User? user = new User { Username = username, Score = 0 };
        await db!.Users.AddAsync(user);
        await db.SaveChangesAsync();
        await context.Response.WriteAsync("A new user has been successfully created!");
    }
}
