/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/21 0:54:49
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


namespace BabyPlan.DataAccess
{
    public class GenVoteUserAccessor
    {
        private MySqlCommand cmdInsertGenVoteUser;
        private MySqlCommand cmdDeleteGenVoteUser;
        private MySqlCommand cmdUpdateGenVoteUser;
        private MySqlCommand cmdLoadGenVoteUser;
        private MySqlCommand cmdLoadAllGenVoteUser;
        private MySqlCommand cmdGetGenVoteUserCount;
        private MySqlCommand cmdGetGenVoteUser;

        private GenVoteUserAccessor()
        {
            #region cmdInsertGenVoteUser

            cmdInsertGenVoteUser = new MySqlCommand("INSERT INTO gen_vote_user(vid,user_id,create_time,v_ip,id) values (@Vid,@UserId,@CreateTime,@VIp,@Id)");

            cmdInsertGenVoteUser.Parameters.Add("@Vid", MySqlDbType.Int32);
            cmdInsertGenVoteUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdInsertGenVoteUser.Parameters.Add("@CreateTime", MySqlDbType.String);
            cmdInsertGenVoteUser.Parameters.Add("@VIp", MySqlDbType.String);
            cmdInsertGenVoteUser.Parameters.Add("@Id", MySqlDbType.Int32);
            #endregion

            #region cmdUpdateGenVoteUser

            cmdUpdateGenVoteUser = new MySqlCommand(" update gen_vote_user set vid = @Vid,user_id = @UserId,create_time = @CreateTime,v_ip = @VIp,id = @Id where id = @Id");
            cmdUpdateGenVoteUser.Parameters.Add("@Vid", MySqlDbType.Int32);
            cmdUpdateGenVoteUser.Parameters.Add("@UserId", MySqlDbType.Int32);
            cmdUpdateGenVoteUser.Parameters.Add("@CreateTime", MySqlDbType.String);
            cmdUpdateGenVoteUser.Parameters.Add("@VIp", MySqlDbType.String);
            cmdUpdateGenVoteUser.Parameters.Add("@Id", MySqlDbType.Int32);

            #endregion

            #region cmdLoadGenVoteUser

            cmdLoadGenVoteUser = new MySqlCommand(@" select vid,user_id,create_time,v_ip,id from gen_vote_user limit @PageIndex,@PageSize");
            cmdLoadGenVoteUser.Parameters.Add("@pageIndex", MySqlDbType.Int32);
            cmdLoadGenVoteUser.Parameters.Add("@pageSize", MySqlDbType.Int32);

            #endregion

            #region cmdGetGenVoteUserCount

            cmdGetGenVoteUserCount = new MySqlCommand(" select count(*)  from gen_vote_user ");

            #endregion

            #region cmdLoadAllGenVoteUser

            cmdLoadAllGenVoteUser = new MySqlCommand(" select vid,user_id,create_time,v_ip,id from gen_vote_user");

            #endregion

            #region cmdGetGenVoteUser

            cmdGetGenVoteUser = new MySqlCommand(" select vid,user_id,create_time,v_ip,id from gen_vote_user where id = @Id");
            cmdGetGenVoteUser.Parameters.Add("@Id", MySqlDbType.Int32);

            #endregion
        }

        /// <summary>
        /// 添加数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Insert(GenVoteUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdInsertGenVoteUser = cmdInsertGenVoteUser.Clone() as MySqlCommand;
            bool returnValue = false;
            _cmdInsertGenVoteUser.Connection = oc;
            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();
                _cmdInsertGenVoteUser.Parameters["@Vid"].Value = e.Vid;
                _cmdInsertGenVoteUser.Parameters["@UserId"].Value = e.UserId;
                _cmdInsertGenVoteUser.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdInsertGenVoteUser.Parameters["@VIp"].Value = e.VIp;
                _cmdInsertGenVoteUser.Parameters["@Id"].Value = e.Id;

                _cmdInsertGenVoteUser.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdInsertGenVoteUser.Dispose();
                _cmdInsertGenVoteUser = null;
            }
        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(GenVoteUser e)
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdUpdateGenVoteUser = cmdUpdateGenVoteUser.Clone() as MySqlCommand;
            _cmdUpdateGenVoteUser.Connection = oc;

