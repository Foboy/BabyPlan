package com.baijiayi.app.ui;
import com.baijiayi.app.R;
import android.app.Activity;
import android.content.Intent;
import android.graphics.drawable.AnimationDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.ViewSwitcher;

import com.baijiayi.app.AppContext;
import com.baijiayi.app.AppException;
import com.baijiayi.app.api.ApiClient;
import com.baijiayi.app.bean.GoodsItemList;
import com.baijiayi.app.bean.Result;
import com.baijiayi.app.bean.User;
import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.common.UIHelper;

/**
 * 用户登录对话框
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class RegisterDialog extends Activity{
	
	private ImageView mBack;
	private TextView mTitle;
	
	private ViewSwitcher mViewSwitcher;
	private ImageButton btn_close;
	private Button btn_login;
	private Button btn_register;
	private AutoCompleteTextView mAccount;
	private EditText mPwd;
	private EditText mPwdConfirm;
	private AnimationDrawable loadingAnimation;
	private View loginLoading;
	private CheckBox chb_rememberMe;
	private int curLoginType;
	private InputMethodManager imm;
	
	public final static int LOGIN_OTHER = 0x00;
	public final static int LOGIN_MAIN = 0x01;
	public final static int LOGIN_SETTING = 0x02;
	public final static int LOGIN_MAINGOODS = 0x03;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register_dialog);
        
        imm = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
        
        curLoginType = getIntent().getIntExtra("LOGINTYPE", LOGIN_OTHER);
        
        mViewSwitcher = (ViewSwitcher)findViewById(R.id.logindialog_view_switcher);       
        loginLoading = (View)findViewById(R.id.login_loading);
        mAccount = (AutoCompleteTextView)findViewById(R.id.register_account);
        mPwd = (EditText)findViewById(R.id.register_password);
        mPwdConfirm = (EditText)findViewById(R.id.register_password_confirm);
        chb_rememberMe = (CheckBox)findViewById(R.id.login_checkbox_rememberMe);
        
        btn_close = (ImageButton)findViewById(R.id.login_close_button);
        btn_close.setOnClickListener(UIHelper.finish(this));        
        
        btn_login = (Button)findViewById(R.id.login_btn_login);
        btn_login.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				UIHelper.showLoginDialog(null);
				finish();
			}
        });
        btn_register = (Button)findViewById(R.id.register_btn_register);
        btn_register.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				//隐藏软键盘
				imm.hideSoftInputFromWindow(v.getWindowToken(), 0);  
				
				String account = mAccount.getText().toString();
				String pwd = mPwd.getText().toString();
				String pwdConfirm = mPwdConfirm.getText().toString();
				boolean isRememberMe = chb_rememberMe.isChecked();
				//判断输入
				if(StringUtils.isEmpty(account)){
					UIHelper.ToastMessage(v.getContext(), getString(R.string.msg_login_email_null));
					return;
				}
				if(StringUtils.isEmpty(pwd)){
					UIHelper.ToastMessage(v.getContext(), getString(R.string.msg_login_pwd_null));
					return;
				}
				
				if(!pwd.equals(pwdConfirm))
				{
					UIHelper.ToastMessage(v.getContext(), getString(R.string.msg_login_password_inconformity));
					return;
				}
				
				int sex = GoodsItemList.SEX_BOY;
				if(((RadioButton)RegisterDialog.this.findViewById(R.id.register_sex_boy)).isChecked())
				{
					sex=GoodsItemList.SEX_BOY;
				}
				else if(((RadioButton)RegisterDialog.this.findViewById(R.id.register_sex_girl)).isChecked())
				{
					sex=GoodsItemList.SEX_GIRL;
				}
				
		        btn_close.setVisibility(View.GONE);
		        loadingAnimation = (AnimationDrawable)loginLoading.getBackground();
		        loadingAnimation.start();
		        mViewSwitcher.showNext();
		        
		        register(account, pwd,sex, isRememberMe);
			}
		});

        //是否显示登录信息
/*        AppContext ac = (AppContext)getApplication();
        User user = ac.getLoginInfo();
        if(user==null || !user.isRememberMe()) return;
        if(!StringUtils.isEmpty(user.getAccount())){
        	mAccount.setText(user.getAccount());
        	mAccount.selectAll();
        	chb_rememberMe.setChecked(user.isRememberMe());
        }
        if(!StringUtils.isEmpty(user.getPwd())){
        	mPwd.setText(user.getPwd());
        }*/
        
    	mBack = (ImageView)findViewById(R.id.frame_main_header_back);
    	mBack.setOnClickListener(UIHelper.finish(this));
    	
    	mTitle=(TextView)findViewById(R.id.frame_main_header_title);
    	mTitle.setText("注册");
    }
    
    //登录验证
    private void register(final String account, final String pwd, final int sex,final boolean isRememberMe) {
		final Handler handler = new Handler() {
			@Override
			public void handleMessage(Message msg) {
				if(msg.what == 1){
					User user = (User)msg.obj;
					if(user != null){
						//清空原先cookie
						ApiClient.cleanCookie();
						//发送通知广播
						UIHelper.sendBroadCast(RegisterDialog.this, user.getNotice());
						//提示登陆成功
						UIHelper.ToastMessage(RegisterDialog.this, R.string.msg_login_success);
						if(curLoginType == LOGIN_MAIN){
							//跳转--加载用户动态
							Intent intent = new Intent(RegisterDialog.this, Main.class);
							intent.putExtra("LOGIN", true);
							startActivity(intent);
						}else if(curLoginType == LOGIN_SETTING){
							//跳转--用户设置页面
							Intent intent = new Intent(RegisterDialog.this, Setting.class);
							intent.putExtra("LOGIN", true);
							startActivity(intent);
						}else if(curLoginType == LOGIN_MAINGOODS){
							//跳转--宝贝发布
							Intent intent = new Intent(RegisterDialog.this, GoodsPub.class);
							intent.putExtra("LOGIN", true);
							startActivity(intent);
						}
						finish();
					}
				}else if(msg.what == 0){
					mViewSwitcher.showPrevious();
					btn_close.setVisibility(View.VISIBLE);
					UIHelper.ToastMessage(RegisterDialog.this, getString(R.string.msg_login_fail)+msg.obj);
				}else if(msg.what == -1){
					mViewSwitcher.showPrevious();
					btn_close.setVisibility(View.VISIBLE);
					((AppException)msg.obj).makeToast(RegisterDialog.this);
				}
			}
		};
		new Thread(){
			@Override
			public void run() {
				Message msg =new Message();
				try {
					AppContext ac = (AppContext)getApplication(); 
	                User user = ac.register(account, pwd,sex);
	                user.setAccount(account);
	                user.setPwd(pwd);
	                user.setRememberMe(isRememberMe);
	                Result res = user.getValidate();
	                if(res.OK()){
	                	ac.saveLoginInfo(user);//保存登录信息
	                	msg.what = 1;//成功
	                	msg.obj = user;
	                }else{
	                	ac.cleanLoginInfo();//清除登录信息
	                	msg.what = 0;//失败
	                	msg.obj = res.getErrorMessage();
	                }
	            } catch (AppException e) {
	            	e.printStackTrace();
			    	msg.what = -1;
			    	msg.obj = e;
	            }
				handler.sendMessage(msg);
			}
		}.start();
    }
}
