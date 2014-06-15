using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BabyPlan.Common;
using BabyPlan.DataAccess;
using BabyPlan.Meta;
using System.ServiceModel.Activation;
using BabyPlan.DataStructure;
using BabyPlan.Cache;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    public class UserServiceClient : IUserService
    {
        public void Close()
        { }
        #region IUserService 成员
        public AdvancedResult<string> Register(string account, string pwd)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdvancedResult<bool> dr = CheckAccout(account);
                if (dr.Data)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    return result;
                }

                AdUser user = new AdUser();
                user.UserAccount = account;
                user.Pwd = pwd;
                int i = UserAccessor.Instance.Insert(user);

                if (i > 0)
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(i.ToString());
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public AdvancedResult<bool> CheckAccout(string account)
        {
            AdvancedResult<bool> result = new AdvancedResult<bool>();
            AdUser user = null;
            user = UserAccessor.Instance.Get(0, account.Trim(), string.Empty, StateType.Ignore);

            try
            {
                if (user != null)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    result.Data = true;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public AdvancedResult<bool> CheckLogin(string token)
        {
            AdvancedResult<bool> result = new AdvancedResult<bool>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Data = true;
                    result.Error = AppError.ERROR_SUCCESS;
                }
                else
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public AdvancedResult<string> Login(string account, string pwd)
        {


            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdUser user = null;
                user = UserAccessor.Instance.Get(0, account.Trim(), pwd.Trim(), StateType.Active);
                if (user != null)
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(user.AdUserId.ToString());
                }
                else
                {
                    result.Error = AppError.ERROR_LOGIN_FAILED;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public RespResult Logout(string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    CacheManagerFactory.GetMemoryManager().Remove(token);
                }

                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public RespResult UpdateBabyAge(int bbage, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        AdUser user = UserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                        user.BabyBirthday = DateTime.Now.AddYears(0 - bbage);
                        UserAccessor.Instance.Update(user);
                        result.Error = AppError.ERROR_SUCCESS;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public AdvancedResult<AdUser> GetUserInfoByID(int userid)
        {
            AdvancedResult<AdUser> result = new AdvancedResult<AdUser>();
            try
            {
                if (userid > 0)
                {
                    AdUser user = UserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = user;
                }
                else
                {
                    result.Error = AppError.ERROR_FAILED;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        public AdvancedResult<AdUser> GetUserInfoByAccount(string account)
        {
            AdvancedResult<AdUser> result = new AdvancedResult<AdUser>();
            try
            {
                if (!string.IsNullOrEmpty(account))
                {
                    AdUser user = UserAccessor.Instance.Get(0, account, string.Empty, StateType.Ignore);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = user;
                }
                else
                {
                    result.Error = AppError.ERROR_FAILED;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }  

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AdvancedResult<AdUser> GetUserInfo(string token)
        {
            AdvancedResult<AdUser> result = new AdvancedResult<AdUser>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        AdUser user = UserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = user;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 判断是否绑定了社交账户，如果有绑定则自动登录，并返回token
        /// </summary>
        /// <param name="socialUser"></param>
        /// <returns></returns>
        public AdvancedResult<string> IsBindSocialUser(SocialUser socialUser)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                SocialUser suer = SocialUserAccessor.Instance.Get(socialUser.Uid, socialUser.SocialUserType);
                if (suer != null)
                {
                    //刷新access_token
                    suer.AccessToken = socialUser.AccessToken;
                    suer.AccessTokenSecret = socialUser.AccessTokenSecret;
                    SocialUserAccessor.Instance.Update(suer);

                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(suer.UserId.ToString());
                }
                else
                {
                    result.Error = AppError.ERROR_FAILED;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 登录后绑定
        /// </summary>
        /// <param name="token"></param>
        /// <param name="socialUser"></param>
        /// <returns></returns>
        public AdvancedResult<string> BindSocialUserAfterLogin(string token, SocialUser socialUser)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    SocialUserAccessor.Instance.Insert(socialUser);
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user">修改用户名，性别，密码，地址，qq，手机</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public RespResult EditeUserInfo(AdUser user, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        AdUser olduser = UserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                        if (olduser.UserAccount != user.UserAccount)
                        {
                            AdvancedResult<bool> dr = CheckAccout(user.UserAccount);
                            if (dr.Data)
                            {
                                result.Error = AppError.ERROR_PERSON_FOUND;
                                return result;
                            }
                        }
                        olduser.UserAccount = user.UserAccount;
                        olduser.Sex = user.Sex;
                        olduser.Pwd = user.Pwd;
                        olduser.Province = user.Province;
                        olduser.City = user.City;
                        olduser.County = user.County;
                        olduser.Street = user.Street;
                        olduser.Mobile = user.Mobile;
                        olduser.Qq = user.Qq;

                        UserAccessor.Instance.Update(olduser);
                        result.Error = AppError.ERROR_SUCCESS;

                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public AdvancedResult<List<AdUser>> GetUserAllInfo()
        {
            AdvancedResult<List<AdUser>> result = new AdvancedResult<List<AdUser>>();
            try
            {
                List<AdUser> users = UserAccessor.Instance.Search();
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = users;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }     

        #endregion
    }
}
