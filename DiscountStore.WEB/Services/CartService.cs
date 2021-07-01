using DiscountStore.DAL.Interfaces;
using DiscountStore.DAL.Models;
using DiscountStore.WEB.Interfaces;
using DiscountStore.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Item> _repository;

        public CartService(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<CartViewModel> AddAsync(int itemId, CartViewModel currentCart)
        {
            if (currentCart == null)
            {
                currentCart = new CartViewModel();
            }

            var cartItem = currentCart.CartItems.FirstOrDefault(x => x.Item.Id == itemId);

            if (cartItem == null)
            {
                var item = await _repository.GetAsync(itemId);
                cartItem = new CartItem(item);
                currentCart.CartItems.Add(cartItem);
            }
            else
            {
                IncrementCartItem(cartItem);
            }

            cartItem.TotalSum = CalculateItemSum(cartItem);

            currentCart.TotalSum = CalculateTotalSum(currentCart.CartItems);

            return currentCart;
        }

        public CartViewModel Remove(int itemId, CartViewModel currentCart)
        {
            var cartItem = currentCart.CartItems.First(x => x.Item.Id == itemId);
            if (cartItem.Count == 1)
            {
                currentCart.CartItems.Remove(cartItem);
            }
            else
            {
                DecrementCartItem(cartItem);
            }

            cartItem.TotalSum = CalculateItemSum(cartItem);

            currentCart.TotalSum = CalculateTotalSum(currentCart.CartItems);

            return currentCart;
        }

        private void IncrementCartItem(CartItem cartItem)
        {
            cartItem.Count++;
        }

        private void DecrementCartItem(CartItem cartItem)
        {
            cartItem.Count--;
        }

        private double CalculateItemSum(CartItem cartItem)
        {
            double totalSum = 0;
            int totalCount = cartItem.Count;
            if (cartItem.Item.Discount != null)
            {
                int discountGroupCount = cartItem.Count / cartItem.Item.Discount.Count;
                totalSum += discountGroupCount * cartItem.Item.Discount.Value;
                totalCount -= discountGroupCount * cartItem.Item.Discount.Count;
            }
            totalSum += totalCount * cartItem.Item.Price;

            return totalSum;
        }

        private double CalculateTotalSum(List<CartItem> cartItems)
        {
            double totalSum = 0;
            foreach (var cartItem in cartItems)
            {
                totalSum += cartItem.TotalSum;
            }

            return totalSum;
        }
    }
}
