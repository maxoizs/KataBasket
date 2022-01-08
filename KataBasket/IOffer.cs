using System.Collections.Generic;
using System.Linq;

namespace KataBasket
{
	public interface IOffer
	{
		decimal Apply(List<Item> items);
	}
}
