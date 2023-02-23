using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserContext>();
builder.Services.AddTransient<RatingService>();
builder.Services.AddTransient<SignupService>();
builder.Services.AddTransient<UpdatingScoresService>();
builder.Services.AddTransient<DeleteService>();
builder.Services.AddTransient<UpdateUserService>();

var app = builder.Build();

app.MapGet("/users/rating", async (UserContext db, RatingService ratingService) => await ratingService.RatingAsync(db));

app.MapPost("/signup/{username}", async (string username, UserContext db, SignupService signupService) => await signupService.SignupAsync(username, db));

app.MapPut("/users/{username}/{score:int}", async (string username, int score, UserContext db, UpdatingScoresService updatingScoresService) => await updatingScoresService.UpdatingScoresAsync(username, score, db));

app.MapDelete("/users/{username}/delete", async (string username, UserContext db, DeleteService deleteService) => await deleteService.DeleteAsync(username, db));

app.MapPut("/users/{username}/update/{new_username}", async (string username, string new_username, UserContext db, UpdateUserService updateUserService) => await updateUserService.UpdateUserAsync(username, new_username, db));

app.Run();
