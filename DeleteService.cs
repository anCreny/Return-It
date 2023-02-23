using Microsoft.EntityFrameworkCore;
using System.Text.Json;

class DeleteService
{
    public async Task<IResult> DeleteAsync(string username, UserContext db)
    {
        User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) return Results.NotFound(new { message = "User not found!" });
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.Json(user);
    }
}