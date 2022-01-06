using NUnit.Framework;
using System.Collections.Generic;

namespace KataBasket.Tests
{
	public class CheckoutTests
	{
		[Test]
		public void GivenItems_CheckoutShouldScannIt()
		{
			var items = new List<Item>
			{
				new Item{ SKU="A99", Price=0.50m},
				new Item{ SKU="B15", Price=0.30m},
				new Item{ SKU="A99", Price=0.50m}
			};

			var checkout = new Checkout();

			foreach(var item in items)
			{
				checkout.Scan(item);
			}

			Assert.That(checkout.Items.Count, Is.EqualTo(items.Count));
			CollectionAssert.AreEquivalent(checkout.Items, items);
		}
	}
}