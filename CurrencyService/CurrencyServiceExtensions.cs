using CurrencyService.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace CurrencyService
{
    public static class CurrencyServiceExtensions
    {
        public static IHttpClientBuilder AddCurrencyServiceManager(this IServiceCollection services)
        {
            return services.AddHttpClient<ICurrencyManager, CurrencyManager>((s, c) =>
            {
                var options = s.GetRequiredService<IOptions<CurrencyManagerOptions>>();
                c.BaseAddress = new Uri(options.Value.CurrencyServiceBaseAddress);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
            });
        }
    }
}
