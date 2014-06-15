using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyPlan.Common;
using BabyPlan.WcfService;
using BabyPlan.Meta;
using BabyPlan.mvcApp.ViewModel;
using BabyPlan.DataStructure;

namespace BabyPlan.mvcApp.Controllers
{
    public class TradeController : BaseController
    {
        //
        // GET: /Trade/
        public ActionResult List(ItemType category,ItemSort sort, SexType sex ,PriceRange range, int age)
        {
            ViewBag.ItemType = category;
            ViewBag.ItemSort = sort;
            ViewBag.SexType = sex;
            ViewBag.Age = age;
            ViewBag.PriceRange = range;
            return View("~/Views/Trade/Index.cshtml", GetGoods(0, 20, category,sort,range, sex, age).Data);
        }

        public JsonResult AjaxGetGoods()
        {
            ItemType itemType = ItemType.Ignore;
            SexType sexType = SexType.Ignore;
            ItemSort itemSort = ItemSort.Ignore;
            PriceRange priceRange = PriceRange.Ignore;
            int age = 0;
            int pageSize=20;
            int pageIndex=0;

            Enum.TryParse<ItemType>(Request["itemType"], out itemType);
            Enum.TryParse<SexType>(Request["sexType"], out sexType);
            Enum.TryParse<ItemSort>(Request["itemSort"], out itemSort);
            Enum.TryParse<PriceRange>(Request["priceRange"], out priceRange);
            int.TryParse(Request["age"],out age);
            int.TryParse(Request["page"], out pageIndex);
            return Json(GetGoods(pageIndex, pageSize, itemType,itemSort,priceRange, sexType, age), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGoodsDetail()
        {
            int id = 0;
            int.TryParse(Request["id"], out id);
            ProductServiceClient client = new ProductServiceClient();
            AdvancedResult<ProProduct> dbresponse = client.GetBBInfo(id);
            AdvancedResult<ProductModel> response = new AdvancedResult<ProductModel>();
            response.Error = dbresponse.Error;
            response.ErrorMessage = dbresponse.ErrorMessage;
            response.ExMessage = dbresponse.ExMessage;
            if (dbresponse.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                //bindOptions.ProductItemBindType = bindOptions.ProductItemBindType | ProductItemBindType.Product;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.Author | ProductBindType.ReplyCount| ProductBindType.Pics;
                bindOptions.BindProductPicsCount = 0;
                response.Data = new ProductModel().Bind(dbresponse.Data, bindOptions);
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private AdvancedResult<List<ProductModel>> GetGoods(int pageIndex,int pageSize, ItemType itemType, ItemSort itemSort,PriceRange bbRange, SexType sexType, int age)
        {
            ProductServiceClient client = new ProductServiceClient();
            AdvancedResult<PageEntity<ProProduct>> response = client.LoadBBList(itemType, itemSort,bbRange, sexType, age, pageSize, pageIndex);

            List<ProductModel> items = new List<ProductModel>();

            if (response.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                response.Data.Items = response.Data.Items ?? new List<ProProduct>();

                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.ReplyCount| ProductBindType.Pics;
                bindOptions.BindProductPicsCount = 1;

                foreach (var p in response.Data.Items)
                {
                    items.Add(new ProductModel().Bind(p, bindOptions));
                }
            }
            AdvancedResult<List<ProductModel>> result = new AdvancedResult<List<ProductModel>>();
            result.Error = response.Error;
            result.ErrorMessage = response.ErrorMessage;
            result.ExMessage = response.ExMessage;
            result.Data = items;
            return result;
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ItemId = id;
            ProductServiceClient client = new ProductServiceClient();
            AdvancedResult<List<ProProduct>> response = client.LoadInterestingBB(CurrentToken);

            List<ProductModel> interestings = new List<ProductModel>();

            if (response.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                response.Data = response.Data ?? new List<ProProduct>();
                response.Data = response.Data.Take(4).ToList();

                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.Pics;

                foreach (var p in response.Data)
                {
                    interestings.Add(new ProductModel().Bind(p, bindOptions));
                }
            }

            ProductModel productModel = new ProductModel();
            AdvancedResult<ProProduct> product = client.GetBBInfo(id);
            if (product.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.BindProductPicsCount = 0;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.Author | ProductBindType.Pics;
                bindOptions.UserBindType = bindOptions.UserBindType | UserBindType.PubProductCount;
                productModel.Bind(product.Data, bindOptions);

                client.SetBBPostViewNum(product.Data.Pid);
            }

            ViewBag.Product = productModel;
            ViewBag.Interests = interestings;

            return View("~/Views/Trade/Detail.cshtml");
        }

        public JsonResult GetReplys(int pid)
        {
            int pageIndex = 0;
            int pageSize = 10;
            int.TryParse(Request["page"], out pageIndex);
            ReplyServiceClient client = new ReplyServiceClient();
            AdvancedResult<PageEntity<GenReply>> response = client.LoadReplyListByBBPostID(pid, pageIndex, pageSize);
            IList<ReplyModel> models = new List<ReplyModel>();
            string paging = string.Empty; 
            int count = 0;
            if (response.Error == AppError.ERROR_SUCCESS)
            {
                count = response.Data.RecordsCount;
                paging = HtmlFormatHelper.GeneratePagingHtml(response.Data.PageIndex, response.Data.PageSize, response.Data.RecordsCount, "javascript:void(LoadComments({0}))", "下一页", "上一页", null, null, null);

                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.BindReplyCount = 0;
                bindOptions.BindRefReplyCount = 1;
                bindOptions.ReplayBindType = ReplyBindType.Author | ReplyBindType.RefReply;
                

                models = ReplyModel.BindList(response.Data.Items, bindOptions);
            }
            foreach (ReplyModel r in models)
            {
                if (r.Author != null && r.Author.HeadPic != null)
                {
                    r.Author.HeadPic.DisplaySeting(50, 50);
                }
            }
            return Json(new { data = models, paging = paging, count = count }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxReplay(int pid)
        {
            string content = Request["comment_input"];
            int ref_id = 0;
            int.TryParse(Request["ref_id"], out ref_id);
            RespResult response = new RespResult();

            if (string.IsNullOrEmpty(content) || content.Length < 2 || content.Length > 300)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "回复内容长度不合法！";
                return Json(response);
            }

            ReplyServiceClient client = new ReplyServiceClient();
            response = client.ReplyBBPost(pid, ref_id, content, CurrentToken);
            return Json(response);
        }
    }
}
