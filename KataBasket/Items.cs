using System;

namespace KataBasket
{
	public class Item
	{
		public string SKU { get; }
		public decimal Price { get; }

		public Item(string sku, decimal price)
		{
			SKU = sku;
			Price = price;
		}
	}
}
