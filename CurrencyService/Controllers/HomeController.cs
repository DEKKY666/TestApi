using Microsoft.AspNetCore.Mvc;
using CurrencyService.Models;
using System.Diagnostics;
using CurrencyService.Repository.Dal;

namespace CurrencyService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrencyDal _currencyDal;

        public HomeController(ICurrencyDal currencyDal, ILogger<HomeController> logger)
        {
            _currencyDal = currencyDal;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index()
        {
            try {

                var currencies = await _currencyDal.LoadCurrencies(CancellationToken.None);

                return View(currencies);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex.Message);
                return BadRequest();
            }
          
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}