using TestApplication.Entities;

namespace TestApplication.Repository
{
    public interface IRepository
    {
        Task CreateNewPersonAsync(Person person, CancellationToken cancellationToken);
        Task<IEnumerable<City>> GetCitiesAsync(CancellationToken cancellationToken);
    }
}
