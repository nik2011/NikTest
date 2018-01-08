
//把string转化为json数据
function JsonEval(jsonObj) {
    var str = "";
    try {
        str = eval(jsonObj);
    } catch (err) {
        str = eval("(" + jsonObj + ")");
    }
    return str;
}

//切换背景颜色
function TranleteBack() {
    $("#tablist tr").each(function (back) {
        //alert(1);
        $(this).mouseover(function () {
            // alert($(this));
            back = $(this).css("background-color");
            $(this).css("background-color", "#ffdddd");
        });
        $(this).mouseout(function () {
            $(this).css("background-color", back);
        });
    });
}

//先提示后跳转
//content为提示的内容，callback为回调函数(可选)，time为等待多久跳转（默认2s）
function layermsg(content, callback, time) {
    if (typeof (time) == 'undefined' || time == "") {
        time = 2000;
    }
    if (typeof (callback) == 'undefined' || time == null) {
        layer.msg(content, {
            // icon: type,
            time: time //几秒关闭
        });
    }
    else {
        layer.msg(content, {
            // icon: type,
            time: time //几秒关闭
        }, callback);
    }
}

//警告层
function layerAlert(message) {
    layer.alert(message, { icon: 5 });
}

//询问层
function layerConfirm(title, btn1, btn2, event1, event2) {
    if (btn1 == null) {
        btn1 = "是";
    }
    if (btn2 == null) {
        btn2 = "否";
    }
    //询问框
    layer.confirm(title, {
        btn: [btn1, btn2] //按钮
    }, event1, event2);
}

//加载函数，可以扩展，默认调layer插件
function layerLoading(vOptions) {
    var options = {
        type: 0
    };
    if (typeof vOptions == 'object') {
        options = $.extend(options, vOptions);
    }
    //调用layer插件，使用:layerLoading();layerLoading(1);取消:layerLoading({ close:true });
    if (options.type == 0) {
        if (options.close === true) {
            layer.closeAll();
        }
        else {
            layer.load(vOptions);
        }
    }
        //背景图片实现，使用:layerLoading({ type: 1, target: "#myAttnList" });取消:layerLoading({ type: 1, target: "#myAttnList", close:true });
    else if (options.type == 1) {
        var $target;
        if (options.target) {
            $target = $(options.target);
            if (options.close === true) {
                $target.removeClass("loading");
            }
            else {
                $target.addClass("loading");
            }
        }
    }
        //文字实现，使用:layerLoading({ type: 2, target: ".bbstags" });取消:layerLoading({ type: 2, target: ".bbstags", close:true });
    else if (options.type == 2) { //文字实现
        var $target;
        if (options.target) {
            $target = $(options.target);
            if (options.close === true) {
                $target.html("");
            }
            else {
                $target.html("加载中...");
            }
        }
    }
}


//截取指定长度的字符串
function subStr(input, num) {
    if (input == "" || input.length <= num) {
        return input;
    }
    var subString = input.substring(0, num) + "...";
    return subString;
}

//匹配第一个数字
function MatchFirstNum(input)
{
    var output = '';
    if (input == '')
    {
        return output;
    }
    var reg = new RegExp("[0-9]+", "g");
    var arr = input.match(reg);
   
    if (arr != null && arr.length > 0) {
        output = arr[0];
    }
    return output;
}

//验证金额
function ValidatePrice(price) {
    var reg = new RegExp("^([1-9][\\d]{0,7}|0)(\.[\\d]{1,2})?$");

    if (!reg.test(price)) {
        return false;
    }
    return true;
}

//验证数字
function ValidateNum(num) {
    var reg = new RegExp("^[0-9]+$");
    if (reg.test(num)) {
        return true;
    }
    return false;
}

//验证手机号
function ValidatePhone(num) {
    var reg = new RegExp("^1[0-9]{10}+$");
    if (reg.test(num)) {
        return true;
    }
    return false;
}

//显示分页其他项 如：总数 共多少页
function ShowOtherPageItem(recordCount, pageSize)
{
    $("#CurrentCount").text($("#hidCurrentCount").val());
    $("#recordCount").text(recordCount);
    $("#TotalPage").text($("#hidTotalPage").val());
    $("#PageSize").text(pageSize);
}

// 对Date的扩展，将 Date 转化为指定格式的String 
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

String.prototype.Convert2DateTime = function () {
    ///Date(1431100800000)/
    var regex = /\/Date\((\d+)\)\//;
    var str = this.replace(regex, "$1");
    return new Date(parseInt(str)).Format("yyyy-MM-dd hh:mm:ss");
}