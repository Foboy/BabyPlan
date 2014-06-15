package com.baijiayi.app.adapter;
import java.util.List;

import android.content.Context;
import android.graphics.BitmapFactory;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.baijiayi.app.R;
import com.baijiayi.app.bean.GoodsModel;
import com.baijiayi.app.bean.PicModel;
import com.baijiayi.app.common.BitmapManager;
import com.baijiayi.app.common.StringUtils;

/**
 * 动弹Adapter类
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class ListViewGoodsAdapter extends BaseAdapter {
	private Context 					context;//运行上下文
	private List<GoodsModel> 		listItems;//数据集合
	private LayoutInflater 				listContainer;//视图容器
	private int 						itemViewResource;//自定义项视图源
	private BitmapManager 				bmpManager;
	static class ListItemView{				//自定义控件集合  
			public ImageView img;  
	        public TextView title;  
		    public TextView date;  
		    public TextView commentCount;
		    public TextView price;
	 }  

	/**
	 * 实例化Adapter
	 * @param context
	 * @param data
	 * @param resource
	 */
	public ListViewGoodsAdapter(Context context, List<GoodsModel> data,int resource) {
		this.context = context;			
		this.listContainer = LayoutInflater.from(context);	//创建视图容器并设置上下文
		this.itemViewResource = resource;
		this.listItems = data;
		this.bmpManager = new BitmapManager(BitmapFactory.decodeResource(context.getResources(), R.drawable.widget_dface_loading));
	}
	
	@Override
	public int getCount() {
		return listItems.size();
	}

	@Override
	public Object getItem(int arg0) {
		return null;
	}

	@Override
	public long getItemId(int arg0) {
		return 0;
	}
	   
	/**
	 * ListView Item设置
	 */
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		//Log.d("method", "getView");
		
		//自定义视图
		ListItemView  listItemView = null;
		
		if (convertView == null) {
			//获取list_item布局文件的视图
			convertView = listContainer.inflate(this.itemViewResource, null);
			
			listItemView = new ListItemView();
			//获取控件对象
			listItemView.img = (ImageView)convertView.findViewById(R.id.goods_listitem_pic);
			listItemView.title = (TextView)convertView.findViewById(R.id.goods_listitem_title);
			listItemView.date= (TextView)convertView.findViewById(R.id.goods_listitem_date);
			listItemView.commentCount= (TextView)convertView.findViewById(R.id.goods_listitem_commentCount);
			listItemView.price= (TextView)convertView.findViewById(R.id.goods_listitem_price);
			
			//设置控件集到convertView
			convertView.setTag(listItemView);
		}else {
			listItemView = (ListItemView)convertView.getTag();
		}
				
		//设置文字和图片
		GoodsModel goods = listItems.get(position);
		listItemView.title.setText(goods.getTitle());
		listItemView.title.setTag(goods);//设置隐藏参数(实体类)
		listItemView.date.setText(StringUtils.friendly_time(goods.getCreateTime()));
		listItemView.commentCount.setText(""+goods.getReplyNum());
		listItemView.price.setText(goods.getPrice());
		
		PicModel pic = goods.getPic();
		String imgURL="";
		if(pic !=null)
			imgURL= pic.getSPicUrl();
		if(imgURL.endsWith("portrait.gif") || StringUtils.isEmpty(imgURL)){
			listItemView.img.setImageResource(R.drawable.ic_menu_picnoshow);
		}else{
			bmpManager.loadBitmap(imgURL, listItemView.img);
		}

		return convertView;
	}
	
}