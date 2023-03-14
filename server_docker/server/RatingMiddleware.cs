using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class RatingMiddleware
{
    readonly RequestDelegate next;
    UserContext db;

    public RatingMiddleware(RequestDelegate next, UserContext db)
    {
        this.next = next;
        this.db = db; 
    }

    public async Task<string> InvokeAsync(HttpContext context)
    {
        List<User> users = await db.Users.ToListAsync();
        var sorted_users = from user in users
                           where user.Score != 0
                           orderby user.Score descending
                           select user;
        return JsonSerializer.Serialize(sorted_users); 
    }
}

