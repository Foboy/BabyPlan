function PicZoomView(width, height) {

    var PicHeight = height, PicWidth = width, DisWidth, DisHeight, DisMargin, DisMarginTop, DisMarginLeft;

    this.DisplaySeting = function (maxWidth, MaxHeight, marginTop, marginLeft) {
        var marginTop = marginTop || 0;
        var marginLeft = marginLeft || 0;
        DisWidth = DisplayWidth(maxWidth, MaxHeight);
        DisHeight = DisplayHeight(maxWidth, MaxHeight);
        DisMargin = DisplayMargin(maxWidth, MaxHeight, marginTop, marginLeft);
        return {
            DisWidth: DisWidth,
            DisHeight: DisHeight,
            DisMargin: DisMargin
        };
    };

    this.DisplayStyle = function (maxWidth, MaxHeight, marginTop, marginLeft) {
        var args = this.DisplaySeting(maxWidth, MaxHeight, marginTop, marginLeft);
        return "width:" + args.DisWidth + "px;height:" + args.DisHeight + "px;margin:" + args.DisMargin;
    };

    function ScaleWidth(height, offset) {
        var offset = offset || 0;
        return Math.floor((height == 0 ? 1 : height) / PicHeight * PicWidth + offset);
    }

    function ScaleHeight(width, offset) {
        var offset = offset || 0;
        return Math.floor((width == 0 ? 1 : width) / PicWidth * PicHeight + offset);
    }

    function DisplayWidth(maxWidth, MaxHeight) {
        var ws = maxWidth / PicWidth;
        var hs = MaxHeight / PicHeight;
        if (ws < hs) {
            return maxWidth;
        }
        else {
            return ScaleWidth(MaxHeight);
        }
    }
    function DisplayHeight(maxWidth, MaxHeight) {
        var ws = maxWidth / PicWidth;
        var hs = MaxHeight / PicHeight;
        if (hs < ws) {
            return MaxHeight;
        }
        else {
            return ScaleHeight(maxWidth);
        }
    }
    function DisplayMargin(maxWidth, MaxHeight, marginTop, marginLeft) {
        var marginTop = marginTop || 0;
        var marginLeft = marginLeft || 0;
        var displayWidth = DisplayWidth(maxWidth, MaxHeight);
        var displayHeight = DisplayHeight(maxWidth, MaxHeight);
        var topMargin = 0;
        var leftMargin = 0;
        if (maxWidth > displayWidth) {
            leftMargin = (maxWidth - displayWidth) / 2;
        }
        if (MaxHeight > displayHeight) {
            topMargin = (MaxHeight - displayHeight) / 2;
        }
        topMargin += marginTop;
        leftMargin += marginLeft;
        DisMarginTop = topMargin;
        DisMarginLeft = leftMargin;
        return topMargin + "px " + leftMargin + "px";
    }
}
(function ($) {
    $.addcTemplateMethod({
        ScalePicWidth: function (picWidth, picHeight, height, offset) {
            return Math.floor((height == 0 ? 1 : height) / picHeight * picWidth + offset);
        },
        ScalePicHeight: function (picWidth, picHeight, width, offset) {
            return Math.floor((width == 0 ? 1 : width) / picWidth * picHeight + offset);
        },
        PicZoom:function(picWidth, picHeight,width,height, offset){
            return new PicZoomView(picWidth,picHeight).DisplayStyle(width,height,offset,offset);
        }
    });
})(jQuery);