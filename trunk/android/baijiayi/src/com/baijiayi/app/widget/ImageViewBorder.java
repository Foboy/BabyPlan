package com.baijiayi.app.widget;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.util.AttributeSet;
import android.widget.ImageView;

public class ImageViewBorder extends ImageView {

	 private String namespace="http://360baijiayi.com";
	 private int color;
	 
	 public ImageViewBorder(Context context, AttributeSet attrs) {
		 super(context, attrs);
		 String colorAttr = attrs.getAttributeValue(namespace, "BorderColor");
		 if(colorAttr!=null)
		 {
			 color=Color.parseColor(colorAttr);
		 }
		 else
		 {
			 color = Integer.MAX_VALUE;
		 }
	}
	
	  
      /* (non-Javadoc)
       * @see android.widget.ImageView#onDraw(android.graphics.Canvas)
       */
      @Override
      protected void onDraw(Canvas canvas) {
          
          super.onDraw(canvas);  
          //画边框  暂时去除小边框
          if(color!=Integer.MAX_VALUE)
          {
	          Rect rec=canvas.getClipBounds();
	          rec.bottom--;
	          rec.right--;
	          Paint paint=new Paint();
	          paint.setColor(color);
	          paint.setStyle(Paint.Style.STROKE);
	          canvas.drawRect(rec, paint);
          }
      }
}