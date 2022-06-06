using CurrencyService.Models;

namespace CurrencyService.Repository
{
    public interface IRepository
    {
        Task SaveCurrency(IEnumerable<CurrencyDto> currencies, CancellationToken cancellationToken);
        Task<IEnumerable<CurrencyDto>> LoadCurrencies(CancellationToken cancellationToken);

    }
}
