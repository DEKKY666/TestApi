using Microsoft.AspNetCore.Mvc;
using TestApplication.Dal;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("City")]
    public class CityController : Controller
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityDal _cityDal;

        public CityController(ICityDal cityDal, ILogger<CityController> logger)
        {
            _logger = logger;
            _cityDal = cityDal;
        }

        [HttpGet("GetAvialableCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCitiesAsync()
        {
            try
            {
                return Ok(await _cityDal.GetCitiesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }


    }
}
