using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using BabyPlan.Common;
using BabyPlan.mvcApp.ViewModel;
using Common.Logging;

namespace BabyPlan.mvcApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILog _logger;
        public BaseController()
        {
            _logger = LogManager.GetLogger(GetType());
        }

        protected ILog Logger
        {
            get { return _logger; }
        }

        #region token
        private string currentToken;
        public string CurrentToken {
            get {
                if (currentToken != null)
                    return currentToken;
                if (FormsAuthentication.CookiesSupported)
                {
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                              Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                            currentToken = ticket.UserData;
                        }
                        catch (Exception e)
                        {
                            // 票据解密失败
                            Logger.Error(e);
                        }
                    }
                }
                else
                {
                      //客户端不支持Cookie
                }
                return currentToken;
            }
        }
        #endregion

        #region adUser
        private AdUser currentUser;
        public AdUser CurrentUser
        {
            get
            {
                if (CurrentToken == null)
                {
                    return null;
                }
                if (currentUser != null)
                    return currentUser;
                UserServiceClient client = new UserServiceClient();
                AdvancedResult<AdUser> user = client.GetUserInfo(CurrentToken);
                if (user.Error != DataStructure.AppError.ERROR_SUCCESS || user.Data == null)
                {
                    Logger.Error("login state losted!");
                    //client.Logout(CurrentToken);
                    //FormsAuthentication.SignOut();
                }
                currentUser = user.Data;
                client.Close();
                client = null;
                return currentUser;
            }
        }
        #endregion

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                ViewBag.LoginUser = new UserModel().Bind(CurrentUser, ViewModelBindOption.DefalutBindOption);
                ViewBag.CurrentToken = CurrentToken;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            base.OnAuthorization(filterContext);
        }
    }
}
