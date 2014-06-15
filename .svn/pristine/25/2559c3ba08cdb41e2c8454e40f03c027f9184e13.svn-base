package com.baijiayi.app.bean;

import java.io.IOException;
import java.util.Date;

import org.json.JSONObject;

import com.baijiayi.app.AppException;

public class CommentModel extends Entity {
	// JSON字段名

	public final static String NODE_ID = "Id";

	public final static String NODE_PID = "Pid";

	public final static String NODE_CONTENT = "Content";

	public final static String NODE_CREATETIME = "CreateTime";
	
	public final static String SUB_NODE_AUTHOR = "Author";
	
	public final static String SUB_NODE_REFCOMMENT = "RefReply";
	
	public final static String SUB_NODE_GOODS = "Product";
	// 对象字段名

	private int Pid;

	private String Content;

	private Date CreateTime;
	
	private UserModel Author;
	
	public UserModel getAuthor() {
		return Author;
	}

	public void setAuthor(UserModel value) {
		this.Author = value;
	}
	
	private GoodsModel Goods;
	
	public GoodsModel getGoods() {
		return Goods;
	}

	public void setGoods(GoodsModel value) {
		this.Goods = value;
	}
	
	private CommentModel RefComment;
	
	public CommentModel getRefComment() {
		return RefComment;
	}

	public void setRefComment(CommentModel value) {
		this.RefComment = value;
	}

	// get,set方法

	public int getPid() {
		return Pid;
	}

	public void setPid(int value) {
		this.Pid = value;
	}

	public String getContent() {
		return Content;
	}

	public void setContent(String value) {
		this.Content = value;
	}

	public Date getCreateTime() {
		return CreateTime;
	}

	public void setCreateTime(String value) {
		value = value.replace("/Date(", "");
		value = value.replace(")/", "");
		this.CreateTime = new Date(Long.parseLong(value));
	}

	public static CommentModel parse(JSONObject itemJSON) throws IOException,
			AppException {
		CommentModel item = null;
		try {
			if (itemJSON != null) {
				item = new CommentModel();
				// 解析JSON

				item.id=itemJSON.optInt(NODE_ID,0);

				item.setPid(itemJSON.optInt(NODE_PID));

				item.setContent(itemJSON.optString(NODE_CONTENT));

				item.setCreateTime(itemJSON.optString(NODE_CREATETIME));
				
				JSONObject authorJSON = itemJSON.optJSONObject(SUB_NODE_AUTHOR);
				if(authorJSON!=null)
				{
					item.setAuthor(UserModel.parse(authorJSON));
				}
				
				JSONObject refCommentJSON = itemJSON.optJSONObject(SUB_NODE_REFCOMMENT);
				if(refCommentJSON!=null)
				{
					item.setRefComment(CommentModel.parse(refCommentJSON));
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
