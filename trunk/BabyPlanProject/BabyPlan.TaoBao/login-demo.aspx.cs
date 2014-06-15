using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace BabyPlan.TaoBao
{
    public partial class login_demo : System.Web.UI.Page
    {
        public Configure config = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
            config = Configure.LoadConfig(Server.MapPath("configure.xml"));
            String appkey = config.AppKey;//"您的appkey";填写appkey
            String secret = config.AppSecret;//"appkey的secret";填写应用的secret
            //取到当前时间的离1970的毫秒数，下面代码中的转换主要是与服务器端的时间一致
            long timestamp = Decimal.ToInt64(Decimal.Divide(DateTime.Now.Ticks - new DateTime(1970, 1, 1, 8, 0, 0).Ticks, 10000));
            Console.WriteLine("timestamp" + timestamp);
            //拼装消息
            String message = secret + "app_key" + appkey + "timestamp" + timestamp + secret;
            //签名消息
            String sign = signByHmacMd5(message, secret);
            //添加时间戳到cookie
            addCookie("timestamp", timestamp.ToString());
            //添加签名到cookie
            addCookie("sign", sign);
        }

        ///添加数据到cookie中,name=value
        protected void addCookie(String name, String value)
        {
            HttpCookie cookie = new HttpCookie(name, value);

            Response.Cookies.Add(cookie);
        }

        ///对message用secret进行hmac-md5签名
        ///返回签名值
        protected String signByHmacMd5(String message, String secret)
        {

            HMACMD5 hmacMD5 = new HMACMD5(Encoding.UTF8.GetBytes(secret));

            // Convert the input string to a byte array and compute the hashamc.
            byte[] data = hmacMD5.ComputeHash(Encoding.UTF8.GetBytes(message));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}