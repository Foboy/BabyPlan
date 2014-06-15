using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BabyPlan.DataAccess
{
    public class AccessorBase
    {
        public AccessorBase()
        {
            oc = Create();
        }

        protected MySqlConnection oc;

        protected MySqlConnection Create()
        {
            return ConnectManager.Create();
        }
    }
    public static class ConnectManager
    {

        public static MySqlConnection Create()
        {
            MySqlConnection oc = new MySqlConnection();
            //本地
            oc.ConnectionString = "Database='babyplan';Data Source='localhost';User Id='root';Password='111111';charset='utf8';pooling=true";
            //正式
            //oc.ConnectionString = "Database='babyplan';Data Source='localhost';User Id='babyplan';Password='000000';charset='utf8';pooling=true";
            return oc;
        }
    }
}
