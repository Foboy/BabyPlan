(function ($) {
    $.fn.cLoading=function(options){
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
    $.fn.cLoaded=function(){
        $(this).find("[name='loadingwraper']").remove();
    };
})(jQuery);