using NUnit.Framework;
using System.Collections.Generic;

namespace KataBasket.Tests
{
	public class CheckoutTests
	{
		[Test]
		public void GivenItems_ShouldScannIt()
		{
			var items = new List<Item>
			{
				new Item{ SKU="A99", Price=0.50m},
				new Item{ SKU="B15", Price=0.30m},
				new Item{ SKU="A99", Price=0.50m}
			};

			var checkout = new Checkout();

			foreach (var item in items)
			{
				checkout.Scan(item);
			}

			Assert.That(checkout.Items.Count, Is.EqualTo(items.Count));
			CollectionAssert.AreEquivalent(checkout.Items, items);
		}

		[Test]
		public void GivenItemScanned_ShouldGiveTotalPrice()
		{
			var apple = new Item { SKU = "A99", Price = 0.50m };
			var biscuits = new Item { SKU = "B15", Price = 0.30m };
			var checkout = new Checkout();

			checkout.Scan(apple);
			Assert.That(checkout.GetTotalPrice(), Is.EqualTo(apple.Price));

			checkout.Scan(biscuits);
			Assert.That(checkout.GetTotalPrice(), Is.EqualTo(apple.Price + biscuits.Price));
		}

		[Test]
		public void GivenItemScanned_WhenOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			var apples = new List<Item> {
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m }
			};

			var biscuits = new Item { SKU = "B15", Price = 0.30m };

			var expectedOfferPrice = 1.60m;

			var checkout = new Checkout();
			apples.ForEach(checkout.Scan);
			checkout.Scan(biscuits);

			Assert.That(checkout.GetTotalPrice(new AppleOffer(3, 0.20m)), Is.EqualTo(expectedOfferPrice));
		}

		[Test]
		public void GivenMultiItemScanned_WhenOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			var apples = new List<Item> {
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m },
				new Item { SKU = "A99", Price = 0.50m }
			};

			var biscuits = new Item { SKU = "B15", Price = 0.30m };

			var expectedOfferPrice = 3.40m;

			var checkout = new Checkout();
			apples.ForEach(checkout.Scan);
			checkout.Scan(biscuits);

			Assert.That(checkout.GetTotalPrice(new AppleOffer(3, 0.20m)), Is.EqualTo(expectedOfferPrice));
		}
	}
}