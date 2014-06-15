
package com.baijiayi.app.ui;
import com.baijiayi.app.AppContext;
import com.baijiayi.app.R;
import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.baijiayi.app.R;
import com.baijiayi.app.bean.AdResult;
import com.baijiayi.app.common.UIHelper;

public class AboutUs extends Activity {
	
	private ImageView mBack;
	private TextView mTitle;
	private Button mHeadPub_post;
	private ProgressBar mProgressbar;
	
	private AppContext appContext;//全局Context

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.about_us);
        
        appContext = (AppContext)getApplication();
        
        initHeader();
    }
    
    private void initHeader()
    {
    	mBack = (ImageView)findViewById(R.id.frame_main_header_back);
    	mBack.setOnClickListener(UIHelper.finish(this));
    	
    	mTitle=(TextView)findViewById(R.id.frame_main_header_title);
    	mTitle.setText("关于我们");
    	
    	mHeadPub_post = (Button)findViewById(R.id.frame_main_header_post);

    	mHeadPub_post.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
	    		//判断登录
				int uid = appContext.getLoginUid();
				if(uid == 0){
					UIHelper.showLoginDialog(AboutUs.this);
					return;
				}   
				AdResult result = appContext.TryLogin();
				if(result.OK())
				{
					UIHelper.showGoodsPub(v.getContext());
				}
				else
				{
					appContext.Logout();
					UIHelper.ToastMessage(v.getContext(), result.getErrorMessage());
					UIHelper.showLoginDialog(AboutUs.this);
					return;
				}
			}
		});
    
    	
    }
}
