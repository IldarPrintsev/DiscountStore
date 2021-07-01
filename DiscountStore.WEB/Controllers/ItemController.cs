using DiscountStore.WEB.Interfaces;
using DiscountStore.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetAllAsync();

            return View(new ItemListViewModel { Items = items });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewItemViewModel newItem)
        {
            await _itemService.CreateAsync(newItem);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
