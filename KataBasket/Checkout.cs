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

		public decimal GetTotalPrice()
		{
			var totalPrice = Items.Sum(i => i.Price);

			return totalPrice;
		}
	}
}
