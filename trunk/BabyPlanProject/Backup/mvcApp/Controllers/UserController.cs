﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyPlan.Common;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using BabyPlan.mvcApp.ViewModel;
using BabyPlan.DataStructure;
using System.Web.Security;

namespace BabyPlan.mvcApp.Controllers
{
    //[Authorize]
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public ActionResult Index()
        {

            ProductServiceClient client = new ProductServiceClient();
            AdvancedResult<List<ProProduct>> response = client.LoadInterestingBB(CurrentToken);

            List<ProductModel> items = new List<ProductModel>();

            if (response.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.Pics;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.ReplyCount;
                bindOptions.BindProductPicsCount = 1;

                response.Data = response.Data ?? new List<ProProduct>();
                foreach (var p in response.Data)
                {
                    items.Add(new ProductModel().Bind(p, bindOptions));
                }
            }
            return View("~/Views/User/Index.cshtml", items);
        }

        public ActionResult Goods(int? id)
        {
            ProductServiceClient client = new ProductServiceClient();

            int page = id ?? 0;

            AdvancedResult<PageEntity<ProProduct>> products = client.SearchBBPostList(CurrentToken, page, 6);
            List<ProductModel> items = new List<ProductModel>();

            ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
            bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.ReplyCount | ProductBindType.Pics;
            bindOptions.BindProductPicsCount = 1;

            if (products.Error == DataStructure.AppError.ERROR_SUCCESS)
            {
                products.Data.Items = products.Data.Items ?? new List<ProProduct>();
                ViewBag.Paging = BabyPlan.Common.HtmlFormatHelper.GeneratePagingHtml(page, 6, products.Data.RecordsCount, Url.Action("Goods", "User") + "/{0}");
                foreach(var p in products.Data.Items)
                {
                    items.Add(new ProductModel().Bind(p, bindOptions));
                }
            }
            return View("~/Views/User/Goods.cshtml", items);
        }
        
        public ActionResult AddGoods(int? id)
        {
            int pid = id ?? 0;
            ProductModel model = new ProductModel();

            ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
            bindOptions.BindProductPicsCount = 0;
            bindOptions.ProductBindType = ProductBindType.Author | ProductBindType.Pics;

            if (pid > 0)
            {
                ProductServiceClient client = new ProductServiceClient();
                AdvancedResult<ProProduct> product = client.GetBBInfo(pid);
                if (product.Error == DataStructure.AppError.ERROR_SUCCESS)
                {
                    model.Bind(product.Data, bindOptions);
                }
                foreach (var item in model.Pics)
                {
                    item.DisplaySeting(126, 126);
                }
            }
            else
            {

                model = new ProductModel();
                model.IsWash = true;
            }

            ViewBag.ProductJson = JsonHelper.Serialize(model);
            
            return View("~/Views/User/AddGoods.cshtml", model);
        }

