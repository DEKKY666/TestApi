namespace CurrencyService.Models
{
    public class CurrencyDto
    {
        public Guid Guid { get; set; }
        public DateTime BookDate { get; set; }
        public double Value { get; set; }
        public string CurrencyPair { get; set; }
    }
}
