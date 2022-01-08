using System.Collections.Generic;
using System.Linq;

namespace KataBasket
{
	public class Checkout
	{
		public List<Item> Items { get; }

		public Checkout()
		{
			Items = new List<Item>();
		}

		public void Scan(Item item)
		{
			if (item != default(Item))
			{
				Items.Add(item);
			}
		}

		public decimal GetTotalPrice(params IOffer[] offers)
		{
			var totalPrice = Items.Sum(i => i.Price);
			var offerPrice = 0.0m;

			foreach (IOffer offer in offers)
			{
				offerPrice += offer.Apply(Items);
			}

			return totalPrice - offerPrice;
		}
	}
}
