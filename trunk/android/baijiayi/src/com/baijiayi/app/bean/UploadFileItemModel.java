package com.baijiayi.app.bean;

import java.io.IOException;

import org.json.JSONObject;

import com.baijiayi.app.AppException;

public class UploadFileItemModel  extends Entity{
    //JSON字段名
    
public final static String NODE_PICID = "PicId";

public final static String NODE_SAVED = "Saved";

public final static String NODE_MSG = "Msg";

public final static String SUB_NODE_PIC = "Pic";

    //对象字段名
    
private int PicId;

private boolean Saved;

private String Msg;

private PicModel Pic;

public PicModel getPic() {
	return Pic;
}

public void setPic(PicModel value) {
	this.Pic = value;
}

    //get,set方法
    
	public int getPicId() {
		return PicId;
	}

	public void setPicId(int value) {
		this.PicId = value;
	}

	public boolean getSaved() {
		return Saved;
	}

	public void setSaved(boolean value) {
		this.Saved = value;
	}

	public String getMsg() {
		return Msg;
	}

	public void setMsg(String value) {
		this.Msg = value;
	}
	
	public static UploadFileItemModel parse(String itemString) throws IOException, AppException {
		JSONObject itemJSON = null;
		try
		{
			itemJSON = new JSONObject(itemString);
		}
		catch(Exception e)
		{}
		return parse(itemJSON);
	}

	public static UploadFileItemModel parse(JSONObject itemJSON) throws IOException, AppException {
		UploadFileItemModel item = null;
		try {
			if(itemJSON != null)
			{
			item = new UploadFileItemModel();
            //解析JSON
            
        item.setPicId(itemJSON.optInt(NODE_PICID));

        item.setSaved(itemJSON.optBoolean(NODE_SAVED));

        item.setMsg(itemJSON.optString(NODE_MSG));
        
        if(item.getSaved())
        {
			JSONObject picJSON = itemJSON.optJSONObject(SUB_NODE_PIC);
			if(picJSON!=null)
			{
				item.setPic(PicModel.parse(picJSON));
			}
        }
			}
			
	    } catch (Exception e) {
			throw AppException.xml(e);
	    }
		return item;
	}
}
        