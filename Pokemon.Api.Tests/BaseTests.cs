using Microsoft.Extensions.DependencyInjection;
using Pokemon.Data;
using Pokemon.Services;

namespace Pokemon.Tests
{
    public class BaseTests
    {
        protected IPokemonService PokemonService { get; private set; }    
        protected IServiceProvider ServiceProvider { get; private set; }
        public BaseTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IPokemonDatabase, PokemonDatabase>();
            services.AddScoped<IPokemonService, PokemonService>();
            
            // Cria um service provider "mockado"
            this.ServiceProvider = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider();
            
            this.PokemonService = ServiceProvider.GetRequiredService<IPokemonService>();
        }
    }
}
