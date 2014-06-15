

package com.baijiayi.app.ui;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.baijiayi.app.AppContext;
import com.baijiayi.app.AppException;
import com.baijiayi.app.R;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.provider.MediaStore;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;

import com.baijiayi.app.R;
import com.baijiayi.app.bean.AdResult;
import com.baijiayi.app.bean.GoodsItemList;
import com.baijiayi.app.bean.GoodsModel;
import com.baijiayi.app.bean.PicModel;
import com.baijiayi.app.bean.PubPhotoList;
import com.baijiayi.app.bean.PubPhotoModel;
import com.baijiayi.app.bean.User;
import com.baijiayi.app.common.FileUtils;
import com.baijiayi.app.common.ImageUtils;
import com.baijiayi.app.common.MediaUtils;
import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.common.UIHelper;
import com.baijiayi.app.widget.ImageViewBorder;

public class UserSetting extends Activity {
	
	private LinearLayout mForm;
	public static LinearLayout mMessage;
	
	private ImageView mBack;
	private TextView mTitle;
	private Button mHeadPub_post;
	private ProgressBar mProgressbar;
	
	public static Context mContext;
	
	private File imgFile;
	private String theLarge;
	private String theThumbnail;
	private InputMethodManager imm;
	
	private ImageViewBorder userface;
	private EditText mQQ;
	private EditText mPhone;
	
	private Button btn_change_head;
	private Button btn_save;
	private Button btn_logout;
	
