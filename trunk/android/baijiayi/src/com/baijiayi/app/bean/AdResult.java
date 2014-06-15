package com.baijiayi.app.bean;

import java.io.IOException;

import org.json.JSONObject;

import com.baijiayi.app.AppException;

public class AdResult extends Base {

	public final static String NODE_ERROR = "Error";
	public final static String NODE_ERRORMESSAGE = "ErrorMessage";
	public final static String NODE_EXMESSAGE = "ExMessage";
	public final static String NODE_DATA = "Data";

	private int Error;
	private String ErrorMessage;
	private String ExMessage;

	public void setError(int error) {
		Error = error;
	}

	public void setErrorMessage(String errorMessage) {
		ErrorMessage = errorMessage;
	}

	public void setExMessage(String exMessage) {
		ExMessage = exMessage;
	}

	private JSONObject Data;

	public boolean OK() {
		return Error == 0;
	}

	public int getError() {
		return Error;
	}

	public String getErrorMessage() {
		return ErrorMessage;
	}

	public String getExMessage() {
		return ExMessage;
	}

	public JSONObject getData() {
		return Data;
	}

	public static AdResult parse(String resultStr) throws IOException,
			AppException {
		AdResult result = null;
		try {
			JSONObject resultJSON=new JSONObject(resultStr);
			if (resultJSON != null) {
				result = new AdResult();
				// 解析JSON

				result.Error = resultJSON.optInt(NODE_ERROR);

				result.ErrorMessage = resultJSON.optString(NODE_ERRORMESSAGE);

				result.ExMessage = resultJSON.optString(NODE_EXMESSAGE);

				result.Data = resultJSON.optJSONObject(NODE_DATA);
			}

		} catch (Exception e) {
			throw AppException.xml(e);
		}
		return result;
	}
}
