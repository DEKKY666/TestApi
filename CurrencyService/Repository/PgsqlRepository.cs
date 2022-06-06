using CurrencyService.Models;
using System.Data;
using Dapper;
using Npgsql;

namespace CurrencyService.Repository
{
    public class PgsqlRepository : IRepository
    {
        private string _connectionString;
        public PgsqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CurrencyDto>> LoadCurrencies(CancellationToken cancellationToken)
        {
            using var db = new NpgsqlConnection(_connectionString);

            var sqlQuery = "select * from public.currency;";
            await db.OpenAsync(cancellationToken);

            var reader = await db.ExecuteReaderAsync(sqlQuery);

            var currencies = new List<CurrencyDto>();
            while (await reader.ReadAsync(cancellationToken))
            {
                currencies.Add(new CurrencyDto {
                    Guid = reader.GetFieldValue<Guid>("Guid"),
                    BookDate = reader.GetFieldValue<DateTime>("BookDate"), 
                    CurrencyPair = reader.GetFieldValue<string>("CurrencyPair"),
                    Value = reader.GetFieldValue<double>("Value")
                });
            }

            await db.CloseAsync();
            return currencies;
        }

        public async Task SaveCurrency(IEnumerable<CurrencyDto> currencies, CancellationToken cancellationToken)
        {
            using var db = new NpgsqlConnection(_connectionString);

            var sqlQuery = "COPY public.currency (Guid, CurrencyPair, Value, BookDate) FROM STDIN BINARY";
            await db.OpenAsync(cancellationToken);

            using (var writer = await db.BeginBinaryImportAsync(sqlQuery, cancellationToken))
            {
                foreach (var currency in currencies)
                { 
                    writer.StartRow();
                    writer.Write(currency.Guid, NpgsqlTypes.NpgsqlDbType.Uuid);
                    writer.Write(currency.CurrencyPair, NpgsqlTypes.NpgsqlDbType.Char);
                    writer.Write(currency.Value, NpgsqlTypes.NpgsqlDbType.Double);
                    writer.Write(currency.BookDate, NpgsqlTypes.NpgsqlDbType.Timestamp);
                }
                await writer.CompleteAsync(cancellationToken);
            }              

            await db.CloseAsync();
        }
    }
}
