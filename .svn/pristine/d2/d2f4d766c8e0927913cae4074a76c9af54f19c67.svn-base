/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:19:17
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
using System.Collections;
using BabyPlan.Meta;


namespace BabyPlan.DataAccess
{
    public class GenVoteTypeAccessor
    {
        private MySqlCommand cmdInsertGenVoteType;
        private MySqlCommand cmdDeleteGenVoteType;
        private MySqlCommand cmdUpdateGenVoteType;
        private MySqlCommand cmdLoadGenVoteType;
        private MySqlCommand cmdLoadAllGenVoteType;
        private MySqlCommand cmdGetGenVoteTypeCount;
        private MySqlCommand cmdGetGenVoteType;

        private GenVoteTypeAccessor()
        {
            #region cmdInsertGenVoteType

            cmdInsertGenVoteType = new MySqlCommand("INSERT INTO gen_vote_type(vt_id,vt_title,vt_description,total_vote_num,img_url,create_time,create_id,state) values (@VtId,@VtTitle,@VtDescription,@TotalVoteNum,@ImgUrl,@CreateTime,@CreateId,@State)");

            cmdInsertGenVoteType.Parameters.Add("@VtId", MySqlDbType.Int32);
            cmdInsertGenVoteType.Parameters.Add("@VtTitle", MySqlDbType.String);
            cmdInsertGenVoteType.Parameters.Add("@VtDescription", MySqlDbType.String);
            cmdInsertGenVoteType.Parameters.Add("@TotalVoteNum", MySqlDbType.Int32);
            cmdInsertGenVoteType.Parameters.Add("@ImgUrl", MySqlDbType.String);
            cmdInsertGenVoteType.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdInsertGenVoteType.Parameters.Add("@CreateId", MySqlDbType.Int32);
            cmdInsertGenVoteType.Parameters.Add("@State", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateGenVoteType

            cmdUpdateGenVoteType = new MySqlCommand(" update gen_vote_type set vt_id = @VtId,vt_title = @VtTitle,vt_description = @VtDescription,total_vote_num = @TotalVoteNum,img_url = @ImgUrl,create_time = @CreateTime,create_id = @CreateId,state = @State where vt_id = @VtId");
            cmdUpdateGenVoteType.Parameters.Add("@VtId", MySqlDbType.Int32);
            cmdUpdateGenVoteType.Parameters.Add("@VtTitle", MySqlDbType.String);
            cmdUpdateGenVoteType.Parameters.Add("@VtDescription", MySqlDbType.String);
            cmdUpdateGenVoteType.Parameters.Add("@TotalVoteNum", MySqlDbType.Int32);
            cmdUpdateGenVoteType.Parameters.Add("@ImgUrl", MySqlDbType.String);
            cmdUpdateGenVoteType.Parameters.Add("@CreateTime", MySqlDbType.DateTime);
            cmdUpdateGenVoteType.Parameters.Add("@CreateId", MySqlDbType.Int32);
            cmdUpdateGenVoteType.Parameters.Add("@State", MySqlDbType.Int32);

            #endregion

            #region cmdLoadGenVoteType

            cmdLoadGenVoteType = new MySqlCommand(@" select vt_id,vt_title,vt_description,total_vote_num,img_url,create_time,create_id,state from gen_vote_type limit @PageIndex,@PageSize");
            cmdLoadGenVoteType.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadGenVoteType.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetGenVoteTypeCount

            cmdGetGenVoteTypeCount = new MySqlCommand(" select count(*)  from gen_vote_type ");

            #endregion

            #region cmdLoadAllGenVoteType

            cmdLoadAllGenVoteType = new MySqlCommand(" select vt_id,vt_title,vt_description,total_vote_num,img_url,create_time,create_id,state from gen_vote_type");

            #endregion

            #region cmdGetGenVoteType

            cmdGetGenVoteType = new MySqlCommand(" select vt_id,vt_title,vt_description,total_vote_num,img_url,create_time,create_id,state from gen_vote_type where vt_id = @VtId");
            cmdGetGenVoteType.Parameters.Add("@VtId", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(GenVoteType e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertGenVoteType = cmdInsertGenVoteType.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdInsertGenVoteType.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertGenVoteType.Parameters["@VtId"].Value = e.VtId;
                _cmdInsertGenVoteType.Parameters["@VtTitle"].Value = e.VtTitle;
                _cmdInsertGenVoteType.Parameters["@VtDescription"].Value = e.VtDescription;
                _cmdInsertGenVoteType.Parameters["@TotalVoteNum"].Value = e.TotalVoteNum;
                _cmdInsertGenVoteType.Parameters["@ImgUrl"].Value = e.ImgUrl;
                _cmdInsertGenVoteType.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdInsertGenVoteType.Parameters["@CreateId"].Value = e.CreateId;
                _cmdInsertGenVoteType.Parameters["@State"].Value = e.State;

                _cmdInsertGenVoteType.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertGenVoteType.Dispose();
                _cmdInsertGenVoteType = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(GenVoteType e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateGenVoteType = cmdUpdateGenVoteType.Clone() as MySqlCommand;
            _cmdUpdateGenVoteType.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateGenVoteType.Parameters["@VtId"].Value = e.VtId;
                _cmdUpdateGenVoteType.Parameters["@VtTitle"].Value = e.VtTitle;
                _cmdUpdateGenVoteType.Parameters["@VtDescription"].Value = e.VtDescription;
                _cmdUpdateGenVoteType.Parameters["@TotalVoteNum"].Value = e.TotalVoteNum;
                _cmdUpdateGenVoteType.Parameters["@ImgUrl"].Value = e.ImgUrl;
                _cmdUpdateGenVoteType.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateGenVoteType.Parameters["@CreateId"].Value = e.CreateId;
                _cmdUpdateGenVoteType.Parameters["@State"].Value = e.State;

                _cmdUpdateGenVoteType.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateGenVoteType.Dispose();
                _cmdUpdateGenVoteType = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<GenVoteType> Search(Int32 VtId, String VtTitle, String VtDescription, Int32 TotalVoteNum, String ImgUrl, DateTime CreateTime, Int32 CreateId, Int32 State, int pageIndex, int pageSize)
        {
            PageEntity<GenVoteType> returnValue = new PageEntity<GenVoteType>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadGenVoteType = cmdLoadGenVoteType.Clone() as MySqlCommand;
            MySqlCommand _cmdGetGenVoteTypeCount = cmdGetGenVoteTypeCount.Clone() as MySqlCommand;
            _cmdLoadGenVoteType.Connection = oc;
            _cmdGetGenVoteTypeCount.Connection = oc;

            try
            {
                _cmdLoadGenVoteType.Parameters["@PageIndex"].Value = pageIndex  * pageSize;
                _cmdLoadGenVoteType.Parameters["@PageSize"].Value = (pageIndex + 1) * pageSize; 
                _cmdLoadGenVoteType.Parameters["@VtId"].Value = VtId;
                _cmdLoadGenVoteType.Parameters["@VtTitle"].Value = VtTitle;
                _cmdLoadGenVoteType.Parameters["@VtDescription"].Value = VtDescription;
                _cmdLoadGenVoteType.Parameters["@TotalVoteNum"].Value = TotalVoteNum;
                _cmdLoadGenVoteType.Parameters["@ImgUrl"].Value = ImgUrl;
                _cmdLoadGenVoteType.Parameters["@CreateTime"].Value = CreateTime;
                _cmdLoadGenVoteType.Parameters["@CreateId"].Value = CreateId;
                _cmdLoadGenVoteType.Parameters["@State"].Value = State;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadGenVoteType.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new GenVoteType().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetGenVoteTypeCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadGenVoteType.Dispose();
                _cmdLoadGenVoteType = null;
                _cmdGetGenVoteTypeCount.Dispose();
                _cmdGetGenVoteTypeCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<GenVoteType> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllGenVoteType = cmdLoadAllGenVoteType.Clone() as MySqlCommand;
            _cmdLoadAllGenVoteType.Connection = oc; List<GenVoteType> returnValue = new List<GenVoteType>();
            try
            {
                _cmdLoadAllGenVoteType.CommandText = string.Format(_cmdLoadAllGenVoteType.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllGenVoteType.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new GenVoteType().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllGenVoteType.Dispose();
                _cmdLoadAllGenVoteType = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public GenVoteType Get(int VtId)
        {
            GenVoteType returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetGenVoteType = cmdGetGenVoteType.Clone() as MySqlCommand;

            _cmdGetGenVoteType.Connection = oc;
            try
            {
                _cmdGetGenVoteType.Parameters["@VtId"].Value = VtId;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetGenVoteType.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new GenVoteType().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetGenVoteType.Dispose();
                _cmdGetGenVoteType = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly GenVoteTypeAccessor instance = new GenVoteTypeAccessor();
        public static GenVoteTypeAccessor Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
