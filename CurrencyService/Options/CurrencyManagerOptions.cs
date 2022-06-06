using System.ComponentModel.DataAnnotations;

namespace CurrencyService.Options
{
    public class CurrencyManagerOptions
    {
        [Required, Url]
        public string CurrencyServiceBaseAddress { get; set; }

    }
}
