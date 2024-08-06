public static class InputModalServiceExtension
{
    public static IServiceCollection AddInputModal(this IServiceCollection services)
    {
        services.AddScoped<IInputModalService, InputModalService>();
        return services;
    } 
}