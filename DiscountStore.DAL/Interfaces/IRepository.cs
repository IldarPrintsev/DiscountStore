using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountStore.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task CreateAsync(T item);

        Task DeleteAsync(int id);
    }
}
