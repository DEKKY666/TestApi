using TestApplication.Entities;

namespace TestApplication.Dal
{
    public interface ICityDal 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
    }
}
