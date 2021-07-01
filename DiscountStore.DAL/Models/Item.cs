using System;

namespace DiscountStore.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Discount Discount { get; set; }

    }

    public class Discount
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public double Value { get; set; }

        [NonSerialized]
        public Item Item;

        [NonSerialized]
        public int ItemId;
    }
}
