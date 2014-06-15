package com.baijiayi.app.widget;
import com.baijiayi.app.R;
import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;

import com.baijiayi.app.AppContext;
import com.baijiayi.app.adapter.PubPhotoListAdapter;
import com.baijiayi.app.bean.PubPhotoList;
import com.baijiayi.app.bean.PubPhotoModel;

public class PubPhotoListLayout extends LinearLayout {

	private PubPhotoListAdapter adapter;
	private PubPhotoList photoList;
	private List<PhotoView> viewList = new ArrayList<PhotoView>();
	private Context context;
	private AppContext appContext;
	private LayoutInflater listContainer;//视图容器
	
	
	public PubPhotoListLayout(Context context, AttributeSet attrs) {
		super(context, attrs);
		this.context=context;
		this.listContainer = LayoutInflater.from(context);	//创建视图容器并设置上下文
		this.setOrientation(HORIZONTAL);
	}
	
	public void setPhotoList(PubPhotoList photoList,AppContext appContext) {
		this.photoList = photoList;
		this.appContext = appContext;
	}
	
	public void notifyItemsChanged()
	{
		for(int i=0,c=photoList.getPhotolist().size();i<c;i++){
			
			PubPhotoModel photo=photoList.getPhotolist().get(i);
			boolean showed = false;
			for(PhotoView pv:viewList)
			{
				if(pv.getPhoto().equal(photo))
				{
					showed = true;
					break;
				}
			}
			if(showed)
				continue;
			View view = listContainer.inflate(R.layout.goods_pub_photo_item,null);
			
			ImageView image=(ImageView)view.findViewById(R.id.photo_image);
			
			ImageButton removeBtn = (ImageButton)view.findViewById(R.id.photo_remove_button);
			
			//TextView text=(TextView)view.findViewById(R.id.movie_text);
			
			image.setImageBitmap(photo.getBitmap());

			view.setPadding(10, 0, 10, 0);
			
			removeBtn.setTag(photo);
			
			removeBtn.setOnClickListener(new OnClickListener() {
				@Override
				public void onClick(View v) {
					//Toast.makeText(context, "您点击了"+map.get("text"), Toast.LENGTH_SHORT).show();
					PubPhotoModel photo=(PubPhotoModel)v.getTag();
					photoList.getPhotolist().remove(photo);
					appContext.saveObject(photoList, PubPhotoList.pubPhotoKey);
					for(PhotoView pv:viewList)
					{
						if(pv.getPhoto().equal(photo))
						{
							pv.getView().setVisibility(View.GONE);
							break;
						}
					}
				}
			});
			viewList.add(new PhotoView(photo,view));
			this.addView(view,0,new LinearLayout.LayoutParams(android.view.ViewGroup.LayoutParams.WRAP_CONTENT,android.view.ViewGroup.LayoutParams.WRAP_CONTENT));
		}
	}
	class PhotoView
	{
		private View view;
		private PubPhotoModel photo;
		
		public PhotoView(PubPhotoModel photo,View view)
		{
			this.setPhoto(photo);
			this.setView(view);
		}
		
		public View getView() {
			return view;
		}
		public void setView(View view) {
			this.view = view;
		}
		public PubPhotoModel getPhoto() {
			return photo;
		}
		public void setPhoto(PubPhotoModel photo) {
			this.photo = photo;
		}
		
	}
}