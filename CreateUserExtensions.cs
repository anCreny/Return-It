public static class CreateUserExtensions
{
    public static IApplicationBuilder UseCreateUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CreateUserMiddleware>();
    }
}