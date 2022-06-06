using CurrencyService.Models;

namespace CurrencyService.Repository.Dal
{
    public interface ICurrencyDal
    {
        Task<IEnumerable<CurrencyViewModel>> LoadCurrencies(CancellationToken cancellationToken);
        Task SaveCurrency(Dictionary<string, double> currencyDictionary, CancellationToken cancellationToken);
    }
}
