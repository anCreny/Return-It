using Microsoft.EntityFrameworkCore;

public class UpdatingScoresMiddleware
{
    readonly RequestDelegate next;
    UserContext db;

    public UpdatingScoresMiddleware(RequestDelegate next, UserContext db)
    {
        this.next = next;
        this.db = db; 
    }
    public async Task InvokeAsync(HttpContext context)
    {
        string? username = context.Request.Query["username"];
        int score = int.Parse(context.Request.Query["score"]!);
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) await context.Response.WriteAsync("A user with this username was not found!");
        if (user!.Score < score)
        {
            user.Score = score;
            await db.SaveChangesAsync();
            await context.Response.WriteAsync("Your rating has been successfully updated!");
        }
    }
}
