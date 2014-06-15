/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:17:46
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
    public class GenReply
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Rid
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ObjId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ObjType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Content
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
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 RefUserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 RefReplyId
        { get; set; }

        /// <summary>
        /// 创建回复的用户
        /// </summary>
        public string UserAccount
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public GenReply BuildSampleEntity(IDataReader reader)
        {
            this.Rid = DBConvert.ToInt32(reader["rid"]);
            this.ObjId = DBConvert.ToInt32(reader["obj_id"]);
            this.ObjType = DBConvert.ToInt32(reader["obj_type"]);
            this.Content = DBConvert.ToString(reader["content"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            this.State = DBConvert.ToInt32(reader["state"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.RefUserId = DBConvert.ToInt32(reader["ref_user_id"]);
            this.RefReplyId = DBConvert.ToInt32(reader["ref_reply_id"]);
            return this;
        }
        public GenReply BuildAllEntity(IDataReader reader)
        {
            this.Rid = DBConvert.ToInt32(reader["rid"]);
            this.ObjId = DBConvert.ToInt32(reader["obj_id"]);
            this.ObjType = DBConvert.ToInt32(reader["obj_type"]);
            this.Content = DBConvert.ToString(reader["content"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            this.State = DBConvert.ToInt32(reader["state"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.RefUserId = DBConvert.ToInt32(reader["ref_user_id"]);
            this.RefReplyId = DBConvert.ToInt32(reader["ref_reply_id"]);
            this.UserAccount = DBConvert.ToString(reader["user_account"]);
            return this;
        }
    }
}