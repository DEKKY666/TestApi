using System.Text.Json;

namespace CurrencyService
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CurrencyManager> _logger;
        public CurrencyManager(HttpClient httpClient, ILogger<CurrencyManager> logger)
        { 
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<T> GetCurrencyAsync<T>(string query, CancellationToken cancellationToken)
        {
            var currencyServiceAddress = _httpClient.BaseAddress.OriginalString + query;
            _logger.LogInformation($"sending request to {currencyServiceAddress}...");

            var response = await _httpClient.GetAsync(currencyServiceAddress, cancellationToken);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"request failed: code {response.StatusCode}: {response.ReasonPhrase}");

            using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);           
            var currency = await JsonSerializer.DeserializeAsync<T>(responseStream, cancellationToken: cancellationToken);
            _logger.LogInformation($"got results from {currencyServiceAddress}");

            return currency;
        }
    }
}
