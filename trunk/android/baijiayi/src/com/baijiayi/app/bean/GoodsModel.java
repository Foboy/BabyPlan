package com.baijiayi.app.bean;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONObject;

import com.baijiayi.app.AppException;

public class GoodsModel extends Entity {
	// JSON字段名

	public final static String NODE_ID = "Id";

	public final static String NODE_TITLE = "Title";

	public final static String NODE_QQ = "QQ";

	public final static String NODE_PHONE = "Phone";
	
	public final static String NODE_PRICE = "Price";

	public final static String NODE_AGE = "Age";

	public final static String NODE_SEX = "Sex";

	public final static String NODE_ITEMTYPE = "ItemType";

	public final static String NODE_DESCRIPTION = "Description";

	public final static String NODE_ISWASH = "IsWash";

	public final static String NODE_CREATETIME = "CreateTime";

	public final static String NODE_STATE = "State";

	public final static String NODE_REPLYNUM = "ReplyNum";

	public final static String NODE_VIEWNUM = "ViewNum";
	
	public final static String SUB_NODE_AUTHOR = "Author";
	
	public final static String SUB_NODE_PIC = "Pic";
	
	public final static String SUB_NODE_PICS = "Pics";

	// 对象字段名

	private String Title;

	private String QQ;

	private String Phone;
	
	private double Price;

	private int Age;

	private int Sex;

	private int ItemType;

	private String Description;

	private boolean IsWash;

	private Date CreateTime;

	private int State;

	private int ReplyNum;

	private int ViewNum;
	
	private UserModel Author;
	
	private List<PicModel> Pics;
	
	public UserModel getAuthor() {
		return Author;
	}

	public void setAuthor(UserModel value) {
		this.Author = value;
	}
	
	private PicModel Pic;
	
	public PicModel getPic() {
		return Pic;
	}

	public void setPic(PicModel value) {
		this.Pic = value;
	}
	
	public List<PicModel> getPics() {
		if(Pics == null)
		{
			Pics = new ArrayList<PicModel>();
		}
		return Pics;
	}

	// get,set方法

	public String getTitle() {
		return Title;
	}

	public void setTitle(String value) {
		this.Title = value;
	}

	public String getQQ() {
		return QQ;
	}

	public void setQQ(String value) {
		this.QQ = value;
	}

	public String getPhone() {
		return Phone;
	}

	public void setPhone(String value) {
		this.Phone = value;
	}
	
	public String getPrice() {
		return "￥"+ Price;
	}
	
	public double getDoublePrice() {
		return Price;
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

	public String getDescription() {
		return Description;
	}

	public void setDescription(String value) {
		this.Description = value;
	}

	public boolean getIsWash() {
		return IsWash;
	}

	public void setIsWash(boolean value) {
		this.IsWash = value;
	}

	public Date getCreateTime() {
		return CreateTime;
	}

	public void setCreateTime(String value) {
		value = value.replace("/Date(", "");
		value = value.replace(")/", "");
		this.CreateTime = new Date(Long.parseLong(value));
	}

	public int getState() {
		return State;
	}

	public void setState(int value) {
		this.State = value;
	}

	public int getReplyNum() {
		return ReplyNum;
	}

	public void setReplyNum(int value) {
		this.ReplyNum = value;
	}

	public int getViewNum() {
		return ViewNum;
	}

	public void setViewNum(int value) {
		this.ViewNum = value;
	}
	
	public static GoodsModel parse(String itemResult)throws IOException,
	AppException {
		GoodsModel goods = null;
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
            	 goods= GoodsModel.parse(itemJSON);
             }
        } catch (Exception e) {
			throw AppException.xml(e);
        }    
        return goods;   
	}

	public static GoodsModel parse(JSONObject itemJSON) throws IOException,
			AppException {
		GoodsModel item = null;
		try {
			if (itemJSON != null) {
				item = new GoodsModel();
				// 解析JSON

				item.id =itemJSON.optInt(NODE_ID,0);

				item.setTitle(itemJSON.optString(NODE_TITLE));

				item.setQQ(itemJSON.optString(NODE_QQ));

				item.setPhone(itemJSON.optString(NODE_PHONE));
				
				item.setPrice(itemJSON.optDouble(NODE_PRICE));

				item.setAge(itemJSON.optInt(NODE_AGE));

				item.setSex(itemJSON.optInt(NODE_SEX));

				item.setItemType(itemJSON.optInt(NODE_ITEMTYPE));

				item.setDescription(itemJSON.optString(NODE_DESCRIPTION));

				item.setIsWash(itemJSON.optBoolean(NODE_ISWASH));

				item.setCreateTime(itemJSON.optString(NODE_CREATETIME));

				item.setState(itemJSON.optInt(NODE_STATE));

				item.setReplyNum(itemJSON.optInt(NODE_REPLYNUM));

				item.setViewNum(itemJSON.optInt(NODE_VIEWNUM));

				JSONObject authorJSON = itemJSON.optJSONObject(SUB_NODE_AUTHOR);
				if(authorJSON!=null)
				{
					item.setAuthor(UserModel.parse(authorJSON));
				}
				
				JSONArray picsJSON = itemJSON.optJSONArray(SUB_NODE_PICS);
				if(picsJSON!=null)
				{
	             	for(int i=0,c=picsJSON.length();i<c;i++)
	             	{
	             		JSONObject pitem = (JSONObject)picsJSON.get(i);
	             		PicModel pic = PicModel.parse(pitem);
	             		if(pic!=null)
	             		{
	             			item.getPics().add(pic);
	             		}
	             	}
				}
				
				if(item.getPics().size()>0)
					item.setPic(item.getPics().get(0));
				
				JSONObject picJSON = itemJSON.optJSONObject(SUB_NODE_PIC);
				if(picJSON!=null)
				{
					item.setPic(PicModel.parse(picJSON));
				}
			}

		} catch (Exception e) {
			throw AppException.xml(e);
		}
		return item;
	}
}
