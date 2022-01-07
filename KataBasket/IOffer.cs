using System.Collections.Generic;
using System.Linq;

namespace KataBasket
{

	public interface IOffer
	{
		decimal Apply(List<Item> items);
	}

	public class AppleOffer : IOffer
	{
		private readonly int _offerCount;
		private readonly decimal _offerPrice;
		private readonly string SKU = "A99";

		public AppleOffer(int offeredCount, decimal offerPrice)
		{
			_offerCount = offeredCount;
			_offerPrice = offerPrice;
		}

		public decimal Apply(List<Item> items)
		{
			int existingCount = items.Count(i => i.SKU == SKU) / _offerCount;

			if (existingCount == 0 || _offerPrice == 0)
			{
				return 0m;
			}

			return existingCount * _offerPrice;
		}
	}
}
