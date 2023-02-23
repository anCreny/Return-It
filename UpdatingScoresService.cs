using Microsoft.EntityFrameworkCore;
using System.Text.Json;

class UpdatingScoresService
{
    public async Task<IResult> UpdatingScoresAsync(string username, int score, UserContext db) 
    {
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return Results.NotFound(new { message = "User not found!" });
        if (user.Score < score)
        {
            user.Score = score;
            await db.SaveChangesAsync();
            return Results.Json(user);
        }
        return Results.Json(user);
    }
}