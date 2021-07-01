using DiscountStore.DAL.Models;
using DiscountStore.WEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllAsync();

        Task CreateAsync(NewItemViewModel newItem);

        Task DeleteAsync(int id);
    }
}
