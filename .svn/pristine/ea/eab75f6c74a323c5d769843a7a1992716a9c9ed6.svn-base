/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/21 0:58:11
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
    public class GenVoteUser
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Vid
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VIp
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Id
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public GenVoteUser BuildSampleEntity(IDataReader reader)
        {
            this.Vid = DBConvert.ToInt32(reader["vid"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.CreateTime = DBConvert.ToString(reader["create_time"]);
            this.VIp = DBConvert.ToString(reader["v_ip"]);
            this.Id = DBConvert.ToInt32(reader["id"]);
            return this;
        }
    }
}