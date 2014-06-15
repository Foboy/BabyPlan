using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyPlan.mvcApp.ViewModel
{
    public struct ViewModelBindOption
    {
        public static ViewModelBindOption DefalutBindOption
        {
            get
            {
                ViewModelBindOption bindOptions = new ViewModelBindOption();
                bindOptions.BindProductCount = -1;
                bindOptions.BindProductPicsCount = 1;
                bindOptions.BindReplyCount = -1;
                bindOptions.BindRefReplyCount = -1;
                return bindOptions;
            }
        }

        public ProductBindType ProductBindType { get; set; }

        //public ProductItemBindType ProductItemBindType { get; set; }

        public ReplyBindType ReplayBindType { get; set; }

        public UserBindType UserBindType { get; set; }

        public BookBindType BookBindType { get; set; }

        public int BindProductCount { get; set; }

        public int BindProductPicsCount { get; set; }

        public int BindReplyCount { get; set; }

        public int BindRefReplyCount { get; set; }

    }
}