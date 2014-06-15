using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyPlan.DataStructure;
using SocialServices.Sina.oAuth;
using BabyPlan.oAuth;
using BabyPlan.mvcApp.Infras;
using BabyPlan.Common;
using System.Web.Security;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using QConnectSDK;
using QConnectSDK.Models;
using AMicroblogAPI;
using AMicroblogAPI.Common;
using AMicroblogAPI.DataContract;
using BabyPlan.mvcApp.ViewModel;
using BabyPlan.mvcApp.Configuration;

namespace BabyPlan.mvcApp.Controllers
{
    public class AccountController : BaseController
    {
        private const string SiteUrl = "";
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        public ActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }

        public ActionResult SocialBind()
        {
            //绑定第三方账号
            object su = Session[sKey_SocialUserKey];
            SocialUser socialUser = su != null ? su as SocialUser : null;
            string socialName = string.Empty;
            if (socialUser != null)
            {
                switch (socialUser.SocialUserType)
                {
                    case SocialUserTypeEnum.QQ:
                        try
                        {
                            OAuthToken oToken = new OAuthToken();
                            oToken.AccessToken = socialUser.AccessToken;
                            oToken.OpenId = socialUser.Uid;
                            QOpenClient qclient = new QOpenClient(oToken);
                            socialName = qclient.GetCurrentUser().Nickname;
                        }
                        catch
                        {
                        }
                        break;
                    case SocialUserTypeEnum.Sina:
                        try
                        {
                            OAuthAccessToken stoken = new OAuthAccessToken();
                            stoken.Token = socialUser.AccessToken;
                            stoken.UserID = socialUser.Uid;
                            socialName = AMicroblog.GetUserInfo(Convert.ToInt64(stoken.UserID), stoken).Name;
                            
                        }
                        catch
                        {
                        }
                        break;
                }
            }
            ViewBag.SocialName = socialName;
            return View("~/Views/Account/socialbind.cshtml");
        }

