public static class UpdateUserExtensions
{
    public static IApplicationBuilder UseUpdateUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UpdateUserMiddleware>();
    }
}