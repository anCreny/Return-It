

namespace MiddlewareExtensions;

public static class MiddlewareHandler
{
    public static IApplicationBuilder UseCheckUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CheckUserMiddleware>();
    }

    public static IApplicationBuilder UseCreateUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CreateUserMiddleware>();
    }

    public static IApplicationBuilder UseRating(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RatingMiddleware>();
    }

    public static IApplicationBuilder UseUpdatingScores(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UpdatingScoresMiddleware>();
    }

    public static IApplicationBuilder UseUpdateUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UpdateUserMiddleware>();
    }
} 