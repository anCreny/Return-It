using Microsoft.EntityFrameworkCore;
using System.Text.Json;

class UpdateUserService
{
    public async Task<IResult> UpdateUserAsync(string username, string new_username, UserContext db)
    {
        List<User> users = await db.Users.ToListAsync();
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return Results.NotFound(new { message = "User not found!" });
        if (new_username != user.Username && new_username != "")
        {
            foreach (User u in users)
            {
                if (u.Username == new_username) throw new Exception("A user with this username already exists!");
            }
            user.Username = new_username;
            await db.SaveChangesAsync();
        };
        return Results.Json(user);
    }
}