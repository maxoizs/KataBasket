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
			Items.Add(item);
		}

		public decimal GetTotalPrice(IOffer offer = null)
		{
			var totalPrice = Items.Sum(i => i.Price);
			var offerPrice = (offer?.Apply(Items)) ?? 0.0m;

			return totalPrice - offerPrice;
		}
	}
}
