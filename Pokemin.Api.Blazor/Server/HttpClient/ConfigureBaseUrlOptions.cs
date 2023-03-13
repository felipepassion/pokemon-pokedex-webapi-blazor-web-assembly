using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

namespace Pokemin.Api.Blazor.Server.HttpClient
{
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