        public JsonResult CloseGoods()
        {
            int id = 0;
            RespResult response;
            if (int.TryParse(Request["productId"], out id))
            {
                ProductServiceClient client = new ProductServiceClient();
                response = client.CloseBBPost(id, CurrentToken);
            }
            else
            {
                response = new RespResult();
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "无效的宝贝ID!";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OpenGoods()
        {
            int id = 0;
            RespResult response;
            if (int.TryParse(Request["productId"], out id))
            {
                ProductServiceClient client = new ProductServiceClient();
                response = client.OpenBBPost(id, CurrentToken);
            }
            else
            {
                response = new RespResult();
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "无效的宝贝ID!";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 提交商品数据
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxAddGoods()
        {
            string userToken = CurrentToken;
            string goods = Request["goods"];
            string del_items = Request["del_items"] ?? "";
            RespResult response = new RespResult();
            if (string.IsNullOrEmpty(goods))
            {
                response.Error = DataStructure.AppError.ERROR_INVALID_PARAMETER;
                response.ErrorMessage = "数据丢失！";
            }
            else
            {
                try
                {
                    ProductModel product = JsonHelper.Deserialize<ProductModel>(goods);
                    product.QQ = product.QQ == "QQ" ? string.Empty : product.QQ;
                    product.Phone = product.Phone == "手机号" ? string.Empty : product.Phone;
                    if (!string.IsNullOrEmpty(CurrentToken))
                    {
                        userToken = CurrentToken;
                    }
                        //未登录的情况（先注册）
                    else
                    {
                        string username = string.IsNullOrEmpty(product.Phone) || product.Phone == "手机号" ? product.QQ : product.Phone;
                        UserServiceClient userclient = new UserServiceClient();
                        try
                        {
                            
                            response = userclient.Register(username, SecurityHelper.MD5("000000"));
                            if (response.Error == AppError.ERROR_SUCCESS)
                            {
                                AdvancedResult<string> regLogin = userclient.Login(username, SecurityHelper.MD5("000000"));
                                userToken = regLogin.Data;
                            }
                        }
                        finally
                        {
                            userclient.Close();
                            userclient = null;
                        }
                        
                    }

                    ProductServiceClient client = new ProductServiceClient();

                    if (product == null)
                    {
                        response.Error = DataStructure.AppError.ERROR_INVALID_PARAMETER;
                        response.ErrorMessage = "无效数据！";
                    }
                    else
                    {
                        AdvancedResult<int> productResponse;
                        if (product.Id > 0)
                        {
                            RespResult result = client.EditBBPostInfo(product.Id, product.Title, product.QQ, product.Phone, product.Description, product.IsWash, product.Price, product.Age, product.ItemType,product.ItemSort, product.Sex, userToken);
                            productResponse = new AdvancedResult<int>();
                            productResponse.Data = product.Id;
                            productResponse.Error = result.Error;
                            productResponse.ErrorMessage = result.ErrorMessage;
                            productResponse.ExMessage = result.ExMessage;
                        }
                        else
                        {
                            productResponse = client.publishBBPost(product.Title, product.QQ, product.Phone, product.Description, product.IsWash, product.Price, product.Age, product.ItemType,product.ItemSort, product.Sex, userToken);
                        }

                        if (productResponse.Error == DataStructure.AppError.ERROR_SUCCESS)
                        {
                            foreach (var pitem in product.Pics)
                            {
                                //if (pitem.Pid > 0)
                                //{
                                //    client.EditeBBPic(pitem.Pid, pitem.PicId, CurrentToken);
                                //}
                                //else
                                //{
                                    client.BindBBPic(productResponse.Data, pitem.PicId, userToken);
                                //}
                            }
                        }
                        response.ErrorMessage = productResponse.ErrorMessage;
                        response.ExMessage = productResponse.ExMessage;
                        response.Error = productResponse.Error;

                    }
                    int[] delItemsList = del_items.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(a => Convert.ToInt32(a)).Where(a => a > 0).ToArray();
                    foreach (int itemId in delItemsList)
                    {
                        client.DeleteBBPic(itemId, userToken);
                    }
                    client.Close();
                    client = null;
                    
                }
                catch (Exception ex)
                {
                    response.ExMessage = "保存失败！" + ex.Message;
                    response.Error = DataStructure.AppError.ERROR_FAILED;
                }
            }
            return Json(response);
        }

        public JsonResult BindBabyAge()
        {
            int babyAge;
            RespResult result = new RespResult();
            if (int.TryParse(Request["babyage"], out babyAge))
            {
                UserServiceClient client = new UserServiceClient();
                result = client.UpdateBabyAge(babyAge, CurrentToken);
            }
            else
            {
                result.Error = DataStructure.AppError.ERROR_FAILED;
                result.ExMessage = "无效的年龄！";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Profile()
        {
            return View("~/Views/User/Profile.cshtml");
        }
        //public JsonResult AjaxGetIsLogin()
        //{
        //    string token = Request["token"];
        //    UserServiceClient client = new UserServiceClient();
        //    AdvancedResult<bool> response = new AdvancedResult<bool>();
        //    var user = client.CheckLogin(token);
        //    if (!user.Data)
        //    {
        //        response.Data = user.Data;
        //        response.Error = AppError.ERROR_PERSON_NOT_LOGIN;
        //        response.ExMessage = "用户未登录！";
        //    }
        //    else
        //    {
        //        response.Data = user.Data;
        //        response.Error = AppError.ERROR_PERSON_LOGINING;
        //        response.ExMessage = "用户已登录！";
        //    }
        //    client.Close();
        //    client = null;
        //    return Json(response);
        //}
        public JsonResult AjaxProfile()
        {
            string oldpassword = Request["u_old_password"];
            string newpassword = Request["u_new_password"];
            SexType sex = SexType.Ignore;
            Enum.TryParse<SexType>(Request["u_sex"], out sex);

            AdvancedResult<string> response = new AdvancedResult<string>();

            if (string.IsNullOrEmpty(oldpassword))
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "原密码不能为空！";
                return Json(response);
            }
            if (string.IsNullOrEmpty(newpassword) || newpassword.Length < 6 || newpassword.Length > 18)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "新密码长度不合法！";
                return Json(response);
            }
            UserServiceClient client = new UserServiceClient();
            AdvancedResult<AdUser> userResponse = client.GetUserInfo(CurrentToken);
            if (userResponse.Error == AppError.ERROR_SUCCESS)
            {
                if (!userResponse.Data.Pwd.Equals(SecurityHelper.MD5(oldpassword)))
                {
                    response.Error = AppError.ERROR_FAILED;
                    response.ExMessage = "原密码错误！";
                }
                else
                {
                    userResponse.Data.Pwd = SecurityHelper.MD5(newpassword);
                    userResponse.Data.Sex = sex;
                    var  saveresponse = client.EditeUserInfo(userResponse.Data, CurrentToken);
                    response.Error = saveresponse.Error;
                    response.ErrorMessage = saveresponse.ErrorMessage;
                    response.ExMessage = saveresponse.ExMessage;
                }
            }
            return Json(response);
        }

        public ActionResult Info()
        {
            ReplyServiceClient client = new ReplyServiceClient();
            AdvancedResult<PageEntity<GenReply>> replys = client.LoadMyBBReplyList(CurrentToken, 0, 9);
            IList<ReplyModel> models = new List<ReplyModel>();
            if (replys.Error == AppError.ERROR_SUCCESS)
            {
                ViewModelBindOption bindOptions = ViewModelBindOption.DefalutBindOption;
                bindOptions.BindReplyCount = 0;
                bindOptions.ProductBindType = bindOptions.ProductBindType | ProductBindType.ReplyCount;
                bindOptions.ReplayBindType = ReplyBindType.Author | ReplyBindType.Product;

                models = ReplyModel.BindList(replys.Data.Items, bindOptions);
            }
            foreach (ReplyModel r in models)
            {
                if (r.Author != null && r.Author.HeadPic != null)
                {
                    r.Author.HeadPic.DisplaySeting(50, 50);
                }
            }
            return View("~/Views/User/Info.cshtml", models);
        }

        public ActionResult BookList()
        {
            return View("~/Views/User/BookList.cshtml");
        }
    }
}