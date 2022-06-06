using CurrencyService.Options;
using CurrencyService.Repository.Dal;
using Microsoft.Extensions.Options;
using Quartz;

namespace CurrencyService.Jobs
{
    public class CurrencyJob : IJob
    {
        private readonly ILogger<CurrencyJob> _logger;
        private readonly ICurrencyManager _currencyManager;
        private readonly CurrencyJobOptions _options;
        private readonly ICurrencyDal _currencyDal;

        public CurrencyJob(ICurrencyManager currencyManager, ICurrencyDal currencyDal, IOptions<CurrencyJobOptions> options, ILogger<CurrencyJob> logger)
        {
            _logger = logger;
            _currencyManager = currencyManager;
            _currencyDal = currencyDal;
            _options = options.Value;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation($"{DateTime.Now}: starting job");

                var currencyDictionary = await _currencyManager.GetCurrencyAsync<Dictionary<string, double>>($"?q={string.Join(',', _options.CurrencyPairs)}&compact=ultra&apiKey={_options.Key}", CancellationToken.None);

                if (currencyDictionary is null || !currencyDictionary.Any()) throw new ArgumentNullException("got empty sequence from service");
                                
                _logger.LogInformation($"{DateTime.Now}: saving currency to database");
                await _currencyDal.SaveCurrency(currencyDictionary, CancellationToken.None);
                _logger.LogInformation($"{DateTime.Now}: saved success");

                _logger.LogInformation($"{DateTime.Now}: job's done");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

      

    }
}
