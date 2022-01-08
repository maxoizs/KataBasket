using NUnit.Framework;
using System.Collections.Generic;

namespace KataBasket.Tests
{
	public class CheckoutTests
	{
		private const string Apple = "A99";
		private const string Biscuits = "B15";

		[Test]
		public void GivenItems_ShouldScannIt()
		{
			var items = new List<Item>
			{
				new Item(Apple, 0.50m),
				new Item(Biscuits, 0.30m),
				new Item(Apple, 0.50m)
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
		public void GivenNoItemScanned_ShouldGiveZero()
		{
			var checkout = new Checkout();

			checkout.Scan(null);
			Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
		}

		[Test]
		public void GivenItemScanned_ShouldGiveTotalPrice()
		{
			var apple = new Item(Apple, 0.50m);
			var biscuits = new Item(Biscuits, 0.30m);
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
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m)
			};

			var biscuits = new Item(Biscuits, 0.30m);
			var appleOffer = new QuantityOfferPrice(Apple, 3, 0.20m);
			const decimal expectedOfferPrice = 1.60m;

			var checkout = new Checkout();
			apples.ForEach(checkout.Scan);
			checkout.Scan(biscuits);

			Assert.That(checkout.GetTotalPrice(appleOffer), Is.EqualTo(expectedOfferPrice));
		}

		[Test]
		public void GivenMultiItemScanned_WhenOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			var items = new List<Item> {
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Biscuits,  0.30m)
		};


			var appleOffer = new QuantityOfferPrice(Apple, 3, 0.20m);
			const decimal expectedOfferPrice = (2 * 1.3m) + 0.5m + 0.3m;

			var checkout = new Checkout();
			items.ForEach(checkout.Scan);

			Assert.That(checkout.GetTotalPrice(appleOffer), Is.EqualTo(expectedOfferPrice));
		}

		[Test]
		public void GivenMultiItemScanned_WhenMultiOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			var items = new List<Item> {
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Biscuits, 0.30m),
				new Item (Biscuits, 0.30m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Apple, 0.50m),
				new Item (Biscuits, 0.30m),
				new Item (Biscuits, 0.30m),
				new Item (Biscuits, 0.30m)
			};

			var appleOffer = new QuantityOfferPrice(Apple, 3, 0.20m);
			var biscuitsOffer = new QuantityOfferPrice(Biscuits, 2, 0.15m);
			const decimal expectedOfferPrice = (2 * 1.30m) + 0.5m + (2 * 0.45m) + 0.30m;

			var checkout = new Checkout();
			items.ForEach(checkout.Scan);

			Assert.That(checkout.GetTotalPrice(appleOffer, biscuitsOffer), Is.EqualTo(expectedOfferPrice));
		}
	}
}