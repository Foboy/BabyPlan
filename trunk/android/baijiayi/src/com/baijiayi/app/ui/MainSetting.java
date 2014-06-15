package com.baijiayi.app.ui;
import com.baijiayi.app.AppContext;
import com.baijiayi.app.R;
import android.app.Activity;
import android.content.Intent;
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

public class MainSetting extends Activity {
	
	private ImageView mBack;
	private TextView mTitle;
	private Button mHeadPub_post;
	private ProgressBar mProgressbar;
	
	private Button user_set;
	private Button about_us;
	
	private AppContext appContext;//全局Context

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_setting);
        
        appContext = (AppContext)getApplication();
        
        initHeader();
        
        initControls();
    }
    
    private void initControls()
    {
    	user_set=(Button)findViewById(R.id.main_setting_usersetting);
    	about_us=(Button)findViewById(R.id.main_setting_aboutus);
    	
    	user_set.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
	    		//判断登录
				int uid = appContext.getLoginUid();
				if(uid == 0){
					UIHelper.showLoginDialog(MainSetting.this);
					return;
				}   
				AdResult result = appContext.TryLogin();
				if(result.OK())
				{
					Intent intent = new Intent(MainSetting.this,UserSetting.class);
					startActivity(intent);
				}
				else
				{
					appContext.Logout();
					UIHelper.ToastMessage(v.getContext(), result.getErrorMessage());
					UIHelper.showLoginDialog(MainSetting.this);
					return;
				}
			}
		});
    	about_us.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
					Intent intent = new Intent(MainSetting.this,AboutUs.class);
					startActivity(intent);
			}
		});
    }
    
    private void initHeader()
    {
    	mBack = (ImageView)findViewById(R.id.frame_main_header_back);
    	mBack.setOnClickListener(UIHelper.finish(this));
    	
    	mTitle=(TextView)findViewById(R.id.frame_main_header_title);
    	mTitle.setText("设置");
    	
    	mHeadPub_post = (Button)findViewById(R.id.frame_main_header_post);

    	mHeadPub_post.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
	    		//判断登录
				int uid = appContext.getLoginUid();
				if(uid == 0){
					UIHelper.showLoginDialog(MainSetting.this);
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
					UIHelper.showLoginDialog(MainSetting.this);
					return;
				}
			}
		});
    
    	
    }
}
