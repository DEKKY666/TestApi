using Microsoft.AspNetCore.Mvc;
using TestApplication.Dal;
using TestApplication.Entities.Dto;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("Person")]
    public class PersonController : ControllerBase
    {    
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonDal _personDal;
        private readonly ICityDal _cityDal;

        public PersonController(IPersonDal personDal, ICityDal cityDal, ILogger<PersonController> logger)
        {
            _logger = logger;
            _cityDal = cityDal;
            _personDal = personDal;
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

        [HttpPost("CreateNewPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewPersonAsync(PersonRequestModel personModel)
        {
            try
            {
                var cities = await _cityDal.GetCitiesAsync();

                if (!cities.Any(c => c.Name == personModel.City)) throw new Exception("city is not found in database");
                await _personDal.CreateNewPersonAsync(personModel);
                return Ok();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }                        
        }
    }

}