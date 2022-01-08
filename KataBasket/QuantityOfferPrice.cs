using System.Collections.Generic;
using System.Linq;

namespace KataBasket
{
	public class QuantityOfferPrice : IOffer
	{
		private readonly int _offerQuantity;
		private readonly decimal _offerPrice;
		private readonly string _offerItemSKU;

		public QuantityOfferPrice(string offerItemSKU, int offerQuantity, decimal offerPrice)
		{
			_offerQuantity = offerQuantity;
			_offerPrice = offerPrice;
			_offerItemSKU = offerItemSKU;
		}

		public decimal Apply(List<Item> items)
		{
			// integer number of division to avoid fraction of offer quantity 
			int existingCount = items.Count(i => i.SKU == _offerItemSKU) / _offerQuantity;

			if (existingCount == 0 || _offerPrice == 0)
			{
				return 0m;
			}

			return existingCount * _offerPrice;
		}
	}
}
