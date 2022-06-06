using TestApplication.Entities;
using TestApplication.Repository;

namespace TestApplication.Dal
{
    public class CityDal : ICityDal
    {
        private readonly IRepository _repository;
        public CityDal(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _repository.GetCitiesAsync(CancellationToken.None);
        }
    }
}
