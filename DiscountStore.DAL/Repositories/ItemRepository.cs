using DiscountStore.DAL.Interfaces;
using DiscountStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountStore.DAL.Repositories
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly ApplicationContext _db;

        public ItemRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _db.Items.Include(u => u.Discount).ToListAsync();
        }

        public async Task<Item> GetAsync(int id)
        {
            return await _db.Items.Include(u => u.Discount).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Item item)
        {
            _db.Items.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Item item = new Item { Id = id };
            _db.Entry(item).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }
    }
}
