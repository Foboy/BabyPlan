/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:19:42
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
    public class GenVoteType
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 VtId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VtTitle
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VtDescription
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 TotalVoteNum
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ImgUrl
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
        /// 
        /// </summary>
        public Int32 State
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public GenVoteType BuildSampleEntity(IDataReader reader)
        {
            this.VtId = DBConvert.ToInt32(reader["vt_id"]);
            this.VtTitle = DBConvert.ToString(reader["vt_title"]);
            this.VtDescription = DBConvert.ToString(reader["vt_description"]);
            this.TotalVoteNum = DBConvert.ToInt32(reader["total_vote_num"]);
            this.ImgUrl = DBConvert.ToString(reader["img_url"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            this.State = DBConvert.ToInt32(reader["state"]);
            return this;
        }
    }
}