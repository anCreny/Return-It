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
        string? username = context.Request.Query["username"];
        string? new_username = context.Request.Query["new_username"];
        List<User> users = await db.Users.ToListAsync();
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) await context.Response.WriteAsync("User not found!");
        if (new_username != user!.Username && new_username != "")
        {
            foreach (User u in users)
            {
                if (u.Username == new_username) throw new Exception("A user with this username already exists!");
            }
            user.Username = new_username;
            await db.SaveChangesAsync();
            await context.Response.WriteAsync("Your data successfully updated!");
        }
    }
}