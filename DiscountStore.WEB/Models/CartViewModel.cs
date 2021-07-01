using DiscountStore.DAL.Models;
using System.Collections.Generic;

namespace DiscountStore.WEB.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }

        public double TotalSum { get; set; }
    }
}
