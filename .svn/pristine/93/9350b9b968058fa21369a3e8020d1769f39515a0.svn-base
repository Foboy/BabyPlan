package com.baijiayi.app;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.ui.MainGoods;

/**
 * 应用程序启动类：显示欢迎界面并跳转到主界面
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class AppStart extends Activity {
	
	private LinearLayout first_loading;
	
	private AppContext appContext;
    
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        final View view = View.inflate(this, R.layout.start, null);
		setContentView(view);
		

		appContext = (AppContext)getApplication();
		
		String first_open = appContext.getProperty("first_open");
		if(StringUtils.isEmpty(first_open)) 
		{
			first_loading = (LinearLayout)findViewById(R.id.first_pic);
			first_loading.setVisibility(View.VISIBLE);
			AlphaAnimation aa = new AlphaAnimation(0.3f,1.0f);
			aa.setDuration(2000);
			view.startAnimation(aa);
			aa.setAnimationListener(new AnimationListener()
			{
				@Override
				public void onAnimationEnd(Animation arg0) {
					AlphaAnimation aa = new AlphaAnimation(1.0f,1.0f);
					aa.setDuration(3000);
					view.startAnimation(aa);
					aa.setAnimationListener(new AnimationListener()
					{
						@Override
						public void onAnimationEnd(Animation arg0) {
							AlphaAnimation aa = new AlphaAnimation(1.0f,0.0f);
							aa.setDuration(1000);
							AppStart.this.first_loading.startAnimation(aa);
							aa.setAnimationListener(new AnimationListener()
							{
								@Override
								public void onAnimationEnd(Animation arg0) {
									AppStart.this.first_loading.setVisibility(View.GONE);
									AppStart.this.appContext.setProperty("first_open", "opened");
									AlphaAnimation aa = new AlphaAnimation(1.0f,1.0f);
									aa.setDuration(2000);
									view.startAnimation(aa);
									aa.setAnimationListener(new AnimationListener()
									{
										@Override
										public void onAnimationEnd(Animation arg0) {
											redirectTo();
										}
										@Override
										public void onAnimationRepeat(Animation animation) {}
										@Override
										public void onAnimationStart(Animation animation) {}
										
									});
								}
								@Override
								public void onAnimationRepeat(Animation animation) {}
								@Override
								public void onAnimationStart(Animation animation) {}
								
							});
						}
						@Override
						public void onAnimationRepeat(Animation animation) {}
						@Override
						public void onAnimationStart(Animation animation) {}
						
					});
				}
				@Override
				public void onAnimationRepeat(Animation animation) {}
				@Override
				public void onAnimationStart(Animation animation) {}
				
			});
		}
		else
		{
			//渐变展示启动屏
			AlphaAnimation aa = new AlphaAnimation(0.3f,1.0f);
			aa.setDuration(2000);
			view.startAnimation(aa);
			aa.setAnimationListener(new AnimationListener()
			{
				@Override
				public void onAnimationEnd(Animation arg0) {
					redirectTo();
				}
				@Override
				public void onAnimationRepeat(Animation animation) {}
				@Override
				public void onAnimationStart(Animation animation) {}
				
			});
		}
		
		//兼容低版本cookie（1.5版本以下，包括1.5.0,1.5.1）
		String cookie = appContext.getProperty("cookie");
		if(StringUtils.isEmpty(cookie)) {
			String cookie_name = appContext.getProperty("cookie_name");
			String cookie_value = appContext.getProperty("cookie_value");
			if(!StringUtils.isEmpty(cookie_name) && !StringUtils.isEmpty(cookie_value)) {
				cookie = cookie_name + "=" + cookie_value;
				appContext.setProperty("cookie", cookie);
				appContext.removeProperty("cookie_domain","cookie_name","cookie_value","cookie_version","cookie_path");
			}
		}
    }
    
    /**
     * 跳转到...
     */
    private void redirectTo(){        
        Intent intent = new Intent(this, MainGoods.class);
        startActivity(intent);
        finish();
    }
}