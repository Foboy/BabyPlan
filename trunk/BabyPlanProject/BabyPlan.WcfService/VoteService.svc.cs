using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using BabyPlan.Common;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“VoteService”。
    public class VoteServiceClient : IVoteService
    {
        public void Close()
        { }
        //获取投票分类列表（辣妈，奶爸）
        public string LoadVoteTypeList()
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }

        //添加投票分类
        public string AddVoteType(string title, string description)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }



        //获取单个投票类型的用户投票数
        public string GetVoteNum(int voteTypeID)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }

        //添加投票结果类型
        public string AddVoteResult(int votid, int min, int max, string re)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }

        //编辑投票结果类型
        public string EditVoteResult(int votid, int min, int max, string re)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }



        //获取投票列表
        public string LoadVoteList(int voteTypeID)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }

        //投票，返回投票结果
        public string Vote(int votid, string ip)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }

        //获取用户投票现有投票数
        public string GetCurrentVoteNum(string ip)
        {
            RespResult result = new RespResult();
            
            
            return JsonHelper.Serialize(result);
        }
    }
}
