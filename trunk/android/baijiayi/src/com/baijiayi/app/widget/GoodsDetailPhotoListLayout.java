package com.baijiayi.app.widget;
import com.baijiayi.app.R;
import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.baijiayi.app.AppContext;
import com.baijiayi.app.adapter.PubPhotoListAdapter;
import com.baijiayi.app.bean.PicModel;
import com.baijiayi.app.common.StringUtils;
import com.baijiayi.app.common.UIHelper;

public class GoodsDetailPhotoListLayout extends LinearLayout {

	private PubPhotoListAdapter adapter;
	private List<PicModel> viewList;
	private Context context;
	private AppContext appContext;
	private LayoutInflater listContainer;//视图容器
	
	
	public GoodsDetailPhotoListLayout(Context context, AttributeSet attrs) {
		super(context, attrs);
		this.context=context;
		this.listContainer = LayoutInflater.from(context);	//创建视图容器并设置上下文
		this.setOrientation(HORIZONTAL);
	}
	
	public void setPhotoList(List<PicModel> photoList,AppContext appContext) {
		this.viewList = photoList;
		this.appContext = appContext;
		if(this.viewList==null )
		{
			this.viewList = new ArrayList<PicModel>();
		}
		else
		{
			notifyItemsChanged();
		}
	}
	
	public void notifyItemsChanged()
	{
		for(int i=0,c=viewList.size();i<c;i++){
			
			PicModel photo = viewList.get(i);
			
			View view = listContainer.inflate(R.layout.goods_detail_photo_item,null);
			
			ImageView image=(ImageView)view.findViewById(R.id.photo_image);
			
			//TextView text=(TextView)view.findViewById(R.id.movie_text);
			
			//加载图片
			String imgSmall = photo.getPicUrl();
			image.setTag(photo);
			if(!StringUtils.isEmpty(imgSmall)) {
				UIHelper.showLoadImage(image, imgSmall, null);
				image.setVisibility(View.VISIBLE);
				image.setOnClickListener(new OnClickListener() {
					@Override
					public void onClick(View v) {
						//Toast.makeText(context, "您点击了"+map.get("text"), Toast.LENGTH_SHORT).show();
						PicModel photo=(PicModel)v.getTag();
						if(photo!=null)
						UIHelper.showImageZoomDialog(v.getContext(), photo.getBPicUrl());
					}
				});
			}

			view.setPadding(10, 0, 10, 0);
			
			this.addView(view,0,new LinearLayout.LayoutParams(android.view.ViewGroup.LayoutParams.WRAP_CONTENT,android.view.ViewGroup.LayoutParams.WRAP_CONTENT));
		}
	}
}