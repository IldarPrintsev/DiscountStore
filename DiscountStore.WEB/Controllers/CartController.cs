using DiscountStore.WEB.Extensions;
using DiscountStore.WEB.Interfaces;
using DiscountStore.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiscountStore.WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var viewModel = HttpContext.Session.GetData<CartViewModel>(nameof(CartViewModel));
            if (viewModel == null)
            {
                viewModel = new CartViewModel();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            var currentCart = HttpContext.Session.GetData<CartViewModel>(nameof(CartViewModel));
            var newCart = await _cartService.AddAsync(id, currentCart);
            HttpContext.Session.SetData(nameof(CartViewModel), newCart);

            return RedirectToAction("Index", "Item");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var currentCart = HttpContext.Session.GetData<CartViewModel>(nameof(CartViewModel));
            var newCart = _cartService.Remove(id, currentCart);
            HttpContext.Session.SetData(nameof(CartViewModel), newCart);

            return RedirectToAction("Index", "Cart");
        }
    }
}
