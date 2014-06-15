(function ($) {
    $.showMsg = function (msg, type, options) {
        //type: warn,error,success,none
        options = $.extend({
            closeTimeout: 3000,
            onClosed: function () { }
        }, options);
        var link = $("a#fancyboxlink");
        var msgboxspan = $("#fancybox_msgbox_span");
        if (!msgboxspan.length) {
            $(document.body).append("<div style='display:none;'>\
                         <div class='msgbox' id='fancybox_msgbox'>\
                             <span class='msgbg msgbox_layer'>\
                               <span class='msgbg gtl_ico_succ' id='fancybox_msgbox_icon'></span><span id='fancybox_msgbox_span'>保存成功</span><span class='msgbg gtl_end'></span>\
                             </span>\
                         </div>\
                     </div>");
            msgboxspan = $("#fancybox_msgbox_span");
        }
        var msgicon = $("#fancybox_msgbox_icon");
        msgicon.removeClass();
        msgicon.addClass("msgbg");
        switch (type) {
            case "s":
            case "succ":
            case "success":
                msgicon.addClass("gtl_ico_succ");
                break;
            case "w":
            case "warn":
                msgicon.addClass("gtl_ico_warn");
                break;
            case "e":
            case "error":
                msgicon.addClass("gtl_ico_error");
                break;
        }

        if (!link.length) {
            link = $("<a id='fancyboxlink'></a>");
            $(document.body).append(link);
        }
        link.fancybox({
            'showCloseButton': false,
            'overlayShow': false,
            'transitionOut': true,
            'hideOnOverlayClick': true,
            'hideOnContentClick': true
        });
        msgboxspan.html(msg);
        link.attr("href", "#fancybox_msgbox").click();
        if (options.closeTimeout > 0) {
            setTimeout(function () {
                $.fancybox.close();
                if (typeof options.onClosed == "function") {
                    options.onClosed();
                }
            }, options.closeTimeout);
        }
    };
    $.showConfirmMsg = function (msg, callback, options) {
        //callback: 参数为bool类型
        options = $.extend({
            onClosed: function () { }
        }, options);
        var link = $("a#fancyboxlink");
        var msgboxspan = $("#fancybox_confirmmsgbox_span");
        if (!msgboxspan.length) {
            $(document.body).append("<div style='display:none;'>\
                         <div class='fancybox-popup' id='fancybox_confirmmsgbox'>\
	<p class='textDes' id='fancybox_confirmmsgbox_span'>成长录</p>\
    <div class='popupBtn'>\
        <a class='button-green' id='confirm_true' href='javascript:void(0);' >确定</a>\
        <a class='button-green' id='confirm_false' href='javascript:void(0);'>取消</a>\
    </div>\
</div>\
                     </div>");
            msgboxspan = $("#fancybox_confirmmsgbox_span");
        }
      
        $("#confirm_true").bind("click", function () {
                  callback(true);
                  $.fancybox.close();

            });
        $("#confirm_false").bind("click", function () {
                 callback(false);
                 $.fancybox.close();
            });

        if (!link.length) {
            link = $("<a id='fancyboxlink'></a>");
            $(document.body).append(link);
        }
        link.fancybox({
            'showCloseButton': false,
            'overlayShow': false,
            'transitionOut': true,
            'hideOnOverlayClick': true,
            'hideOnContentClick': true
        });
        msgboxspan.html(msg);
        link.attr("href", "#fancybox_confirmmsgbox").click();
      
    };

})(jQuery);
