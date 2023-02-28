
public static class CheckUserExtensions
{
    public static IApplicationBuilder UseCheckUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CheckUserMiddleware>();
    }
}