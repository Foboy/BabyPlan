package com.baijiayi.app.adapter;
import com.baijiayi.app.R;
import java.util.List;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;

import com.baijiayi.app.bean.PubPhotoModel;

public class PubPhotoListAdapter extends BaseAdapter {

	private Context context;
	private List<PubPhotoModel> photoList;
	private LayoutInflater listContainer;//视图容器
	
	public PubPhotoListAdapter(Context context,List<PubPhotoModel> photoList){
		this.context=context;
		this.listContainer = LayoutInflater.from(context);	//创建视图容器并设置上下文
		this.photoList = photoList;
	}

	@Override
	public int getCount() {
		return photoList.size();
	}


	@Override
	public PubPhotoModel getItem(int location) {
		return photoList.get(location);
	}

	@Override
	public long getItemId(int arg0) {
		return arg0;
	}

	@Override
	public View getView(int location, View arg1, ViewGroup arg2) {
		View view = listContainer.inflate(R.layout.goods_pub_photo_item,null);
		ImageView image=(ImageView)view.findViewById(R.id.photo_image);
		//TextView text=(TextView)view.findViewById(R.id.movie_text);
		PubPhotoModel photo=getItem(location);
		image.setImageBitmap(photo.getBitmap());
		//text.setText(map.get("text").toString());
		return view;
	}

}