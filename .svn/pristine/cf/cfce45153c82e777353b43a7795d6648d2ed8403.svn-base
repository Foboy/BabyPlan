using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.DataStructure;
using System.Web.Routing;
using System.Text;

namespace BabyPlan.mvcApp.UrlRoutes
{
    public class TradeListParam
    {
        public TradeListParam()
        {
            this.Category = ItemType.Ignore;
            this.Sex = SexType.Ignore;
            this.Age = 0;
            this.Sort = ItemSort.AboutBaby;
            this.Range = PriceRange.Ignore;
        }

        public ItemType Category { get; set; }
        public SexType Sex { get; set; }
        public int Age { get; set; }
        public ItemSort Sort { get; set; }
        public PriceRange Range { get; set; }
    }
    public class TradeListParamManager
    {
        public static TradeListParam ParamParse(string virtualPath)
        {
            TradeListParam param = new TradeListParam();
            if (virtualPath.Contains("宝宝") || virtualPath.Contains("aboutbaby"))
            {
                param.Sort = ItemSort.AboutBaby;
            }
            if (virtualPath.Contains("妈妈") || virtualPath.Contains("aboutmother"))
            {
                param.Sort = ItemSort.AboutMother;
            }
            if (virtualPath.Contains("十元专区") || virtualPath.Contains("tenzone"))
            {
                param.Range = PriceRange.TenZone;
            }
            if (virtualPath.Contains("百元专区") || virtualPath.Contains("hundredzone"))
            {
                param.Range = PriceRange.HundredZone;
            }
            if (virtualPath.Contains("衣服") || virtualPath.Contains("clothes"))
            {
                param.Category = ItemType.Clothes;
            }
            else if (virtualPath.Contains("玩具") || virtualPath.Contains("toys"))
            {
                param.Category = ItemType.Toys;
            }
            else if (virtualPath.Contains("婴儿床") || virtualPath.Contains("cots"))
            {
                param.Category = ItemType.Cots;
            }
            else if (virtualPath.Contains("其他") || virtualPath.Contains("others"))
            {
                param.Category = ItemType.Others;
            }
            if (virtualPath.Contains("男孩") || virtualPath.Contains("公子"))
            {
                param.Sex = SexType.Man;
            }
            else if (virtualPath.Contains("女孩") || virtualPath.Contains("公主"))
            {
                param.Sex = SexType.Woman;
            }

            if (virtualPath.Contains("1岁"))
            {
                param.Age = 1;
            }
            else if (virtualPath.Contains("2岁"))
            {
                param.Age = 2;
            }
            else if (virtualPath.Contains("3岁"))
            {
                param.Age = 3;
            }
            else if (virtualPath.Contains("4岁"))
            {
                param.Age = 4;
            }

            return param;
        }

        public static string GeneratParamUrl(RouteValueDictionary requestValues, RouteValueDictionary values)
        {
            StringBuilder path = new StringBuilder();

            ItemSort itemSort = ItemSort.Ignore;
            RouteHepler.ParseRouteValue<ItemSort>(requestValues, values, "sort", out itemSort, itemSort);
            switch (itemSort)
            {
                case ItemSort.AboutBaby:
                    path.Append("aboutbaby");
                    break;
                case ItemSort.AboutMother:
                    path.Append("Aboutmother");
                    break;
                default:
                    break;
            }
            ItemType itemType = ItemType.Ignore;
            RouteHepler.ParseRouteValue<ItemType>(requestValues, values, "category", out itemType, itemType);
            switch (itemType)
            {
                case ItemType.Clothes:
                    path.Append("-clothes");
                    break;
                case ItemType.Toys:
                    path.Append("-toys");
                    break;
                case ItemType.Cots:
                    path.Append("-cots");
                    break;
                case ItemType.Others:
                    path.Append("-others");
                    break;
                default:
                    break;
            }
            PriceRange priceRange = PriceRange.Ignore;
            RouteHepler.ParseRouteValue<PriceRange>(requestValues, values, "range", out priceRange, priceRange);
            switch (priceRange)
            {
                case PriceRange.TenZone:
                    path.Append("-tenzone");
                    break;
                case PriceRange.HundredZone:
                    path.Append("-hundredzone");
                    break;
                default:
                    break;
            }

            SexType sexType = SexType.Ignore;
            RouteHepler.ParseRouteValue<SexType>(requestValues, values, "sex", out sexType, sexType);
            switch (sexType)
            {
                case SexType.Man:
                    path.Append("-男孩");
                    break;
                case SexType.Woman:
                    path.Append("-女孩");
                    break;
            }

            int age = 0;
            RouteHepler.ParseRouteValue<int>(requestValues, values, "age", out age, age);
            switch (age)
            {
                case 1:
                    path.Append("-0-1岁");
                    break;
                case 2:
                    path.Append("-2岁");
                    break;
                case 3:
                    path.Append("-3岁");
                    break;
                case 4:
                    path.Append("-4岁");
                    break;
            }

            return path.ToString();
        }
    }
}