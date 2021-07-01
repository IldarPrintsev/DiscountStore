using DiscountStore.DAL.Interfaces;
using DiscountStore.DAL.Models;
using DiscountStore.WEB.Interfaces;
using DiscountStore.WEB.Models;
using DiscountStore.WEB.Services;
using Moq;
using Xunit;

namespace DiscountStore.Tests
{
    public class CartTests
    {

        [Fact]
        public async void AddItemToEmptyCart()
        {
            int testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            mock.Setup(repo => repo.GetAsync(testItemId)).ReturnsAsync(GetTestItem());
            ICartService cartService = new CartService(mock.Object);

            var newCart = await cartService.AddAsync(testItemId, null);

            Assert.Single(newCart.CartItems);
            Assert.Equal(10, newCart.CartItems[0].TotalSum);
            Assert.Equal(1, newCart.CartItems[0].Count);
            Assert.Equal(10, newCart.TotalSum);
        }

        [Fact]
        public async void AddItemToCartWithAnotherItem()
        {
            int testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            mock.Setup(repo => repo.GetAsync(testItemId)).ReturnsAsync(GetTestItem());
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();
            testCart.CartItems.Add(new CartItem(new Item { Id = 2, Name = "Item2", Price = 20 }) { TotalSum = 20, Count = 1});

            var newCart = await cartService.AddAsync(testItemId, testCart);

            Assert.Equal(2, newCart.CartItems.Count);
            Assert.Equal(20, newCart.CartItems[0].TotalSum);
            Assert.Equal(10, newCart.CartItems[1].TotalSum);
            Assert.Equal(1, newCart.CartItems[0].Count);
            Assert.Equal(1, newCart.CartItems[1].Count);
            Assert.Equal(30, newCart.TotalSum);
        }

        [Fact]
        public async void AddItemToCartWithSameItem()
        {
            int testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            mock.Setup(repo => repo.GetAsync(testItemId)).ReturnsAsync(GetTestItem());
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();
            testCart.CartItems.Add(new CartItem(GetTestItem()) { TotalSum = 10, Count = 1 });

            var newCart = await cartService.AddAsync(testItemId, testCart);

            Assert.Single(newCart.CartItems);
            Assert.Equal(20, newCart.CartItems[0].TotalSum);
            Assert.Equal(2, newCart.CartItems[0].Count);
            Assert.Equal(20, newCart.TotalSum);
        }

        [Fact]
        public void RemoveLastItemFromCart()
        {
            var testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();
            testCart.CartItems.Add(new CartItem(GetTestItem()) { TotalSum = 10, Count = 1 });

            var newCart = cartService.Remove(testItemId, testCart);

            Assert.Empty(newCart.CartItems);
            Assert.Equal(0, newCart.TotalSum);
        }

        [Fact]
        public void RemoveOneItemFromCart()
        {
            var testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();
            testCart.CartItems.Add(new CartItem(GetTestItem()) { TotalSum = 20, Count = 2 });

            var newCart = cartService.Remove(testItemId, testCart);

            Assert.Single(newCart.CartItems);
            Assert.Equal(10, newCart.TotalSum);
        }

        [Fact]
        public async void AddItemsWithDiscount()
        {
            var testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            mock.Setup(repo => repo.GetAsync(testItemId)).ReturnsAsync(GetTestDiscountItem());
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();

            for(int i = 0; i < 5; i++)
            {
                testCart = await cartService.AddAsync(testItemId, testCart);
            }

            Assert.Equal(5, testCart.CartItems[0].Count);
            Assert.Equal(30, testCart.CartItems[0].TotalSum);
            Assert.Equal(30, testCart.TotalSum);
        }

        [Fact]
        public async void RemoveItemsWithDiscount()
        {
            var testItemId = 1;
            var mock = new Mock<IRepository<Item>>();
            mock.Setup(repo => repo.GetAsync(testItemId)).ReturnsAsync(GetTestDiscountItem());
            ICartService cartService = new CartService(mock.Object);
            var testCart = new CartViewModel();

            for (int i = 0; i < 5; i++)
            {
                testCart = await cartService.AddAsync(testItemId, testCart);
            }

            testCart = cartService.Remove(testItemId, testCart);

            Assert.Equal(4, testCart.CartItems[0].Count);
            Assert.Equal(20, testCart.CartItems[0].TotalSum);
            Assert.Equal(20, testCart.TotalSum);
        }

        private Item GetTestItem()
        {
            return new Item { Id = 1, Name = "Item1", Price = 10 };
        }

        private Item GetTestDiscountItem()
        {
            return new Item { Id = 1, Name = "Item1", Price = 10, Discount = new Discount { Count = 2, Value = 10} };
        }
    }
}
