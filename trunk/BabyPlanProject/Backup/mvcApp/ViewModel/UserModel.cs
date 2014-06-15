using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.Meta;
using BabyPlan.DataStructure;
using BabyPlan.Common;
using BabyPlan.WcfService;

namespace BabyPlan.mvcApp.ViewModel
{
    [Serializable]
    public class UserModel : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Account { get; set; }


        public DateTime BabyBirthday { get; set; }

        public bool HasBindBabyBirthday
        {
            get
            {
                string binded = CookieHelper.Get(string.Format(CookieKeys.BirthdayBindWindPop, Account));
                if (binded == "1")
                {
                    return true;
                }
                else
                {
                    return this.BabyBirthday.Year > 1900;
                }
            }
        }
        public void PopBabyBirthdayBindWind()
        {
            CookieHelper.Set(string.Format(CookieKeys.BirthdayBindWindPop, Account), "1", DateTime.MinValue);
        }

        public int PostProductNum { get; set; }

        public string QQ { get; set; }

        public string Phone { get; set; }

        public SexType Sex { get; set; }

        public PicModel HeadPic { get; set; }


        public UserModel Bind(AdUser user,ViewModelBindOption bindOption)
        {
            if (user != null)
            {
                this.Id = user.AdUserId;
                this.Account = user.UserAccount;
                this.Name = user.UserAccount;
                this.Sex = user.Sex;
                this.BabyBirthday = user.BabyBirthday;
                this.QQ = user.Qq;
                this.Phone = user.Mobile;
                this.HeadPic = new PicModel();
                if (user.PicId > 0 && user.HeadImgUrl != null)
                {
                    this.HeadPic.BPicUrl = user.HeadImgUrl;
                    this.HeadPic.PicWidth = user.PicWidth;
                    this.HeadPic.PicHeight = user.PicHeight;
                    this.HeadPic.PicId = user.PicId;
                }
                else
                {
                    this.HeadPic.BPicUrl ="/images/tx.png";
                    this.HeadPic.PicWidth = 277;
                    this.HeadPic.PicHeight = 273;
                }

                if (ContainEnumType<UserBindType>(bindOption.UserBindType, UserBindType.PubProductCount))
                {
                    ProductServiceClient client = new ProductServiceClient();
                    AdvancedResult<int> postNum = client.GetProductCountByUserId(user.AdUserId);
                    if (postNum.Error == AppError.ERROR_SUCCESS)
                    {
                        PostProductNum = postNum.Data;
                    }
                }
            }
            return this;
        }
    }
}