using DiscountStore.WEB.Models;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> AddAsync(int itemId, CartViewModel currentCart);

        CartViewModel Remove(int itemId, CartViewModel currentCar);
    }
}
