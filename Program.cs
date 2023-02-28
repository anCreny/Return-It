using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MiddlewareExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>();


var app = builder.Build();



app.Map("/signup", _ =>
{
    _.UseCheckUser();
    _.UseCreateUser();
});

app.Map("/rating", _ =>
{
    _.UseRating();
});

app.Map("/user/updatescores", _ =>
{
    _.UseUpdatingScores();
});

app.Map("/user/updateuser", _ =>
{
    _.UseUpdateUser();
});

app.Run();
