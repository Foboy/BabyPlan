package com.baijiayi.app.bean;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.json.JSONObject;

import com.baijiayi.app.AppException;

/**
 * 动弹列表实体类
 * @author liux 
 * @version 1.0
 * @created 2012-3-21
 */
public class GoodsItemList extends Entity{
	
	public final static int CATALOG_ALL = 0;
	public final static int CATALOG_CLOTHES = 1;
	public final static int CATALOG_TOYS = 2;
	public final static int CATALOG_OHERS = 3;
	
	public final static int SEX_ALL=0;
	public final static int SEX_BOY=1;
	public final static int SEX_GIRL=2;

	private int pageSize;
	private int goodsCount;
	private List<GoodsModel> goodslist = new ArrayList<GoodsModel>();
	
	public int getPageSize() {
		return pageSize;
	}
	public int getGoodsCount() {
		return goodsCount;
	}
	public List<GoodsModel> getGoodslist() {
		return goodslist;
	}

	public static GoodsItemList parse(String goodsResult) throws IOException, AppException {
		GoodsItemList goodslist = new GoodsItemList();
        try { 
        	 JSONObject goodsJSON=new JSONObject(goodsResult);
             String msg = goodsJSON.optString("ErrorMessage", "获取数据失败!");
             int success = goodsJSON.optInt("Error", 1);
             if(success != 0)
             {
             	throw new Exception(msg);
             }
             
             org.json.JSONArray  itemsArray = goodsJSON.optJSONArray("Data");
             if(itemsArray!=null)
             {
            	 goodslist.goodsCount=Integer.MAX_VALUE;
            	 goodslist.pageSize = itemsArray.length();
             	for(int i=0,c=itemsArray.length();i<c;i++)
             	{
             		JSONObject item = (JSONObject)itemsArray.get(i);
             		GoodsModel goods = GoodsModel.parse(item);
             		if(goods!=null)
             		{
             			goodslist.getGoodslist().add(goods);
             		}
             	}
             }
        } catch (Exception e) {
			throw AppException.xml(e);
        }    
        return goodslist;       
	}
}
