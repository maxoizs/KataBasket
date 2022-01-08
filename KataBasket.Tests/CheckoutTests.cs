using NUnit.Framework;
using System.Collections.Generic;

namespace KataBasket.Tests
{
	public class CheckoutTests
	{
		private readonly Item Apple = new("A99", 0.50m);
		private readonly Item Biscuits = new("B15", 0.30m);
		private Checkout _checkout;


		[SetUp]
		public void Setup()
		{
			_checkout = new Checkout();
		}

		[Test]
		public void GivenItems_ShouldScannIt()
		{
			var items = new List<Item>
			{
				Apple,
				Biscuits,
				Apple
			};

			items.ForEach(_checkout.Scan);

			Assert.That(_checkout.Items.Count, Is.EqualTo(items.Count));
			CollectionAssert.AreEquivalent(_checkout.Items, items);
		}

		[Test]
		public void GivenNoItemScanned_ShouldGiveZero()
		{
			_checkout.Scan(null);

			Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(0));
		}

		[Test]
		public void GivenItemScanned_ShouldGiveTotalPrice()
		{
			_checkout.Scan(Apple);
			Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(Apple.Price));

			_checkout.Scan(Biscuits);
			Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(Apple.Price + Biscuits.Price));
		}

		[Test]
		public void GivenItemScanned_WhenOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			const decimal expectedOfferPrice = 1.60m;
			var items = new List<Item> {
				Apple,
				Biscuits,
				Apple,
				Apple
			};

			var appleOffer = new QuantityOfferPrice(Apple.SKU, 3, 0.20m);

			items.ForEach(_checkout.Scan);

			Assert.That(_checkout.GetTotalPrice(appleOffer), Is.EqualTo(expectedOfferPrice));
		}

		[Test]
		public void GivenMultiItemScanned_WhenOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			const decimal expectedOfferPrice = (2 * 1.3m) + 0.5m + 0.3m;
			var items = new List<Item> {
				Apple,
				Apple,
				Apple,
				Biscuits,
				Apple,
				Apple,
				Apple,
				Apple,
			};

			var appleOffer = new QuantityOfferPrice(Apple.SKU, 3, 0.20m);

			items.ForEach(_checkout.Scan);

			Assert.That(_checkout.GetTotalPrice(appleOffer), Is.EqualTo(expectedOfferPrice));
		}

		[Test]
		public void GivenMultiItemScanned_WhenMultiOfferExists_ShouldGiveTotalPriceWithOffer()
		{
			const decimal expectedOfferPrice = (2 * 1.30m) + 0.5m + (2 * 0.45m) + 0.30m;
			var items = new List<Item> {
				Apple,
				Apple,
				Biscuits,
				Apple,
				Biscuits,
				Apple,
				Apple,
				Biscuits,
				Apple,
				Biscuits,
				Apple,
				Biscuits
			};

			var appleOffer = new QuantityOfferPrice(Apple.SKU, 3, 0.20m);
			var biscuitsOffer = new QuantityOfferPrice(Biscuits.SKU, 2, 0.15m);

			items.ForEach(_checkout.Scan);

			Assert.That(_checkout.GetTotalPrice(appleOffer, biscuitsOffer), Is.EqualTo(expectedOfferPrice));
		}
	}
}