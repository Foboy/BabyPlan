
/*
创 建 人     ： 陈 锐
创 建 时 间  ： 2012.07.24
功 能 说 明  ： 图片上传控件
环境 jquery,cLoading,jquery.form
*/
(function ($) {
    var cUpload = function (element, options) {
        var me = $(element);
        var loadingbox = $(options.loadingbox || me);
        var mwidth = me.width();
        var mheight = me.height();
        var inputSize = Math.ceil(1 + (mwidth * 5 - 85) / 6.5);
        var fontSize = Math.ceil(mwidth / 3);
        var timer;
        me.css({ "position": (me.css("position") == "absolute" ? "absolute" : "relative"), "overflow": "hidden", 'cursor': 'pointer' });
        var form = $("<form action='" + options.url + "' class='ignore' method='post' enctype='MULTIPART/FORM-DATA'  style='filter:alpha(opacity=0);opacity:0;position:absolute;top:0;right:0;cursor:pointer;z-index:1'></form>");
        me.append(form);

        var file = $("<input type='file' class='ignore' name='" + options.dataName + "' size='" + inputSize + "'  title='点击选择图片' />");
        file.css({ 'width': mwidth * 5 + "px", 'height': mheight + "px", 'font-size': fontSize + "px", 'line-height': mheight + "px", 'cursor': 'pointer' });

        form.append(file);
        var oldfile = file.val();
        var nowfile;
        timer = setInterval(function () {
            nowfile = file.val();
            if (oldfile != nowfile) {
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
            allowload && loadingbox.cLoading();
            return allowload;
        }

        var uploaded = function (response) {
            if (typeof options.uploaded == "function") {
                response = $.parseJSON(response);
                options.uploaded.call(me, response);
            }
            loadingbox.cLoaded();
        }

        var formoptions = {
            dataType: 'text',
            beforeSubmit: beforeupload,
            success: uploaded
        };
        if (options.size) {
            formoptions.data = { width: options.size.width, height: options.size.height };
        }
        form.ajaxForm(formoptions);
    };
    $.fn.cUpload = function (options) {
        options = $.extend({
            beforeUpload: null,
            uploaded: null,
            dataName: "filedata",
            loadingbox: null
        }, options);
        $(this).each(function () {
            cUpload(this, options);
        });
    }

})(jQuery);