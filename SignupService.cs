using Microsoft.EntityFrameworkCore;
using System.Text.Json;

class SignupService
{
    public async Task<IResult> SignupAsync(string username, UserContext db)
    {
        List<User> users = await db.Users.ToListAsync();

        foreach (User u in users)
        {
            if (u.Username == username) throw new Exception("A user with this username already exists!");
        }
        User? user = new User { Username = username, Score = 0 };
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return Results.Json(user);
    }
}