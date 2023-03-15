using Microsoft.EntityFrameworkCore;

public class UpdateUserMiddleware
{
    readonly RequestDelegate next;
    UserContext db; 

    public UpdateUserMiddleware(RequestDelegate next, UserContext db)
    {
        this.next = next;
        this.db = db; 
    }
    public async Task InvokeAsync(HttpContext context)
    {
        string? old_username = context.Request.Query["old_username"];
        string? new_username = context.Request.Query["new_username"];
        List<User> users = await db.Users.ToListAsync();
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == old_username);
        if (user == null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("There is no user with this username!");
        }
        if (new_username != user!.Username && new_username != "")
        {
            foreach (User u in users)
            {
                if (u.Username == new_username)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("A user with this username already exists!");
                }
            }
            user.Username = new_username;
            await db.SaveChangesAsync();
            await context.Response.WriteAsync("Your data successfully updated!");
        }
    }
}