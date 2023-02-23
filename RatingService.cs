using Microsoft.EntityFrameworkCore;
using System.Text.Json;

class RatingService
{
    public async Task<string> RatingAsync(UserContext db)
    {
        List<User> users = await db.Users.ToListAsync();
        var sorted_users = from user in users
                           where user.Score != 0
                           orderby user.Score descending
                           select user;
        return JsonSerializer.Serialize(sorted_users);
    }
}