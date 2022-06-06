using AutoMapper;
using CurrencyService.Models;

namespace CurrencyService.Repository.Dal
{
    public class CurrencyDal : ICurrencyDal
    {
        private readonly IRepository _repository;

        public CurrencyDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CurrencyViewModel>> LoadCurrencies(CancellationToken cancellationToken)
        {
            var currencyDtos = await _repository.LoadCurrencies(cancellationToken);
            return GetCurrencyViewModels(currencyDtos);
        }

        public async Task SaveCurrency(Dictionary<string, double> currencyDictionary, CancellationToken cancellationToken)
        {
            await _repository.SaveCurrency(GetDtosFromDictionary(currencyDictionary), CancellationToken.None);
        }

        private IEnumerable<CurrencyViewModel> GetCurrencyViewModels(IEnumerable<CurrencyDto> currencyDtos)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CurrencyDto, CurrencyViewModel>());
            var mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<CurrencyViewModel>>(currencyDtos);            
        }

        private List<CurrencyDto> GetDtosFromDictionary(Dictionary<string, double> currencyDictionary)
        {
            var currencies = new List<CurrencyDto>();

            for (int i = 0; i < currencyDictionary.Count; i++)
            {
                currencies.Add(new CurrencyDto
                {
                    Guid = Guid.NewGuid(),
                    BookDate = DateTime.Now,
                    CurrencyPair = currencyDictionary.Keys.ElementAt(i),
                    Value = currencyDictionary.Values.ElementAt(i)
                });
            }
            return currencies;
        }
    }
}
