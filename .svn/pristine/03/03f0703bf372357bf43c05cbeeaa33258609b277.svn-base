/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/21 0:45:55
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BabyPlan.Common;

namespace BabyPlan.Meta
{
    [Serializable]
    public class GenBless
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Bid
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Content
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 CreateId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public GenBless BuildSampleEntity(IDataReader reader)
        {
            this.Bid = DBConvert.ToInt32(reader["bid"]);
            this.Content = DBConvert.ToString(reader["content"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            return this;
        }
    }
}