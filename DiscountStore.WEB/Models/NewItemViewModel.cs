using System.ComponentModel.DataAnnotations;

namespace DiscountStore.WEB.Models
{
    public class NewItemViewModel
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        [Required]
        public double Price { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid sum")]
        public double? DiscontValue { get; set; }

        [Range(2, int.MaxValue, ErrorMessage = "Please enter a number more than 1")]
        public int? DiscontCount { get; set; }
    }
}
