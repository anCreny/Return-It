using Microsoft.EntityFrameworkCore;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserContext>();

var app = builder.Build();

app.MapGet("/users/rating", async (UserContext db) =>
{
    List<User> users = await db.Users.ToListAsync();
    var sorted_users = from user in users
                       where user.Score != 0
                       orderby user.Score descending
                       select user;
    return sorted_users;
});

app.MapPost("/signup", async (User user, UserContext db) =>
{
    List<User> users = await db.Users.ToListAsync();

    foreach (User u in users) 
    {
        if (u.Username == user.Username) throw new Exception("A user with this username already exists!");
    }
    
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Json(user);  
    
});

app.MapPut("/users/{username}/{score:int}", async (string username, int score, UserContext db) =>
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
});

app.MapDelete("users/delete/{username}", async (string username, UserContext db) =>
{
    User? user = await db.Users.FirstOrDefaultAsync(u => u.Username == username);
    if (user == null) return Results.NotFound(new { message = "User not found!" });
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("user/{username}/update", async (string username, string new_username, string new_password, UserContext db) =>
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
    } 
    if (new_password != user.Password && new_password != "")
    {
        user.Password = new_password;
    }
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();
