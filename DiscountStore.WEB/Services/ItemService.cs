using DiscountStore.DAL.Interfaces;
using DiscountStore.DAL.Models;
using DiscountStore.WEB.Interfaces;
using DiscountStore.WEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _repository;

        public ItemService(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(NewItemViewModel newItem)
        {
            Discount discount = null;
            if (newItem.DiscontCount.HasValue && newItem.DiscontValue.HasValue)
            {
                discount = new Discount {Count = newItem.DiscontCount.Value, Value = newItem.DiscontValue.Value };
            }

            var item = new Item { Name = newItem.Name, Price = newItem.Price, Discount = discount };

            await _repository.CreateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