        public JsonResult AjaxRegister()
        {
            string username = Request["username"];
            string password = Request["password"];
            string sex = Request["sex"];
            string getUserInfo = Request["userinfo"];

            AdvancedResult<string> response = new AdvancedResult<string>();

            if (string.IsNullOrEmpty(username) || username.Length < 4 || username.Length > 16)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "用户名长度不合法！";
                return Json(response);
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 18)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "密码长度不合法！";
                return Json(response);
            }
            AdUser user = null;
            try
            {
                UserServiceClient client = new UserServiceClient();
                response = client.Register(username, SecurityHelper.MD5(password));
                if (response.Error == AppError.ERROR_SUCCESS)
                {
                    //AdvancedResult<AdUser> getResponse = client.GetUserInfo(response.Data);
                    //if (getResponse.Error == AppError.ERROR_SUCCESS)
                    //{
                    //    SexType sexType = SexType.Ignore;
                    //    Enum.TryParse<SexType>(sex, out sexType);
                    //    getResponse.Data.Sex = sexType;
                    //    client.EditeUserInfo();
                    //}
                    AdvancedResult<string> regLogin = Login(username, password, ref user);
                    if (regLogin.ExMessage != null && regLogin.ExMessage.Length > 0)
                    {
                        response.ExMessage = string.Format("注册成功！,{0}", regLogin.ExMessage);
                    }
                }
                client.Close();
                client = null;
            }
            catch (Exception ex)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = ex.Message;
            }
            if (string.IsNullOrEmpty(getUserInfo))
            {
                return this.Json(response);
            }
            else
            {
                AdvancedResult<UserModel> userResponse = new AdvancedResult<UserModel>();
                if (response.Error != AppError.ERROR_SUCCESS)
                {
                    return this.Json(response);
                }
                else
                {
                    userResponse.Error = response.Error;
                    userResponse.ErrorMessage = response.ErrorMessage;
                    userResponse.ExMessage = response.ExMessage;
                    ViewModelBindOption bindoption = new ViewModelBindOption();
                    userResponse.Data = new UserModel().Bind(user, bindoption);
                }
                return this.Json(userResponse);
            }
        }

        public JsonResult AjaxLogin()
        {
            string username = Request["username"];
            string password = Request["password"];
            string getUserInfo = Request["userinfo"];
            AdUser user = null;
            AdvancedResult<string> response = Login(username, password, ref user);
            if (string.IsNullOrEmpty(getUserInfo))
            {
                return this.Json(response);
            }
            else
            {
                AdvancedResult<UserModel> userResponse = new AdvancedResult<UserModel>();
                if (response.Error != AppError.ERROR_SUCCESS)
                {
                    return this.Json(response);
                }
                else
                {
                    userResponse.Error = response.Error;
                    userResponse.ErrorMessage = response.ErrorMessage;
                    userResponse.ExMessage = response.ExMessage;
                    ViewModelBindOption bindoption = new ViewModelBindOption();
                    userResponse.Data = new UserModel().Bind(user, bindoption);
                }
                return this.Json(userResponse);
            }
        }

        private AdvancedResult<string> Login(string username, string password,ref AdUser loginUser)
        {
            UserServiceClient client = new UserServiceClient();
            AdvancedResult<string> response = new AdvancedResult<string>();
            if (string.IsNullOrEmpty(username) || username.Length < 4 || username.Length > 16)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "登陆失败！用户名长度不合法！";
                return response;
            }
            if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 18)
            {
                response.Error = AppError.ERROR_FAILED;
                response.ExMessage = "登陆失败！密码长度不合法！";
                return response;
            }

            response = client.Login(username, SecurityHelper.MD5(password));


            if (response.Error == AppError.ERROR_SUCCESS)
            {
                WriteAuthCookie(username, response.Data);

                AdvancedResult<AdUser> aduser = client.GetUserInfo(response.Data);

                loginUser = aduser.Data;

                //绑定第三方账号
                //object su = Session[sKey_SocialUserKey];
                //SocialUser socialUser = su != null ? su as SocialUser : null;
                SocialUser socialUser = SecurityHelper.DecryptObject<SocialUser>(CookieHelper.Get(sKey_SocialUserKey), null);
                if (socialUser != null)
                {
                    AdvancedResult<string> result = client.IsBindSocialUser(socialUser);
                    if (result.Error != AppError.ERROR_SUCCESS)
                    {
                        socialUser.UserId = aduser.Data.AdUserId;
                        AdvancedResult<string> bindresponse = client.BindSocialUserAfterLogin(response.Data, socialUser);
                        if (bindresponse.Error == AppError.ERROR_SUCCESS)
                        {
                            switch (socialUser.SocialUserType)
                            {
                                case SocialUserTypeEnum.QQ:
                                    try
                                    {
                                        OAuthToken oToken = new OAuthToken();
                                        oToken.AccessToken = socialUser.AccessToken;
                                        oToken.OpenId = socialUser.Uid;
                                        QOpenClient qclient = new QOpenClient(oToken);
                                        var postresult = qclient.AddTopic(Config.Instance.RegisteredTwitter, "2", SiteUrl);
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                case SocialUserTypeEnum.Sina:
                                    try
                                    {
                                        OAuthAccessToken stoken = new OAuthAccessToken();
                                        stoken.Token = socialUser.AccessToken;
                                        stoken.UserID = socialUser.Uid;
                                        UpdateStatusInfo statusInfo = new UpdateStatusInfo();
                                        statusInfo.Status = Config.Instance.RegisteredTwitter + SiteUrl;
                                        var postresult = AMicroblog.PostStatus(statusInfo, stoken);
                                    }
                                    catch
                                    {
                                    }
                                    break;
                            }
                            //绑定成功
                            response.ExMessage = "绑定成功!";
                            //Session[sKey_SocialUserKey] = null;
                            CookieHelper.Remove(sKey_SocialUserKey);
                        }
                        else
                        {
                            //绑定失败
                            response.ExMessage = "绑定失败!请确认该第三方账号未与本站已注册账户绑定！";
                        }
                    }
                    else
                    {
                        response.ExMessage = "绑定失败!该第三方账号已经于本站现有账号绑定！";
                    }
                }
            }
            client.Close();
            client = null;
            return response;
        }

        private void WriteAuthCookie(string username, string token)
        {
            DateTime issueDate = DateTime.Now;
            DateTime expiration = issueDate.AddDays(1);
            string userData = token;
            bool isPersistent = true;

            FormsAuthenticationTicket ticket;
            ticket = new FormsAuthenticationTicket(
                1, username, issueDate, expiration, isPersistent, userData);

            string value = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(username, isPersistent);
            cookie.Value = value;
            cookie.Expires = expiration;
            Response.Cookies.Set(cookie);

        }

        public JsonResult AjaxLogout()
        {
            UserServiceClient client = new UserServiceClient();
            client.Logout(CurrentToken);
            client.Close();
            client = null;
            FormsAuthentication.SignOut();
            return this.Json(null);
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            Response.Redirect(Url.Action("Index", "Home"));
        }

        #region 第三方登录

        //QQ微博登录后记录其oAuth信息(Session)Key
        private string sKey_QQoAuthKey = "oAuthKey";
        private string sKey_SocialUserKey = "socialUserKey";

        private void oAuth_Sina()
        {
            ISinaoAuth oAuthService = new SinaOAuthService();
            string requestTokenUrl = oAuthService.GetAuthorizeUri();
            Response.Redirect(requestTokenUrl, true);
        }

        private void oAuth_TQQ()
        {
            IQoAuth oAuthService = new QoAuthService();
            QoAuthKey oAuthKey = null;
            string requestTokenUrl = oAuthService.GetAuthorizationUrl(out oAuthKey);

            //用于获取access_token
            //Session[sKey_QQoAuthKey] = oAuthKey;
            CookieHelper.Set(sKey_QQoAuthKey, SecurityHelper.EncryptObject(oAuthKey), DateTime.Now.AddHours(1));
            Response.Redirect(requestTokenUrl, true);
        }

        /// <summary>
        /// 新浪微博账号登陆
        /// </summary>
        public void Sina_Login()
        {
            oAuth_Sina();
        }

        /// <summary>
        ///腾讯微博账号登陆 
        /// </summary>
        public void QQ_Login()
        {
            oAuth_TQQ();
        }

        /// <summary>
        /// 绑定社交账号后的回调函数，使用场景
        /// 1.直接采用社交账号登陆
        /// 2.平台账号登陆后->绑定新的社交账号
        /// </summary>
        public ActionResult Social_Callback(string type)
        {
            SocialState ssResponse = new SocialState();
            try
            {
                //判断登陆的社交账户类型(默认为新浪微博账号)
                String req_type = (String.IsNullOrEmpty(type)) ? "1" : type;
                SocialUserTypeEnum socialUserTypeEnum = (SocialUserTypeEnum)Enum.Parse(typeof(SocialUserTypeEnum), req_type);

                //初始化社交账户结构体
                SocialUser socialUser = new SocialUser();
                socialUser.SocialUserType = socialUserTypeEnum;

                #region 获取社交账号的access_token及security_key
                switch (socialUserTypeEnum)
                {
                    case SocialUserTypeEnum.Sina:
                        ISinaoAuth sina_oAuthService = new SinaOAuthService();
                        string _code = Request["code"];
                        string access_code = string.Empty;
                        string sina_uid = string.Empty;
                        bool _isOk = sina_oAuthService.GetAccessToken(_code, out access_code, out sina_uid);
                        socialUser.AccessToken = access_code;
                        socialUser.Uid = sina_uid;

                        ssResponse.SocialType = "新浪微博";

                        break;
                    case SocialUserTypeEnum.QQ:
                        IQoAuth oAuthService = new QoAuthService();
                        string verifierCode = Request["code"];
                        //QoAuthKey oAuthKey = (QoAuthKey)Session[sKey_QQoAuthKey];
                        QoAuthKey oAuthKey = SecurityHelper.DecryptObject<QoAuthKey>(CookieHelper.Get(sKey_QQoAuthKey), null);
                        oAuthService.GetAccessToken(verifierCode, ref oAuthKey);

                        socialUser.AccessToken = oAuthKey.Access_Token;
                        socialUser.AccessTokenSecret = oAuthKey.State;
                        socialUser.Uid = oAuthKey.OpenId;

                        ssResponse.SocialType = "QQ";
                        break;
                    default:
                        break;
                }
                #endregion

                //Session[sKey_SocialUserKey] = socialUser;
                CookieHelper.Set(sKey_SocialUserKey, SecurityHelper.EncryptObject(socialUser), DateTime.Now.AddHours(1));
                //用户业务逻辑服务
                #region 社交账号登陆

                //社交账号登陆
                if (socialUserTypeEnum == SocialUserTypeEnum.QQ || socialUserTypeEnum == SocialUserTypeEnum.Sina)
                {
                    //检查社交账号是否和平台账号绑定
                    UserServiceClient client = new UserServiceClient();
                    AdvancedResult<string> result = client.IsBindSocialUser(socialUser);

                    ssResponse.Logined = true;

                    if (result.Error == AppError.ERROR_SUCCESS)
                    {
                        //跳转到个人中心
                        AdvancedResult<AdUser> aduser = client.GetUserInfo(result.Data);

                        ssResponse.Binded = true;

                        if (aduser.Error == AppError.ERROR_SUCCESS)
                        {
                            WriteAuthCookie(aduser.Data.UserAccount, result.Data);
                            Response.AddHeader("P3P", "CP=CAO PSA OUR");
                            ssResponse.Name = aduser.Data.UserAccount;
                        }
                        CookieHelper.Remove(sKey_SocialUserKey);
                    }
                    else
                    {
                        //提示绑定
                        ssResponse.Binded = false;
                    }
                    client.Close();
                    client = null;
                }
                else
                {
                    ssResponse.Logined = false;
                }


                #endregion
            }
            catch(Exception ex)
            {
                ssResponse.Logined = false;
                ssResponse.Message = ex.Message;
            }
            
            ViewBag.LoginResponse = JsonHelper.Serialize(ssResponse);
            return View("~/Views/Account/socialcallback.cshtml");
        }

        #endregion

        [Serializable]
        private class SocialState
        {
            public bool Logined { get; set; }

            public bool Binded { get; set; }

            public string Name { get; set; }

            public string SocialType { get; set; }

            public string Message { get; set; }
        }

    }
}
