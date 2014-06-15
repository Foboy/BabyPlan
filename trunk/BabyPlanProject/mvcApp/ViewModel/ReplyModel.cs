using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using BabyPlan.Common;
using BabyPlan.DataStructure;

namespace BabyPlan.mvcApp.ViewModel
{
    public class ReplyModel : BaseModel
    {
        public int Id { get; set; }
        public int Pid { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public ReplyModel RefReply { get; set; }

        public string CreateTimeString { get; set; }

        public string FriendlyCreateTime
        {
            get
            {
                return FriendlyDate(CreateTime, "yyyy-MM-dd");
            }
        }

        public ProductModel Product { get; set; }

        public UserModel Author { get; set; }

        public ReplyModel Bind(GenReply item,ViewModelBindOption bindOptions)
        {
            this.Id = item.Rid;
            this.Content = item.Content;
            this.CreateTime = item.CreateTime;
            this.CreateTimeString = item.CreateTime.ToString("HH:mm");

            if (ContainEnumType<ReplyBindType>(bindOptions.ReplayBindType, ReplyBindType.Author))
            {
                UserServiceClient client = new UserServiceClient();
                AdvancedResult<AdUser> userRes = client.GetUserInfoByID(item.CreateId);
                if (userRes.Error == DataStructure.AppError.ERROR_SUCCESS && userRes.Data != null)
                {
                    this.Author = new UserModel().Bind(userRes.Data, bindOptions);
                }
            }

            if (ContainEnumType<ReplyBindType>(bindOptions.ReplayBindType, ReplyBindType.Product))
            {
                ProductServiceClient client = new ProductServiceClient();
                AdvancedResult<ProProduct> product = client.GetBBInfo(item.ObjId);
                if (product.Error == AppError.ERROR_SUCCESS)
                {
                    this.Product = new ProductModel().Bind(product.Data, bindOptions);
                }
            }
            if (ContainEnumType<ReplyBindType>(bindOptions.ReplayBindType, ReplyBindType.RefReply))
            {
                ReplyServiceClient client = new ReplyServiceClient();
                if (--bindOptions.BindRefReplyCount >= 0 && item.RefReplyId>0)
                {
                    AdvancedResult<GenReply> refReply = client.Get(item.RefReplyId);
                    if (refReply.Error == AppError.ERROR_SUCCESS)
                    {
                        RefReply = new ReplyModel().Bind(refReply.Data, bindOptions);
                    }
                }
            }
            return this;
        }

        public static IList<ReplyModel> BindList(IList<GenReply> items, ViewModelBindOption bindOptions)
        {
            IList<ReplyModel> models = new List<ReplyModel>();
            if (bindOptions.BindReplyCount < 0)
                return models;
            if (items != null)
            {
                for (int i = 0, c = items.Count(); i < c; i++)
                {
                    models.Add(new ReplyModel().Bind(items[i], bindOptions));
                    if (i + 1 == bindOptions.BindReplyCount)
                        break;
                }
            }
            return models;
        }
    }


}