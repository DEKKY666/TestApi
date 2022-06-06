namespace CurrencyService
{
    public interface ICurrencyManager
    {
        Task<T> GetCurrencyAsync<T>(string query, CancellationToken cancellationToken);
    }
}
