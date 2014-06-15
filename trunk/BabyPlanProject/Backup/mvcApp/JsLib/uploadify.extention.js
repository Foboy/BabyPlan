
/*
创 建 人     ： 陈 锐
创 建 时 间  ： 2012.07.24
功 能 说 明  ： 图片上传控件

*/
(function ($) {
    $.fn.cloading=function(options){
        var me = $(this);
        var myop = $.extend({
            width:16,
            height:16,
            img:"/images/loading.gif"
        },options);
        me.css({"position":"relative","overflow":"hidden"});
        var loading = $("<div style='width:"+me.width()+"px;height:"+me.height()+"px;background:url("+myop.img+") center center no-repeat;left:0;top:0;z-index:99;position:absolute;' name='loadingwraper'></div>");
        me.append(loading);
    };
    $.fn.cloaded=function(){
        $(this).find("[name='loadingwraper']").remove();
    };
    $.fn.cupload = function (options) {
        options = $.extend({
            beforeUpload: null,
            uploaded: null,
            dataName: "filedata"
        }, options);
        var me = $(this);
        var mwidth = me.width();
        var mheight = me.height();
        var timer;
        me.css({"position":"relative","overflow":"hidden"});
        var form = $("<form action='"+options.url+"' method='post' enctype='MULTIPART/FORM-DATA'  style='filter:alpha(opacity=0);opacity:0;position:absolute;left:-"+mwidth+"px;top:0;z-index:1'></form>");
        me.append(form);

        var file = $("<input type='file' name='" + options.dataName + "' style='width:" + (mwidth * 2) + "px;height:" + mheight + "px;cursor:pointer;' />");
        form.append(file);
        var oldfile = file.val();
        var nowfile;
        timer = setInterval(function(){
            nowfile = file.val();
            if(oldfile!=nowfile)
            {
                oldfile = nowfile;
                form.submit();
            }
        }, 100);

        var beforeupload = function () {
            var allowload = true;
            if (typeof options.beforeUpload == "function") {
                if (options.beforeUpload.call(me)) {
                    allowload = true;
                } else {
                    allowload = false;
                }
            }
            allowload && me.cloading();
            return allowload;
        }

        var uploaded = function (response) {
            if (typeof options.uploaded == "function") {
                response = $.parseJSON(response);
                options.uploaded.call(me, response);
            }
            me.cloaded();
        }

        var formoptions = {
            dataType: 'text',
            beforeSubmit: beforeupload,
            success: uploaded
        };
        form.ajaxForm(formoptions);
    }
})(jQuery);