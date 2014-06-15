using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabyPlan.oAuth.Common
{
    /// <summary>
    /// 参数类
    /// </summary>
   public class OAuthParameter
    {

       public string ParameterName { get; set; }
       
       public string ParameterValue { get; set; }
       
       public OAuthParameter(string parameterName, string paraemterVaule)
       {
           this.ParameterName = parameterName;
           this.ParameterValue = paraemterVaule;
       }
    }
}
