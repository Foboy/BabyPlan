/**
 * @author yangchao modify:zhengrunqiang 
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:21:53 modify_date:2013/2/2 22:26
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BabyPlan.Common;
using BabyPlan.DataStructure;

namespace BabyPlan.Meta
{
    [Serializable]
    public class ProProduct
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public Int32 Pid
        { get; set; }

        /// <summary>
        /// 发布商品标题
        /// </summary>
        public String Title
        { get; set; }

        /// <summary>
        /// 商品的描述
        /// </summary>
        public String BbInfo
        { get; set; }

        /// <summary>
        /// 1：启用 2：禁用
        /// </summary>
        public Int32 State
        { get; set; }

        /// <summary>
        /// 1：普通 2：VIP
        /// </summary>
        public Int32 Level
        { get; set; }

        /// <summary>
        /// 商品发布时间
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 商品发布人id
        /// </summary>
        public Int32 CreateId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ViewNum
        { get; set; }

        /// <summary>
        /// true 已清洗
        /// </summary>
        public bool IsWash
        { get; set; }

       

        /// <summary>
        /// 宝贝适用年龄下限
        /// </summary>
        public Int32 MinAge
        { get; set; }

        /// <summary>
        /// 宝贝适用年龄上限
        /// </summary>
        public Int32 MaxAge
        { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public Double Price
        { get; set; }
        /// <summary>
        /// 1：衣服 2：玩具 3：其他(小类细分)
        /// </summary>
        public ItemType ItemType
        { get; set; }
        /// <summary>
        /// 1:宝宝的 2:妈妈的(大类)
        /// </summary>
        public ItemSort ItemSort
        { get; set; }
        public SexType Sex
        { get; set; }
        //商品附带图片
        public List<ResPic> pics
        { get; set; }

        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ProProduct BuildSampleEntity(IDataReader reader)
        {
            this.Pid = DBConvert.ToInt32(reader["pid"]);
            this.Title = DBConvert.ToString(reader["title"]);
            this.BbInfo = DBConvert.ToString(reader["bb_info"]);
            this.State = DBConvert.ToInt32(reader["state"]);
            this.Level = DBConvert.ToInt32(reader["level"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            this.ViewNum = DBConvert.ToInt32(reader["view_num"]);
            this.IsWash = DBConvert.ToInt32(reader["iswash"]) == 1 ? true : false ;
            this.MinAge = DBConvert.ToInt32(reader["min_age"]);
            this.MaxAge = DBConvert.ToInt32(reader["max_age"]);
            this.Price = DBConvert.ToDouble(reader["price"]);
            this.ItemType = (ItemType)DBConvert.ToInt32(reader["item_type"]);
            this.ItemSort = (ItemSort)DBConvert.ToInt32(reader["item_sort"]);
            this.Sex = (SexType)DBConvert.ToInt32(reader["sex"]);
            return this;
        }
    }
}