using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.Meta;
using BabyPlan.mvcApp.ViewModel;

namespace BabyPlan.mvcApp.EventsHandler
{
    public class ProductEventsHandler
    {
        private delegate void ProductPosted(AdUser user,ProductModel product);
        private event ProductPosted OnProductPostedEvent;

        private ProductEventsHandler()
        {
            OnProductPostedEvent += new ProductPosted(OnProductPosted);
        }

        public void RiseProductPostedEvent(AdUser user, ProductModel product)
        {
            AsyncCallback ac = new AsyncCallback(ProductPostedCallBack);
            if (OnProductPostedEvent != null)
                OnProductPostedEvent.BeginInvoke(user, product, ac, null);
        }

        private void ProductPostedCallBack(IAsyncResult result)
        { 
        
        }

        private void OnProductPosted(AdUser user, ProductModel product)
        { 
            
        }

        private static readonly ProductEventsHandler instance = new ProductEventsHandler();

        public static ProductEventsHandler Instance
        {
            get { return instance; }
        }
    }
}