package com.baijiayi.app.bean;

import java.io.IOException;
import java.util.Date;


import org.json.JSONObject;

import com.baijiayi.app.AppException;

public class GoodsItemModel extends Entity {
	// JSON字段名
	public final static String NODE_ID = "Id";

	public final static String NODE_PID = "Pid";

	public final static String NODE_PICID = "PicId";

	public final static String NODE_PRICE = "Price";

	public final static String NODE_AGE = "Age";

	public final static String NODE_SEX = "Sex";

	public final static String NODE_ITEMTYPE = "ItemType";

	public final static String NODE_CREATETIME = "CreateTime";
	
	public final static String SUB_NODE_PIC = "Pic";
	
	public final static String SUB_NODE_GOODS = "Product";

	// 对象字段名

	private int Pid;

	private int PicId;

	private double Price;

	private int Age;

	private int Sex;

	private int ItemType;

	private Date CreateTime;
	
	private PicModel Pic;
	
	public PicModel getPic() {
		return Pic;
	}

	public void setPic(PicModel value) {
		this.Pic = value;
	}
	
	private GoodsModel Goods;
	
	public GoodsModel getGoods() {
		return Goods;
	}

	public void setGoods(GoodsModel value) {
		this.Goods = value;
	}

	// get,set方法

	public int getPid() {
		return Pid;
	}

	public void setPid(int value) {
		this.Pid = value;
	}

	public int getPicId() {
		return PicId;
	}

	public void setPicId(int value) {
		this.PicId = value;
	}

	public String getPrice() {
		return "￥"+ Price;
	}

	public void setPrice(double value) {
		this.Price = value;
	}

	public int getAge() {
		return Age;
	}

	public void setAge(int value) {
		this.Age = value;
	}

	public int getSex() {
		return Sex;
	}

	public void setSex(int value) {
		this.Sex = value;
	}

	public int getItemType() {
		return ItemType;
	}

	public void setItemType(int value) {
		this.ItemType = value;
	}

	public Date getCreateTime() {
		return CreateTime;
	}

	public void setCreateTime(String value) {
		value = value.replace("/Date(", "");
		value = value.replace(")/", "");
		this.CreateTime = new Date(Long.parseLong(value));
	}
	
	public static GoodsItemModel parse(String itemResult)throws IOException,
	AppException {
		GoodsItemModel goods = null;
        try { 
        	 JSONObject goodsJSON=new JSONObject(itemResult);
             String msg = goodsJSON.optString("ErrorMessage", "获取数据失败!");
             int success = goodsJSON.optInt("Error", 1);
             if(success != 0)
             {
             	throw new Exception(msg);
             }
             
             JSONObject  itemJSON = goodsJSON.optJSONObject("Data");
             if(itemJSON!=null)
             {
            	 goods= GoodsItemModel.parse(itemJSON);
             }
        } catch (Exception e) {
			throw AppException.xml(e);
        }    
        return goods;   
	}

	public static GoodsItemModel parse(JSONObject itemJSON) throws IOException,
			AppException {
		GoodsItemModel item = null;
		try {
			if (itemJSON != null) {
				item = new GoodsItemModel();
				// 解析JSON

				item.setPid(itemJSON.optInt(NODE_PID));

				item.setPicId(itemJSON.optInt(NODE_PICID));

				item.id=itemJSON.optInt(NODE_ID,0);

				item.setPrice(itemJSON.optDouble(NODE_PRICE));

				item.setAge(itemJSON.optInt(NODE_AGE));

				item.setSex(itemJSON.optInt(NODE_SEX));

				item.setItemType(itemJSON.optInt(NODE_ITEMTYPE));

				item.setCreateTime(itemJSON.optString(NODE_CREATETIME));
				
				JSONObject picJSON = itemJSON.optJSONObject(SUB_NODE_PIC);
				if(picJSON!=null)
				{
					item.setPic(PicModel.parse(picJSON));
				}
				JSONObject goodsJSON = itemJSON.optJSONObject(SUB_NODE_GOODS);
				if(goodsJSON!=null)
				{
					item.setGoods(GoodsModel.parse(goodsJSON));
				}
			}

		} catch (Exception e) {
			throw AppException.xml(e);
		}
		return item;
	}
}
