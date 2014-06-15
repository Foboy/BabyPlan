using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IVoteService”。
    [ServiceContract]
    public interface IVoteService
    {
        [OperationContract]
        string AddVoteResult(int votid, int min, int max, string result);
        [OperationContract]
        string AddVoteType(string title, string description);

        [OperationContract]
        string EditVoteResult(int votid, int min, int max, string result);


        [OperationContract]
        string GetCurrentVoteNum(string ip);
        [OperationContract]
        string GetVoteNum(int voteTypeID);
        [OperationContract]
        string LoadVoteList(int voteTypeID);
        [OperationContract]
        string LoadVoteTypeList();
   
        [OperationContract]
        string Vote(int votid, string ip);
    }
}
