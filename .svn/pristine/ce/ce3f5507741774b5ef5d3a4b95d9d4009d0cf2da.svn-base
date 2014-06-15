/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:22:49
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using BabyPlan.Common;
using BabyPlan.Meta;
using BabyPlan.DataStructure;


namespace BabyPlan.DataAccess
{
    public class ProProductAccessor
    {
        private MySqlCommand cmdInsertProProduct;
        private MySqlCommand cmdDeleteProProduct;
        private MySqlCommand cmdUpdateProProduct;
        private MySqlCommand cmdLoadProProduct;
        private MySqlCommand cmdLoadAllProProduct;
        private MySqlCommand cmdGetProProductCountByUserId;
        private MySqlCommand cmdGetProProduct;
        private MySqlCommand cmdLoadAllProProductItem;

        //宝贝列表用
        private MySqlCommand cmdLoadBB;
        private MySqlCommand cmdGetBBCount;

        private ProProductAccessor()
        {
            #region cmdInsertProProduct
            //商品表pro_product插入商品数据
            cmdInsertProProduct = new MySqlCommand("INSERT INTO pro_product(pid,title,bb_info,state,level,create_time,create_id,view_num,iswash,sex,price,min_age,max_age,item_type,item_sort) values (@Pid,@Title,@BbInfo,@State,@Level,@CreateTime,@CreateId,@ViewNum,@IsWash,@Sex,@Price,@MinAge,@MaxAge,@ItemType,@ItemSort)");

            cmdInsertProProduct.Parameters.Add("@Pid", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@Title", MySqlDbType.String);
            cmdInsertProProduct.Parameters.Add("@BbInfo", MySqlDbType.String);
            cmdInsertProProduct.Parameters.Add("@State", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@Level", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdInsertProProduct.Parameters.Add("@CreateId", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@ViewNum", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@IsWash", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@MinAge", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@MaxAge", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdInsertProProduct.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@ItemType", MySqlDbType.Int32);
            cmdInsertProProduct.Parameters.Add("@ItemSort", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateProProduct
            //商品表pro_product某个商品数据更新
            cmdUpdateProProduct = new MySqlCommand(" update pro_product set title = @Title,bb_info = @BbInfo,state = @State,level = @Level,create_time = @CreateTime,create_id = @CreateId,view_num = @ViewNum,iswash = @IsWash,sex = @Sex,price = @Price,min_age = @MinAge,max_age = @MaxAge,item_type = @ItemType,item_sort=@ItemSort where pid = @Pid");
            cmdUpdateProProduct.Parameters.Add("@Pid", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@Title", MySqlDbType.String);
            cmdUpdateProProduct.Parameters.Add("@BbInfo", MySqlDbType.String);
            cmdUpdateProProduct.Parameters.Add("@State", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@Level", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdUpdateProProduct.Parameters.Add("@CreateId", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@ViewNum", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@IsWash", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@MinAge", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@MaxAge", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@Price", MySqlDbType.Decimal);
            cmdUpdateProProduct.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@ItemType", MySqlDbType.Int32);
            cmdUpdateProProduct.Parameters.Add("@ItemSort", MySqlDbType.Int32);

            #endregion

            #region cmdLoadProProduct
            //根据（商品状态/商品等级/商品发布人）分页查询商品信息
            cmdLoadProProduct = new MySqlCommand(@" select pid,title,bb_info,state,level,create_time,create_id,view_num,iswash,sex,price,min_age,max_age,item_type,item_sort from pro_product where  (@State = 0 or state = @State) and (@Level = 0 or level = @Level) and (@CreateId = 0 or create_id = @CreateId) order by create_time desc  limit @PageIndex,@PageSize");
          
            cmdLoadProProduct.Parameters.Add("@State", MySqlDbType.Int32);
            cmdLoadProProduct.Parameters.Add("@Level", MySqlDbType.Int32);
            cmdLoadProProduct.Parameters.Add("@CreateId", MySqlDbType.Int32);
            cmdLoadProProduct.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadProProduct.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetProProductCount
            //根据（商品状态/商品等级/商品发布人）分页查询商品数据总数
            cmdGetProProductCountByUserId = new MySqlCommand(" select count(*)  from pro_product where (@State = 0 or state = @State) and (@Level = 0 or level = @Level) and (@CreateId = 0 or create_id = @CreateId)   ");
            cmdGetProProductCountByUserId.Parameters.Add("@State", MySqlDbType.Int32);
            cmdGetProProductCountByUserId.Parameters.Add("@Level", MySqlDbType.Int32);
            cmdGetProProductCountByUserId.Parameters.Add("@CreateId", MySqlDbType.Int32);
            #endregion

            #region cmdLoadBB
            //根据（商品类型：item_type/商品分类:item_sort/商品适合人群性别:sex/价格区间）分页查询
            cmdLoadBB = new MySqlCommand(@"select 
    *
from
    pro_product pt
where  pt.state = 1 and (item_type = @bbType or @bbType = 0) and (item_sort = @bbSort or @bbSort = 0) and (sex = @Sex or @Sex = 0)  {0} {1}
order by pt.level desc,pt.create_time desc
limit @PageIndex,@PageSize");
            cmdLoadBB.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadBB.Parameters.Add("@pageSize", MySqlDbType.Int32);
            cmdLoadBB.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdLoadBB.Parameters.Add("@bbType", MySqlDbType.Int32);
            cmdLoadBB.Parameters.Add("@bbSort", MySqlDbType.Int32);

            #endregion

            #region cmdGetBBCount
            //根据（商品类型：item_type/商品分类:item_sort/商品适合人群性别:sex/价格区间）查询总数据条数
            cmdGetBBCount = new MySqlCommand(@" select 
    count(*)
from
    pro_product pt
where  pt.state = 1 and (item_type = @bbType or @bbType = 0) and (item_sort = @bbSort or @bbSort = 0) and (sex = @Sex or @Sex = 0)  {0} {1} ");
            cmdGetBBCount.Parameters.Add("@Sex", MySqlDbType.Int32);
            cmdGetBBCount.Parameters.Add("@bbType", MySqlDbType.Int32);
            cmdGetBBCount.Parameters.Add("@bbSort", MySqlDbType.Int32);
            #endregion

            #region cmdLoadAllProProduct
            //商品表pro_product所有数据
            cmdLoadAllProProduct = new MySqlCommand(" select pid,title,bb_info,state,level,create_time,create_id,view_num,iswash,sex,price,min_age,max_age,item_type,item_sort from pro_product");

            #endregion

            #region cmdGetProProduct
            //商品表pro_product某个商品数据
            cmdGetProProduct = new MySqlCommand(" select pid,title,bb_info,state,level,create_time,create_id,view_num,iswash,sex,price,min_age,max_age,item_type,item_sort from pro_product where pid = @Pid");
            cmdGetProProduct.Parameters.Add("@Pid", MySqlDbType.Int32);

            #endregion
            //查询单个发布商品相关上传图片
            #region cmdLoadAllProProductItem
            cmdLoadAllProProductItem = new MySqlCommand(@"select 
                                                        pro_item_id,
                                                        item_info,
                                                        pro_id,
                                                        create_time,
                                                        pi.state,
                                                        level,
                                                        p.pic_url,
                                                p.pic_height,
                                                p.pic_width,
                                                p.pic_id
                                                    from
                                                        pro_product_item pi
                                                            left join
                                                       /*  (select pp.* from res_pic pp where pp.obj_type = 1) */ (select pp.* from res_pic pp where pp.obj_type=0) p ON pi.pro_item_id = p.obj_id
                                                    where pro_id = @pid");
            cmdLoadAllProProductItem.Parameters.Add("@pid", MySqlDbType.Int32);
            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public int Insert(ProProduct e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertProProduct = cmdInsertProProduct.Clone() as MySqlCommand;
            int returnValue = 0;
            _cmdInsertProProduct.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertProProduct.Parameters["@Pid"].Value = e.Pid;
                _cmdInsertProProduct.Parameters["@Title"].Value = e.Title;
                _cmdInsertProProduct.Parameters["@BbInfo"].Value = e.BbInfo;
                _cmdInsertProProduct.Parameters["@State"].Value = e.State == 0 ? 1 : e.State;
                _cmdInsertProProduct.Parameters["@Level"].Value = e.Level;
                _cmdInsertProProduct.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdInsertProProduct.Parameters["@CreateId"].Value = e.CreateId;
                _cmdInsertProProduct.Parameters["@ViewNum"].Value = e.ViewNum;
                _cmdInsertProProduct.Parameters["@IsWash"].Value = e.IsWash ? 1 : 2;
                _cmdInsertProProduct.Parameters["@MinAge"].Value = e.MinAge;
                _cmdInsertProProduct.Parameters["@MaxAge"].Value = e.MaxAge;
                _cmdInsertProProduct.Parameters["@Price"].Value = e.Price;
                _cmdInsertProProduct.Parameters["@ItemType"].Value = (int)e.ItemType;
                _cmdInsertProProduct.Parameters["@ItemSort"].Value = (int)e.ItemSort;
                _cmdInsertProProduct.Parameters["@Sex"].Value = (int)e.Sex;

                _cmdInsertProProduct.ExecuteNonQuery();



                returnValue = Convert.ToInt32(_cmdInsertProProduct.LastInsertedId);
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertProProduct.Dispose();
                _cmdInsertProProduct = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(ProProduct e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateProProduct = cmdUpdateProProduct.Clone() as MySqlCommand;
            _cmdUpdateProProduct.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateProProduct.Parameters["@Pid"].Value = e.Pid;
                _cmdUpdateProProduct.Parameters["@Title"].Value = e.Title;
                _cmdUpdateProProduct.Parameters["@BbInfo"].Value = e.BbInfo;
                _cmdUpdateProProduct.Parameters["@State"].Value = e.State == 0 ? 1 : e.State;
                _cmdUpdateProProduct.Parameters["@Level"].Value = e.Level;
                _cmdUpdateProProduct.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateProProduct.Parameters["@CreateId"].Value = e.CreateId;
                _cmdUpdateProProduct.Parameters["@ViewNum"].Value = e.ViewNum;
                _cmdUpdateProProduct.Parameters["@IsWash"].Value = e.IsWash ? 1 : 2;
                _cmdUpdateProProduct.Parameters["@MinAge"].Value = e.MinAge;
                _cmdUpdateProProduct.Parameters["@MaxAge"].Value = e.MaxAge;
                _cmdUpdateProProduct.Parameters["@Price"].Value = e.Price;
                _cmdUpdateProProduct.Parameters["@ItemType"].Value = (int)e.ItemType;
                _cmdUpdateProProduct.Parameters["@ItemSort"].Value = (int)e.ItemSort;
                _cmdUpdateProProduct.Parameters["@Sex"].Value = (int)e.Sex;

                _cmdUpdateProProduct.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateProProduct.Dispose();
                _cmdUpdateProProduct = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 查询属于用户的宝贝列表
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<ProProduct> Search(StateType State, LevelType Level, Int32 CreateId, int pageIndex, int pageSize)
        {
            PageEntity<ProProduct> returnValue = new PageEntity<ProProduct>();
            List<ProProduct> prolist = new List<ProProduct>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadProProduct = cmdLoadProProduct.Clone() as MySqlCommand;
            MySqlCommand _cmdGetProProductCountByUserId = cmdGetProProductCountByUserId.Clone() as MySqlCommand;
          //  MySqlCommand _cmdLoadAllProProductItem = cmdLoadAllProProductItem.Clone() as MySqlCommand;
            
         //   _cmdLoadAllProProductItem.Connection = oc; 
            _cmdLoadProProduct.Connection = oc;
            _cmdGetProProductCountByUserId.Connection = oc;

            try
            {
                _cmdLoadProProduct.Parameters["@PageIndex"].Value = pageIndex * pageSize;
                _cmdLoadProProduct.Parameters["@PageSize"].Value = (pageIndex + 1) * pageSize;
                _cmdLoadProProduct.Parameters["@State"].Value = (int)State;
                _cmdLoadProProduct.Parameters["@Level"].Value = (int)Level;
                _cmdLoadProProduct.Parameters["@CreateId"].Value = CreateId;

                _cmdGetProProductCountByUserId.Parameters["@State"].Value = (int)State;
                _cmdGetProProductCountByUserId.Parameters["@Level"].Value = (int)Level;
                _cmdGetProProductCountByUserId.Parameters["@CreateId"].Value = CreateId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadProProduct.ExecuteReader();
                while (reader.Read())
                {
                    prolist.Add(new ProProduct().BuildSampleEntity(reader));
                }
                reader.Close();
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                for (int i = 0; i < prolist.Count; i++)
                {
                    List<ResPic> returnValue_item = new List<ResPic>();
                   returnValue_item =  ResPicAccessor.Instance.Search(prolist[i].Pid, PicType.BBPicture);
                    prolist[i].pics = returnValue_item;
                }
                returnValue.Items = prolist;
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                returnValue.PageIndex = pageIndex;
                returnValue.PageSize = pageSize;
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetProProductCountByUserId.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadProProduct.Dispose();
                _cmdLoadProProduct = null;
                _cmdGetProProductCountByUserId.Dispose();
                _cmdGetProProductCountByUserId = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 根据年龄，宝贝类型(小类)，宝贝分类（大类),性别查询宝贝列表
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<ProProduct> Search(Int32 MaxAge, ItemType bbType,ItemSort bbSort,PriceRange bbRange, SexType sex, int pageIndex, int pageSize)
        {
            PageEntity<ProProduct> returnValue = new PageEntity<ProProduct>();
            List<ProProduct> bblist = new List<ProProduct>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadBB = cmdLoadBB.Clone() as MySqlCommand;
            MySqlCommand _cmdGetBBCount = cmdGetBBCount.Clone() as MySqlCommand;
            _cmdLoadBB.Connection = oc;
            _cmdGetBBCount.Connection = oc;

            try
            {
                _cmdLoadBB.Parameters["@PageIndex"].Value = pageIndex * pageSize;
                _cmdLoadBB.Parameters["@PageSize"].Value = (pageIndex + 1) * pageSize; 
                _cmdLoadBB.Parameters["@bbType"].Value = (int)bbType;
                _cmdLoadBB.Parameters["@bbSort"].Value=(int)bbSort;
                _cmdLoadBB.Parameters["@Sex"].Value = (int)sex;
                string PriceRangeSql = string.Empty;
                switch (bbRange)
                {
                    case PriceRange.Ignore:
                        PriceRangeSql = " ";
                        break;
                    case PriceRange.TenZone:
                        PriceRangeSql = "and price>=0 and price<=10 ";
                        break;
                    case PriceRange.HundredZone:
                        PriceRangeSql = "and price>=10 and price<=100";
                        break;
                    default:
                        break;
                }
                if (MaxAge > 0)
                {
                    _cmdLoadBB.CommandText = string.Format(_cmdLoadBB.CommandText, " and " + MaxAge + " >= min_age and " + MaxAge + "<= max_age ",PriceRangeSql);
                }
                else
                {
                    _cmdLoadBB.CommandText = string.Format(_cmdLoadBB.CommandText, " ", PriceRangeSql);
                }
                _cmdGetBBCount.Parameters["@bbType"].Value = (int)bbType;
                _cmdGetBBCount.Parameters["@bbSort"].Value = (int)bbSort;
                _cmdGetBBCount.Parameters["@Sex"].Value = (int)sex;
                if (MaxAge > 0)
                {
                    _cmdGetBBCount.CommandText = string.Format(_cmdGetBBCount.CommandText, " and " + MaxAge + " >= min_age and " + MaxAge + "<= max_age ", PriceRangeSql);
                }
                else
                {
                    _cmdGetBBCount.CommandText = string.Format(_cmdGetBBCount.CommandText, " ", PriceRangeSql);
                }

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadBB.ExecuteReader();
                while (reader.Read())
                {
                    bblist.Add(new ProProduct().BuildSampleEntity(reader));
                }
                reader.Close();

                for (int i = 0; i < bblist.Count; i++)
                {
                    List<ResPic> returnValue_item = new List<ResPic>();
                    returnValue_item = ResPicAccessor.Instance.Search(bblist[i].Pid, PicType.BBPicture);
                    bblist[i].pics = returnValue_item;
                }

                returnValue.Items = bblist;
                returnValue.PageIndex = pageIndex;
                returnValue.PageSize = pageSize;
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetBBCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadBB.Dispose();
                _cmdLoadBB = null;
                _cmdGetBBCount.Dispose();
                _cmdGetBBCount = null;
                GC.Collect();
            }
            return returnValue;

        }


        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<ProProduct> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllProProduct = cmdLoadAllProProduct.Clone() as MySqlCommand;
            _cmdLoadAllProProduct.Connection = oc; List<ProProduct> returnValue = new List<ProProduct>();
            try
            {
                _cmdLoadAllProProduct.CommandText = string.Format(_cmdLoadAllProProduct.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllProProduct.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new ProProduct().BuildSampleEntity(reader));
                }

                for (int i = 0; i < returnValue.Count; i++)
                {
                    List<ResPic> returnValue_item = new List<ResPic>();
                    returnValue_item = ResPicAccessor.Instance.Search(returnValue[i].Pid, PicType.BBPicture);
                    returnValue[i].pics = returnValue_item;
                }

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllProProduct.Dispose();
                _cmdLoadAllProProduct = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public ProProduct Get(int bbPostID)
        {
            ProProduct returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetProProduct = cmdGetProProduct.Clone() as MySqlCommand;

            _cmdGetProProduct.Connection = oc;
            try
            {
                _cmdGetProProduct.Parameters["@Pid"].Value = bbPostID;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetProProduct.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new ProProduct().BuildSampleEntity(reader);
                }

                List<ResPic> returnValue_item = new List<ResPic>();
                returnValue_item = ResPicAccessor.Instance.Search(bbPostID, PicType.BBPicture);
                returnValue.pics = returnValue_item;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetProProduct.Dispose();
                _cmdGetProProduct = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取获取用户发布帖子数
        /// <param name="userid"></param>
        /// </summary>
        public int GetProductCountByUserId(int userid)
        {
            int returnValue = 0;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetProProductCount = cmdGetProProductCountByUserId.Clone() as MySqlCommand;

            _cmdGetProProductCount.Connection = oc;
            try
            {
                _cmdGetProProductCount.Parameters["@CreateId"].Value = userid;
                _cmdGetProProductCount.Parameters["@State"].Value = 1;
                _cmdGetProProductCount.Parameters["@Level"].Value = 1;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

               returnValue =  Convert.ToInt32(_cmdGetProProductCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetProProductCount.Dispose();
                _cmdGetProProductCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        private static readonly ProProductAccessor instance = new ProProductAccessor();
        public static ProProductAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
