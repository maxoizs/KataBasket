using System.Collections.Generic;

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
	}
}
