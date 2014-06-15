package com.baijiayi.app.bean;

import java.io.File;
import java.util.UUID;

import android.graphics.Bitmap;

public class PubPhotoModel extends Entity {
	
	private String filePath;
	private Bitmap bitmap;
	private File file;
	private UUID uuid;
	
	public PubPhotoModel(Bitmap bitmap,File file)
	{
		this.bitmap = bitmap;
		this.file = file;
		filePath = file.getPath();
		uuid = UUID.randomUUID();
	}
	
	public Bitmap getBitmap() {
		return bitmap;
	}
	public void setBitmap(Bitmap bitmap) {
		this.bitmap = bitmap;
	}
	public File getFile() {
		return file;
	}
	public void setFile(File file) {
		this.file = file;
	}
	public UUID getUUID() {
		return uuid;
	}
	
	public boolean equal(PubPhotoModel photo)
	{
		return uuid.compareTo(photo.getUUID()) == 0;
	}
	
}
