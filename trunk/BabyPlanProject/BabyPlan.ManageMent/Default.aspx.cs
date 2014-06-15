using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BabyPlan.WcfService;
using BabyPlan.Common;
using BabyPlan.Meta;

namespace BabyPlan.ManageMent
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
      

        protected void Button1_Click(object sender, EventArgs e)
        {
            AdvancedResult<List<GenReply>> result;
            ReplyServiceClient client = new ReplyServiceClient();
            result = client.LoadAllReplyList();
            if (result.Data != null && result.Data.Count > 0)
            {
                var list = from o in result.Data
                           select new { o.UserAccount, o.Content, o.CreateTime, o.ObjId };
                
                GridView1.DataSource = list;
                GridView1.DataBind();
                
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[3].Text;
                string content = e.Row.Cells[1].Text;
                e.Row.Cells[1].Text = "<a target=\"_blank\" href=\"http://www.360baijiayi.com/二手宝贝/" + id + "\">" + content + "</a>";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            AdvancedResult<List<AdUser>> result;
            UserServiceClient client = new UserServiceClient();
            result = client.GetUserAllInfo();
            if (result.Data != null && result.Data.Count > 0)
            {
                var list = from o in result.Data
                           select new { o.UserAccount, o.Mobile, o.Qq, o.CreateTime };

                GridView1.DataSource = list;
                GridView1.DataBind();

            }
        }
    }
}
