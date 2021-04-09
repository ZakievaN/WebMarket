using System.Collections.Generic;
using System.Linq;

namespace WebMarketDomain.Entityes
{
    public class Card
    {
        public ICollection<CardItem> Items { get; set; } = new List<CardItem>();

        public int ItemsCount()
        {
            return Items?.Sum(Items => Items.Quality) ?? 0;
        }
    }
}
