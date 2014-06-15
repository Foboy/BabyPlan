package com.baijiayi.app.ui;
import com.baijiayi.app.R;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

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
import android.telephony.TelephonyManager;
import android.text.Editable;
import android.text.Html;
import android.text.InputFilter;
import android.text.TextWatcher;
import android.view.View;
import android.view.WindowManager;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.HorizontalScrollView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.baijiayi.app.AppConfig;
import com.baijiayi.app.AppContext;
import com.baijiayi.app.AppException;
import com.baijiayi.app.bean.AdResult;
import com.baijiayi.app.bean.GoodsItemList;
import com.baijiayi.app.bean.GoodsModel;
import com.baijiayi.app.bean.PicModel;
import com.baijiayi.app.bean.PubPhotoList;
import com.baijiayi.app.bean.PubPhotoModel;
import com.baijiayi.app.bean.Tweet;
import com.baijiayi.app.bean.User;
import com.baijiayi.app.common.FileUtils;
import com.baijiayi.app.common.ImageUtils;
import com.baijiayi.app.common.MediaUtils;
import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.common.UIHelper;
import com.baijiayi.app.widget.PubPhotoListLayout;

/**
 * 发表动弹
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class GoodsPub extends Activity{

	private FrameLayout mForm;
	private ImageView mBack;
	private EditText mTitle;
	private RadioGroup mType;
	private RadioGroup mSex;
	private EditText mPrice;
	private EditText mAge;
	private EditText mContent;
	private Button mPublish;
	private ImageView mPick;
	private TextView mPickTxt;
	private EditText mQQ;
	private EditText mPhone;
	private CheckBox mIsWash;

	private PubPhotoListLayout mPhotoLayout;
	private HorizontalScrollView mPhtotsContainer;
	
	private Tweet tweet;
	private GoodsModel goods;
	private File imgFile;
	private String theLarge;
	private String theThumbnail;
	private InputMethodManager imm;
	
	private PubPhotoList photoList= new PubPhotoList();
	
	private String tempTweetKey = AppConfig.TEMP_TWEET;
	private String tempTweetImageKey = AppConfig.TEMP_TWEET_IMAGE;
	
	public static LinearLayout mMessage;
	public static Context mContext;
	private AppContext appContext;//全局Context
	
	private static final int MAX_TEXT_LENGTH = 160;//最大输入字数
	private static final String TEXT_ATME = "@请输入用户名 ";
	private static final String TEXT_SOFTWARE = "#请输入软件名#";
	private static final String TEXT_ISWASH = "承诺 \"<font color=\"#2A8FC4\">发布的衣物均经过清洗</font>\"";
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.goods_pub);
		
		mContext = this;
		appContext = (AppContext)getApplication();
		
		//软键盘管理类
		imm = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);
		
		getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_ADJUST_RESIZE |
                WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN); 
		
		//初始化基本视图
		this.initView();
	}
	
    @Override
	protected void onDestroy() {
    	mContext = null;
    	super.onDestroy();
	}
    
    
	//初始化视图控件
    private void initView()
    {    	
    	mForm = (FrameLayout)findViewById(R.id.tweet_pub_form);
    	mBack = (ImageView)findViewById(R.id.tweet_pub_back);
    	mMessage = (LinearLayout)findViewById(R.id.tweet_pub_message);

    	mPublish = (Button)findViewById(R.id.tweet_pub_publish);
    	mTitle = (EditText)findViewById(R.id.goods_pub_title);
    	mPrice = (EditText)findViewById(R.id.goods_pub_price);
    	mAge = (EditText)findViewById(R.id.goods_pub_age);
    	mType = (RadioGroup)findViewById(R.id.goods_pub_type_radiogroup);
    	mSex = (RadioGroup)findViewById(R.id.goods_pub_sex_radiogroup);
    	mContent = (EditText)findViewById(R.id.goods_pub_content);
    	mPick = (ImageView)findViewById(R.id.goods_pub_photo_btn);
    	mPickTxt = (TextView)findViewById(R.id.goods_pub_photo_txt);
    	mQQ = (EditText)findViewById(R.id.goods_pub_qq);
    	mPhone = (EditText)findViewById(R.id.goods_pub_phone);
    	mIsWash = (CheckBox)findViewById(R.id.goods_pub_iswash);
    	
    	String mphonenumber = getPhoneNumber();
    	User user= appContext.getLoginInfo();
    	if(!StringUtils.isEmpty(user.getPhone()))
    	{
    		mPhone.setText(user.getPhone());
    	}
    	else if(!StringUtils.isEmpty(mphonenumber))
    	{
    		mPhone.setText(mphonenumber);
    	}

    	
    	if(!StringUtils.isEmpty(user.getQq()))
    	{
    		mQQ.setText(user.getQq());
    	}
    	
    	mIsWash.setText(Html.fromHtml(TEXT_ISWASH));
    	
    	mPhtotsContainer = (HorizontalScrollView)findViewById(R.id.pub_photos_container); 

		mPhotoLayout = (PubPhotoListLayout)findViewById(R.id.pub_photos_Layout);
    	
    	mBack.setOnClickListener(UIHelper.finish(this));
    	mPublish.setOnClickListener(publishClickListener);

    	mPick.setOnClickListener(pickClickListener);
    	mPickTxt.setOnClickListener(pickClickListener);

    	
    	//编辑器添加文本监听
    	mContent.addTextChangedListener(new TextWatcher() {		
			@Override
			public void onTextChanged(CharSequence s, int start, int before, int count) {
				//保存当前EditText正在编辑的内容
				//((AppContext)getApplication()).setProperty(tempTweetKey, s.toString());
				//显示剩余可输入的字数
			}		
			@Override
			public void beforeTextChanged(CharSequence s, int start, int count, int after) {}		
			@Override
			public void afterTextChanged(Editable s) {}
		});
    	//编辑器点击事件
    	mContent.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				//显示软键盘
				showIMM();
			}
		});
    	//设置最大输入字数
    	InputFilter[] filters = new InputFilter[1];  
    	filters[0] = new InputFilter.LengthFilter(MAX_TEXT_LENGTH);
    	mContent.setFilters(filters);
    	
    	//显示临时编辑内容
		//UIHelper.showTempEditContent(this, mContent, tempTweetKey);
		//显示临时保存图片
/*		photoList = (PubPhotoList)appContext.readObject(PubPhotoList.pubPhotoKey);
		if(photoList==null) {
			photoList= new PubPhotoList();
		}
		mPhotoLayout.setPhotoList(photoList,appContext);
		if(photoList.getPhotolist().size()>0)
		{
			mPhotoLayout.notifyItemsChanged();
		}
		*/
    	photoList= new PubPhotoList();
    	mPhotoLayout.setPhotoList(photoList,appContext);
    }
    
    private String getPhoneNumber(){  
        TelephonyManager mTelephonyMgr;  
        mTelephonyMgr = (TelephonyManager)  getSystemService(Context.TELEPHONY_SERVICE);   
        return mTelephonyMgr.getLine1Number();  
    }  
    
    private void showIMM() {
    	imm.showSoftInput(mContent, 0);
    }
    
    
	private View.OnClickListener pickClickListener = new View.OnClickListener() {
		@Override
		public void onClick(View v) {	
			//隐藏软键盘
			imm.hideSoftInputFromWindow(v.getWindowToken(), 0);  
			if(photoList.getPhotolist().size()>6)
			{
				UIHelper.ToastMessage(v.getContext(), "一个宝贝最多只能上传6张图片哦亲！");
				return;
			}
			CharSequence[] items = {
					GoodsPub.this.getString(R.string.img_from_album),
					GoodsPub.this.getString(R.string.img_from_camera)
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
							UIHelper.ToastMessage(GoodsPub.this, "无法保存照片，请检查SD卡是否挂载");
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
					PubPhotoModel photo = new PubPhotoModel((Bitmap)msg.obj,imgFile);
					photoList.getPhotolist().add(photo);
					mPhotoLayout.notifyItemsChanged();
/*					int mw = mPhotoLayout.getMeasuredWidth();
					int pw = mPhtotsContainer.getWidth();
				    int off = mPhotoLayout.getMeasuredWidth() - mPhtotsContainer.getWidth(); 
				    if (off > 0) { 
				    	   mPhtotsContainer.scrollBy(off, 0); 
				       }*/
					//保存动弹临时图片
					appContext.saveObject(photoList, PubPhotoList.pubPhotoKey);
					
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
		        		theLarge = ImageUtils.getAbsoluteImagePath(GoodsPub.this,thisUri);
		        	}
		        	else
		        	{
		        		theLarge = thePath;
		        	}
		        	
		        	String attFormat = FileUtils.getFileFormat(theLarge);
		        	if(!"photo".equals(MediaUtils.getContentType(attFormat)))
		        	{
		        		Toast.makeText(GoodsPub.this, getString(R.string.choose_image), Toast.LENGTH_SHORT).show();
		        		return;
		        	}
		        	
		        	//获取图片缩略图 只有Android2.1以上版本支持
		    		if(AppContext.isMethodsCompat(android.os.Build.VERSION_CODES.ECLAIR_MR1)){
		    			String imgName = FileUtils.getFileName(theLarge);
		    			bitmap = ImageUtils.loadImgThumbnail(GoodsPub.this, imgName, MediaStore.Images.Thumbnails.MICRO_KIND);
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
								ImageUtils.createImageThumbnail(GoodsPub.this, theLarge, theThumbnail, 800, 80);
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
			
			if(photoList.getPhotolist().size() <=0)
			{
				UIHelper.ToastMessage(v.getContext(), "至少需要上传一张图片哦亲！");
				return;
			}
			
			String title = mTitle.getText().toString();
			if(StringUtils.isEmpty(title)){
				UIHelper.ToastMessage(v.getContext(), "请输入宝贝标题哦亲！");
				return;
			}
			
			if(title.length()<3)
			{
				UIHelper.ToastMessage(v.getContext(), "标题不能少于6个字哦亲！");
				return;
			}
			
			String price = mPrice.getText().toString();
			if(StringUtils.isEmpty(price)){
				UIHelper.ToastMessage(v.getContext(), "请输入宝贝价钱哦亲！");
				return;
			}
			
			String age = mAge.getText().toString();
			if(StringUtils.isEmpty(age)){
				UIHelper.ToastMessage(v.getContext(), "请输入宝贝适合年龄！");
				return;
			}
			
			String content = mContent.getText().toString();
			if(StringUtils.isEmpty(content)){
				UIHelper.ToastMessage(v.getContext(), "请输入宝贝描述哦亲！");
				return;
			}
			
			if(content.length()<3)
			{
				UIHelper.ToastMessage(v.getContext(), "描述不能少于6个字哦亲！");
				return;
			}
			
			String qq = mQQ.getText().toString();
			String phone = mPhone.getText().toString();
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
			
			int itemType = GoodsItemList.CATALOG_CLOTHES;
			if(((RadioButton)GoodsPub.this.findViewById(R.id.goods_pub_type_clothes)).isChecked())
			{
				itemType=GoodsItemList.CATALOG_CLOTHES;
			}
			else if(((RadioButton)GoodsPub.this.findViewById(R.id.goods_pub_type_toys)).isChecked())
			{
				itemType=GoodsItemList.CATALOG_TOYS;
			}
			else if(((RadioButton)GoodsPub.this.findViewById(R.id.goods_pub_type_others)).isChecked())
			{
				itemType=GoodsItemList.CATALOG_OHERS;
			}
			
			int itemSex = GoodsItemList.SEX_BOY;
			if(((RadioButton)GoodsPub.this.findViewById(R.id.goods_pub_sex_boy)).isChecked())
			{
				itemSex=GoodsItemList.SEX_BOY;
			}
			else if(((RadioButton)GoodsPub.this.findViewById(R.id.goods_pub_sex_girl)).isChecked())
			{
				itemSex=GoodsItemList.SEX_GIRL;
			}
			//(RadioButton)GoodsPub.this.findViewById(mSex.getCheckedRadioButtonId());

			
			final AppContext ac = (AppContext)getApplication();
			if(!ac.isLogin()){
				UIHelper.showLoginDialog(GoodsPub.this);
				return;
			}	
			
			mMessage.setVisibility(View.VISIBLE);
			mForm.setVisibility(View.GONE);
			
			goods = new GoodsModel();
			goods.setTitle(title);
			goods.setDescription(content);
			goods.setIsWash(mIsWash.isChecked());
			goods.setQQ(qq);
			goods.setPhone(phone);
			goods.setPrice(Double.parseDouble( price));
			goods.setAge(Integer.parseInt(age));
			goods.setItemType(itemType);
			goods.setSex(itemSex);
			
			for(PubPhotoModel ppm:photoList.getPhotolist())
			{
				PicModel gim = new PicModel();
				gim.setPubPic(ppm.getFile());
				goods.getPics().add(gim);
			}
			
			final Handler handler = new Handler(){
				@Override
				public void handleMessage(Message msg) {
					if(msg.what == 1){
				    	if(!StringUtils.isEmpty(goods.getQQ()))
				    	{
				    		ac.saveUserQQ(goods.getQQ());
				    	}
				    	if(!StringUtils.isEmpty(goods.getPhone()))
				    	{
				    		ac.saveUserPhone(goods.getPhone());
				    	}
						//清除之前保存的编辑内容
						ac.removeProperty(tempTweetKey,tempTweetImageKey);						
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
						res = ac.pubGoods(goods);
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
