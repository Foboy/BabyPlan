/**
* @author cray
*/
var cvalid = cvalid || {};
(function ($) {
    $.fn.ShowErrorTag = function () {
        var parent = $(this).parent();
        var limit = 2;
        while (limit > 0) {
            limit--;
            var target = parent.find("[name='errortag']");
            if (target.length > 0) {
                target.html("*");
                return;
            }
            parent = parent.parent();
        }
    };
    $.fn.HideErrorTag = function () {
        var parent = $(this).parent();
        var limit = 2;
        while (limit > 0) {
            limit--;
            var target = parent.find("[name='errortag']");
            if (target.length > 0) {
                target.html("&nbsp;");
                return;
            }
            parent = parent.parent();
        }
    };

    $.fn.AutoValidation = function () {
        var funcs = new Array();
        var me = this;
        var checker = function () {
            var pos = $(me).position();
            var msg;
            if (pos.left != 0 || pos.top != 0) {
                for (var i = 0; i < funcs.length; i++) {
                    var result = funcs[i].call(me);
                    if (result && result.valid) {
                        continue;
                    }
                    else {
                        $(me).ShowErrorTag();
                        return $.extend(result, { valid: false, elt: me });
                    }
                }
            }
            else {
                msg = "元素可能已被移除，不予验证!";
            }
            $(me).HideErrorTag();
            return { valid: true, elt: me, msg: msg };
        };
        for (var i = 0; i < arguments.length; i++) {
            var arg = arguments[i];
            if (typeof arg == "string") {
                $(this).bind(arg, checker);
            } else if (typeof arg == "function") {
                funcs.push(arg);
            }
        }
        var allchecker = $.data(document.body, "cray_validations");
        if (allchecker) {
            allchecker.push(checker);
        }
        else {
            allchecker = new Array();
            allchecker.push(checker);
            $.data(document.body, "cray_validations", allchecker);
        }
        $.data(this, "cray_validation", checker);
    };

    $.fn.ValidationSelf = function () {
        var checker = $.data(this, "cray_validation");
        if (checker && typeof checker == "function") {
            return checker();
        } else {
            return { valid: false, msg: "未绑定验证规则!", elt: this };
        }
    };
    //验证所有注册项
    cvalid.ValidationAll = function () {
        var valid = true;
        var valids = new Array();
        var avalids = new Array();
        var checkers = $.data(document.body, "cray_validations");
        if (checkers && !isNaN(checkers.length)) {
            for (var i = 0; i < checkers.length; i++) {
                if (typeof checkers[i] == "function") {
                    var result = checkers[i]();
                    if (result && result.valid) {
                        valids.push(result);
                    }
                    else {
                        avalids.push(result);
                        valid = false;
                    }
                }
            }
        }
        return { valid: valid, valids: valids, avalids: avalids };
    };
    //生成验证返回结果，避免写错
    cvalid.CreateVR = function (valid, msg, elt) {
        return { valid: valid, msg: msg, elt: elt };
    };
    cvalid.numeric = function () {
        return function () {
        var val = $(this).val();
        var valid = /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(val);
        return cvalid.CreateVR(valid,"只能填写数字！");
    };
};
cvalid.required = function (ignore) {
    return function () {
        var val = $.trim($(this).val());
        var valid = (val.length > 0);
        if(ignore && !isNaN(ignore.length))
        {
            for(var i=0;i<ignore.length;i++)
            {
                if(val == ignore[i])
                {
                    valid = false;
                    break;
                }
            }
        }
        return cvalid.CreateVR(valid,"此项为必填项目！");
    };
}
cvalid.minlength = function (minlength) {
    return function () {
        var val = $.trim($(this).val());
        var valid = (val.length > minlength);
        return cvalid.CreateVR(valid);
    };
}
cvalid.maxlength = function (maxlength) {
    return function () {
        var val = $.trim($(this).val());
        var valid = (val.length < maxlength);
        return cvalid.CreateVR(valid);
    };
};

})(jQuery); 