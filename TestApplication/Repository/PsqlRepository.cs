using System.Data;
using TestApplication.Entities;
using Npgsql;
using Dapper;

namespace TestApplication.Repository
{
    public class PsqlRepository : IRepository
    {
        private string _connectionString;
        public PsqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateNewPersonAsync(Person person, CancellationToken cancellationToken)
        {
            using var db = new NpgsqlConnection(_connectionString);

            var sqlQuery = "INSERT INTO public.person (Guid, Name, PhoneNumber, Email, City, CreateDate) " +
                "VALUES(@Guid, @Name, @PhoneNumber, @Email, @City, @CreateDate)";
            await db.OpenAsync(cancellationToken);

            await db.ExecuteAsync(sqlQuery, new
            {
                Guid = person.Guid,
                Name = person.Name,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                City = person.City,
                CreateDate = person.CreateDate
            });

            await db.CloseAsync();
        }
        public async Task<IEnumerable<City>> GetCitiesAsync(CancellationToken cancellationToken)
        {
            using var db = new NpgsqlConnection(_connectionString);

            var sqlQuery = "select * from public.city;";
            await db.OpenAsync(cancellationToken);

            var reader = await db.ExecuteReaderAsync(sqlQuery);

            var cities = new List<City>();
            while (await reader.ReadAsync(cancellationToken))
            {
                cities.Add(new City { Name = reader.GetFieldValue<string>("Name") });
            }
            await db.CloseAsync();
            return cities;
        }
    }
}