            try
            {
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                _cmdUpdateGenVoteUser.Parameters["@Vid"].Value = e.Vid;
                _cmdUpdateGenVoteUser.Parameters["@UserId"].Value = e.UserId;
                _cmdUpdateGenVoteUser.Parameters["@CreateTime"].Value = e.CreateTime;
                _cmdUpdateGenVoteUser.Parameters["@VIp"].Value = e.VIp;
                _cmdUpdateGenVoteUser.Parameters["@Id"].Value = e.Id;

                _cmdUpdateGenVoteUser.ExecuteNonQuery();

            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdUpdateGenVoteUser.Dispose();
                _cmdUpdateGenVoteUser = null;
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
        public PageEntity<GenVoteUser> Search(Int32 Vid, Int32 UserId, String CreateTime, String VIp, Int32 Id, int pageIndex, int pageSize)
        {
            PageEntity<GenVoteUser> returnValue = new PageEntity<GenVoteUser>();
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadGenVoteUser = cmdLoadGenVoteUser.Clone() as MySqlCommand;
            MySqlCommand _cmdGetGenVoteUserCount = cmdGetGenVoteUserCount.Clone() as MySqlCommand;
            _cmdLoadGenVoteUser.Connection = oc;
            _cmdGetGenVoteUserCount.Connection = oc;

            try
            {
                _cmdLoadGenVoteUser.Parameters["@PageIndex"].Value = pageIndex;
                _cmdLoadGenVoteUser.Parameters["@PageSize"].Value = pageSize;
                _cmdLoadGenVoteUser.Parameters["@Vid"].Value = Vid;
                _cmdLoadGenVoteUser.Parameters["@UserId"].Value = UserId;
                _cmdLoadGenVoteUser.Parameters["@CreateTime"].Value = CreateTime;
                _cmdLoadGenVoteUser.Parameters["@VIp"].Value = VIp;
                _cmdLoadGenVoteUser.Parameters["@Id"].Value = Id;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadGenVoteUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Items.Add(new GenVoteUser().BuildSampleEntity(reader));
                }
                returnValue.RecordsCount = Convert.ToInt32(_cmdGetGenVoteUserCount.ExecuteScalar());
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadGenVoteUser.Dispose();
                _cmdLoadGenVoteUser = null;
                _cmdGetGenVoteUserCount.Dispose();
                _cmdGetGenVoteUserCount = null;
                GC.Collect();
            }
            return returnValue;

        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<GenVoteUser> Search()
        {
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdLoadAllGenVoteUser = cmdLoadAllGenVoteUser.Clone() as MySqlCommand;
            _cmdLoadAllGenVoteUser.Connection = oc; List<GenVoteUser> returnValue = new List<GenVoteUser>();
            try
            {
                _cmdLoadAllGenVoteUser.CommandText = string.Format(_cmdLoadAllGenVoteUser.CommandText, string.Empty);
                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdLoadAllGenVoteUser.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new GenVoteUser().BuildSampleEntity(reader));
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdLoadAllGenVoteUser.Dispose();
                _cmdLoadAllGenVoteUser = null;
                GC.Collect();
            }
            return returnValue;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public GenVoteUser Get(int Id)
        {
            GenVoteUser returnValue = null;
            MySqlConnection oc = ConnectManager.Create();
            MySqlCommand _cmdGetGenVoteUser = cmdGetGenVoteUser.Clone() as MySqlCommand;

            _cmdGetGenVoteUser.Connection = oc;
            try
            {
                _cmdGetGenVoteUser.Parameters["@Id"].Value = Id;

                if (oc.State == ConnectionState.Closed)
                    oc.Open();

                MySqlDataReader reader = _cmdGetGenVoteUser.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    returnValue = new GenVoteUser().BuildSampleEntity(reader);
                }
            }
            finally
            {
                oc.Close();
                oc.Dispose();
                oc = null;
                _cmdGetGenVoteUser.Dispose();
                _cmdGetGenVoteUser = null;
                GC.Collect();
            }
            return returnValue;

        }
        private static readonly GenVoteUserAccessor instance = new GenVoteUserAccessor();
        public static GenVoteUserAccessor Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
