using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyPlan.Model.Product
{
    [Serializable]
    public class Product
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string QQ { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }

        public IList<ProductItem> Items { get; set; }
    }
}
