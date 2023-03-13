using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Pokemon.Data;
using Pokemon.Services;

namespace Pokemon.Tests
{
    public class BaseTests
    {
        protected IPokemonService PokemonService { get; private set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        protected IPokemonDatabase PokemonDatabase { get; private set; }

        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        public BaseTests()
        {
            var services = new ServiceCollection();

            // Registra a implementação da interface IPokemonDatabase
            services.AddScoped<IPokemonDatabase, PokemonDatabase>();

            // Registra a implementação da interface IPokemonService
            services.AddHttpClient();
            services.AddSingleton<IConfigureOptions<HttpClientFactoryOptions>>(new ConfigureBaseUrlOptions(BaseUrl));
            services.AddScoped<IPokemonService, PokemonService>();

            // Cria um service provider "mockado"
            this.ServiceProvider = services.BuildServiceProvider();

            this.PokemonService = ServiceProvider.GetRequiredService<IPokemonService>();
            this.PokemonDatabase = ServiceProvider.GetRequiredService<IPokemonDatabase>();
        }
    }

    public class ConfigureBaseUrlOptions : IConfigureOptions<HttpClientFactoryOptions>
    {
        private readonly string _baseUrl;

        public ConfigureBaseUrlOptions(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void Configure(HttpClientFactoryOptions options)
        {
            options.HttpClientActions.Add(client =>
            {
                client.BaseAddress = new Uri(_baseUrl);
            });
        }
    }
}