	private AppContext appContext;//全局Context

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.user_setting);
        
		mContext = this;
		appContext = (AppContext)getApplication();
        
        initHeader();
        initView();
        initData();
        
		//软键盘管理类
		imm = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
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
					UIHelper.showLoginDialog(UserSetting.this);
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
					UIHelper.showLoginDialog(UserSetting.this);
					return;
				}
			}
		});
    }
    
    private void initView()
    {
    	mMessage = (LinearLayout)findViewById(R.id.user_set_save_message);
    	mForm = (LinearLayout)findViewById(R.id.user_set_form);
    	//
    	userface = (ImageViewBorder)findViewById(R.id.user_setting_pic);
    	mQQ = (EditText)findViewById(R.id.user_setting_qq);
    	mPhone = (EditText)findViewById(R.id.user_setting_phone);
    	
    	btn_change_head =(Button)findViewById(R.id.user_setting_update_head);
    	btn_save = (Button)findViewById(R.id.user_setting_save);
    	btn_logout = (Button)findViewById(R.id.user_setting_logout);
    	
    	userface.setOnClickListener(pickClickListener);
    	btn_change_head.setOnClickListener(pickClickListener);
    	
    	btn_save.setOnClickListener(publishClickListener);
    	btn_logout.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				appContext.Logout();
				finish();
			}
		});
    }
    
    private void initData()
    {
    	User user= appContext.getLoginInfo();
    	if(!StringUtils.isEmpty(user.getPhone()))
     	{
     		mPhone.setText(user.getPhone());
     	}
     	
     	if(!StringUtils.isEmpty(user.getQq()))
     	{
     		mQQ.setText(user.getQq());
     	}
     	
		//加载用户头像
		String faceURL = user.getFace();
		if(faceURL.endsWith("portrait.gif") || StringUtils.isEmpty(faceURL)) {
			userface.setImageResource(R.drawable.widget_dface);
		}else{
			UIHelper.showUserFace(userface, faceURL);
		}
		
    }
    
    
	private View.OnClickListener pickClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {	
			//隐藏软键盘
			imm.hideSoftInputFromWindow(v.getWindowToken(), 0);  
			CharSequence[] items = {
					UserSetting.this.getString(R.string.img_from_album),
					UserSetting.this.getString(R.string.img_from_camera)
			};
			imageChooseItem(items);
		}
	};
	/**
	 * 操作选择
	 * @param items
	 */
	public void imageChooseItem(CharSequence[] items )
	{
		AlertDialog imageDialog = new AlertDialog.Builder(this).setTitle(R.string.ui_insert_image).setIcon(android.R.drawable.btn_star).setItems(items,
			new DialogInterface.OnClickListener(){
				@Override
				public void onClick(DialogInterface dialog, int item)
				{
					//手机选图
					if( item == 0 )
					{
						Intent intent = new Intent(Intent.ACTION_GET_CONTENT); 
						intent.addCategory(Intent.CATEGORY_OPENABLE); 
						intent.setType("image/*"); 
						startActivityForResult(Intent.createChooser(intent, "选择图片"),ImageUtils.REQUEST_CODE_GETIMAGE_BYSDCARD); 
					}
					//拍照
					else if( item == 1 )
					{	
						String savePath = "";
						//判断是否挂载了SD卡
						String storageState = Environment.getExternalStorageState();		
						if(storageState.equals(Environment.MEDIA_MOUNTED)){
							savePath = Environment.getExternalStorageDirectory().getAbsolutePath() + "/Baijiayi/Camera/";//存放照片的文件夹
							File savedir = new File(savePath);
							if (!savedir.exists()) {
								savedir.mkdirs();
							}
						}
						
						//没有挂载SD卡，无法保存文件
						if(StringUtils.isEmpty(savePath)){
							UIHelper.ToastMessage(UserSetting.this, "无法保存照片，请检查SD卡是否挂载");
							return;
						}

						String timeStamp = new SimpleDateFormat("yyyyMMddHHmmss").format(new Date());
						String fileName = "osc_" + timeStamp + ".jpg";//照片命名
						File out = new File(savePath, fileName);
						Uri uri = Uri.fromFile(out);
						
						theLarge = savePath + fileName;//该照片的绝对路径
						
						Intent intent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
						intent.putExtra(MediaStore.EXTRA_OUTPUT, uri);
						startActivityForResult(intent, ImageUtils.REQUEST_CODE_GETIMAGE_BYCAMERA);
					}   
				}}).create();
		
		 imageDialog.show();
	}

	@Override 
	protected void onActivityResult(final int requestCode, final int resultCode, final Intent data)
	{ 
    	if(resultCode != RESULT_OK) return;
		
		final Handler handler = new Handler(){
			@Override
			public void handleMessage(Message msg) {
				if(msg.what == 1 && msg.obj != null){
					//显示图片
					userface.setImageBitmap((Bitmap)msg.obj);
				}
			}
		};
		
		new Thread(){
			@Override
			public void run() 
			{
				Bitmap bitmap = null;
				
		        if(requestCode == ImageUtils.REQUEST_CODE_GETIMAGE_BYSDCARD) 
		        {         	
		        	if(data == null)  return;
		        	
		        	Uri thisUri = data.getData();
		        	String thePath = ImageUtils.getAbsolutePathFromNoStandardUri(thisUri);
		        	
		        	//如果是标准Uri
		        	if(StringUtils.isEmpty(thePath))
		        	{
		        		theLarge = ImageUtils.getAbsoluteImagePath(UserSetting.this,thisUri);
		        	}
		        	else
		        	{
		        		theLarge = thePath;
		        	}
		        	
		        	String attFormat = FileUtils.getFileFormat(theLarge);
		        	if(!"photo".equals(MediaUtils.getContentType(attFormat)))
		        	{
		        		Toast.makeText(UserSetting.this, getString(R.string.choose_image), Toast.LENGTH_SHORT).show();
		        		return;
		        	}
		        	
		        	//获取图片缩略图 只有Android2.1以上版本支持
		    		if(AppContext.isMethodsCompat(android.os.Build.VERSION_CODES.ECLAIR_MR1)){
		    			String imgName = FileUtils.getFileName(theLarge);
		    			bitmap = ImageUtils.loadImgThumbnail(UserSetting.this, imgName, MediaStore.Images.Thumbnails.MICRO_KIND);
		    		}
		        	
		        	if(bitmap == null && !StringUtils.isEmpty(theLarge))
		        	{
		        		bitmap = ImageUtils.loadImgThumbnail(theLarge, 100, 100);
		        	}
		        }
		        //拍摄图片
		        else if(requestCode == ImageUtils.REQUEST_CODE_GETIMAGE_BYCAMERA)
		        {	
		        	if(bitmap == null && !StringUtils.isEmpty(theLarge))
		        	{
		        		bitmap = ImageUtils.loadImgThumbnail(theLarge, 100, 100);
		        	}
		        }
		        
				if(bitmap!=null)
				{
					//存放照片的文件夹
					String savePath = Environment.getExternalStorageDirectory().getAbsolutePath() + "/Baijiayi/Camera/";
					File savedir = new File(savePath);
					if (!savedir.exists()) {
						savedir.mkdirs();
					}
					
					String largeFileName = FileUtils.getFileName(theLarge);
					String largeFilePath = savePath + largeFileName;
					//判断是否已存在缩略图
					if(largeFileName.startsWith("thumb_") && new File(largeFilePath).exists()) 
					{
						theThumbnail = largeFilePath;
						imgFile = new File(theThumbnail);
					} 
					else 
					{
						//生成上传的800宽度图片
						String thumbFileName = "thumb_" + largeFileName;
						theThumbnail = savePath + thumbFileName;
						if(new File(theThumbnail).exists())
						{
							imgFile = new File(theThumbnail);
						}
						else
						{
							try {
								//压缩上传的图片
								ImageUtils.createImageThumbnail(UserSetting.this, theLarge, theThumbnail, 800, 80);
								imgFile = new File(theThumbnail);
							} catch (IOException e) {
								e.printStackTrace();
							}	
						}						
					}					
					
					Message msg = new Message();
					msg.what = 1;
					msg.obj = bitmap;
					handler.sendMessage(msg);
				}				
			};
		}.start();
    }
	
	private View.OnClickListener publishClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {	
			//隐藏软键盘
			imm.hideSoftInputFromWindow(v.getWindowToken(), 0);  
			
			final String qq = mQQ.getText().toString();
			final String phone = mPhone.getText().toString();
			if(!StringUtils.isEmpty(qq)){
				Pattern p = Pattern.compile("^[1-9][0-9]{3,10}$");     
			    Matcher m = p.matcher(qq);  
			    if(!m.matches())
			    {
			    	UIHelper.ToastMessage(v.getContext(), "请输入正确的QQ哦亲！");
			    	return;
			    }
			}
			
			if(!StringUtils.isEmpty(phone)){
				Pattern p = Pattern.compile("^1[0-9]{10}$");     
			    Matcher m = p.matcher(phone);  
			    if(!m.matches())
			    {
			    	UIHelper.ToastMessage(v.getContext(), "请输入正确的手机号码哦亲！");
			    	return;
			    }
			}
			
			if(StringUtils.isEmpty(phone) && StringUtils.isEmpty(qq))
			{
		    	UIHelper.ToastMessage(v.getContext(), "请输入手机号码或者QQ哦亲！");
		    	return;
			}

			final AppContext ac = (AppContext)getApplication();
			if(!ac.isLogin()){
				UIHelper.showLoginDialog(UserSetting.this);
				return;
			}	
			
			mMessage.setVisibility(View.VISIBLE);
			mForm.setVisibility(View.GONE);
			
			final Handler handler = new Handler(){
				@Override
				public void handleMessage(Message msg) {
					if(msg.what == 1){
						//清除之前保存的编辑内容					
						finish();
					}else{
						mMessage.setVisibility(View.GONE);
						mForm.setVisibility(View.VISIBLE);
					}
				}				
			};
			
			new Thread(){
				@Override
				public void run() {
					Message msg =new Message();
					AdResult res = null;
					int what = 0;
					try {
						res = ac.userSetSave(imgFile, qq, phone);
						what = 1;
						msg.what = 1;
						msg.obj = res;
		            } catch (AppException e) {
		            	e.printStackTrace();
						msg.what = -1;
						msg.obj = e;
		            }
					handler.sendMessage(msg);
				}
			}.start();
		}
	};

}
