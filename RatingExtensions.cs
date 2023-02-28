public static class RatingExtensions
{
    public static IApplicationBuilder UseRating(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RatingMiddleware>();
    }
}