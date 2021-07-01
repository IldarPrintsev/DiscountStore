namespace DiscountStore.DAL.Models
{
    public class CartItem
    {
        public CartItem(Item item)
        {
            Item = item;
            Count = 1;
        }

        public Item Item { get; }

        public int Count { get; set; }

        public double TotalSum { get; set; }
    }
}
