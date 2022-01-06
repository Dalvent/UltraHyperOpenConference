using Microsoft.Extensions.DependencyInjection;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddProjectRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IThemeRepository, ThemeRepository>();
            serviceCollection.AddScoped<IBanUserRepository, BanUserRepository>();
            serviceCollection.AddScoped<IMessageRepository, MessageRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
        
        public static void AddCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserAuthorisationService, UserAuthorisationService>();
            serviceCollection.AddScoped<IBanUserRepository, BanUserRepository>();
            serviceCollection.AddScoped<IModerationService, ModerationService>();
            serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
            serviceCollection.AddScoped<IConferenceService, ConferenceService>();
            serviceCollection.AddScoped<IThemeMessageTreeService, ThemeMessageTreeService>();
        }
    }
}