using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BabyPlan.Common;
using BabyPlan.Meta;
using BabyPlan.DataStructure;
using BabyPlan.Cache;
using BabyPlan.DataAccess;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ReplyService”。
    public class ReplyServiceClient : IReplyService
    {
        public void Close()
        { }
        //获取与我相关的回复数量
        public AdvancedResult<int> GetMyBBReplyNum(string token)
        {
            AdvancedResult<int> result = new AdvancedResult<int>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    result.Data = GenReplyAccessor.Instance.GetBBReplyCount(0, ReplyType.BB, userid, StateType.Ignore, 0, 0);
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

        //获取单个宝贝的回复数量
        public AdvancedResult<int> GetBBPostReplyNum(int bbPostID)
        {
            AdvancedResult<int> result = new AdvancedResult<int>();
            try
            {
                result.Data = GenReplyAccessor.Instance.GetBBReplyCount(bbPostID, ReplyType.BB, 0, StateType.Ignore, 0,0);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        //获取与我相关的宝贝回复列表
        public AdvancedResult<PageEntity<GenReply>> LoadMyBBReplyList(string token, int pageInex, int pageSize)
        {
            AdvancedResult<PageEntity<GenReply>> result = new AdvancedResult<PageEntity<GenReply>>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    result.Data = GenReplyAccessor.Instance.Search(0, ReplyType.BB, userid, StateType.Active, 0, 0, pageInex, pageSize);
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
        //获取全部回复列表
        public AdvancedResult<List<GenReply>> LoadAllReplyList()
        {
            AdvancedResult<List<GenReply>> result = new AdvancedResult<List<GenReply>>();
            try
            {
                    result.Data = GenReplyAccessor.Instance.Search();
                    result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        //分页获取单个宝贝的回复列表
        public AdvancedResult<PageEntity<GenReply>> LoadReplyListByBBPostID(int bbPostID, int pageInex, int pageSize)
        {
            AdvancedResult<PageEntity<GenReply>> result = new AdvancedResult<PageEntity<GenReply>>();
            try
            {
                result.Data = GenReplyAccessor.Instance.Search(bbPostID, ReplyType.BB, 0, StateType.Active, 0, 0,pageInex, pageSize);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        //回复评论
        public RespResult ReplyBBPost(int bbPostID,int refReplyID, string Content, string token)
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
                    ProProduct product = ProProductAccessor.Instance.Get(bbPostID);
                    if (product != null)
                    {
                        GenReply reply = new GenReply();
                        reply.ObjId = bbPostID;
                        reply.ObjType = (int)ReplyType.BB;
                        reply.Content = Content;
                        reply.CreateId = userid;
                        reply.RefUserId = product.CreateId;
                        reply.RefReplyId = refReplyID;
                        GenReplyAccessor.Instance.Insert(reply);
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
        //获取回复评论
        public AdvancedResult<GenReply> Get(int rid)
        {
            AdvancedResult<GenReply> result = new AdvancedResult<GenReply>();
            try
            {
                result.Data = GenReplyAccessor.Instance.Get(rid);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        #region 心愿贴相关功能

        /// <summary>
        /// 发布心愿单
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public RespResult Bless(string content)
        {
            RespResult result = new RespResult();
            try
            {
                GenBless bless = new GenBless();
                bless.Content = content;
                bless.CreateTime = System.DateTime.Now;
                GenBlessAccessor.Instance.Insert(bless);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        //获取心愿贴
        /// <summary>
        /// 获取心愿贴
        /// </summary>
        /// <param name="blessID"></param>
        /// <returns></returns>
        public AdvancedResult<GenBless> GetBless(int blessID)
        {
            AdvancedResult<GenBless> result = new AdvancedResult<GenBless>();
            try
            {
                GenBless bless = new GenBless();
                bless = GenBlessAccessor.Instance.Get(blessID);
                result.Data = bless;
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        //分页获取单个心愿贴的回复列表
        public AdvancedResult<PageEntity<GenReply>> LoadReplyListByBlessID(int blessID, int pageInex, int pageSize)
        {
            AdvancedResult<PageEntity<GenReply>> result = new AdvancedResult<PageEntity<GenReply>>();
            try
            {
                result.Data = GenReplyAccessor.Instance.Search(blessID, ReplyType.Bless, 0, StateType.Active, 0, 0, pageInex, pageSize);
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 回复心愿贴
        /// </summary>
        /// <param name="blessID"></param>
        /// <param name="refReplyID"></param>
        /// <param name="Content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public RespResult ReplyBless(int blessID, int refReplyID, string Content, string token)
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
                    GenBless bless = GetBless(blessID).Data;
                    if (bless != null)
                    {
                        GenReply reply = new GenReply();
                        reply.ObjId = blessID;
                        reply.ObjType = (int)ReplyType.Bless;
                        reply.Content = Content;
                        reply.CreateId = userid;
                        reply.RefUserId = bless.CreateId;
                        reply.RefReplyId = refReplyID;
                        GenReplyAccessor.Instance.Insert(reply);
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
        #endregion 心愿贴相关功能

    }
}
