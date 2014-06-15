using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.DataStructure;
using System.Reflection;
using System.ComponentModel;

namespace BabyPlan.mvcApp.Infras
{
    public class ResponseObj<T>
    {
        //错误码
        public AppError Error { get; set; }

        //错误信息
        public string RetMsg 
        {
            get { return GetEnumDescription(Error); }            
        }
        

        //返回的数据 
        public T RetData { get; set; }

        private string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();
            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return strValue;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else
                return "未知错误";            
        } 
       
    }
}