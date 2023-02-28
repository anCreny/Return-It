public static class UpdatingScoresExtensions
{
    public static IApplicationBuilder UseUpdatingScores(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UpdatingScoresMiddleware>();
    }
}  