
var timer;
var titleInter;
var flushTime;
var isStart = true;

$(function () {


    $("#coins,#count,#percent").change(function () {
        GetList();
    });

    $("#flushTime").change(function () {
        clearInterval(timer);
        InitTimer();
    });

    $("#btnStop").click(function () {

        if ($(this).val() == '暂停') {
            isStart = false;
            layermsg("已暂停");
            $("#btnStop").val("开始");
            clearInterval(timer);
            clearInterval(titleInter);
            document.title = "首页数据";
        } else {
            isStart = true;
            layermsg("已开始");
            $("#btnStop").val("暂停");
            InitTimer();
        }
    });

    $("#btnFlash").click(function () {
        window.location.href = window.location.href;
    });

    InitTimer();
});

//初始化涮新时间
function InitTimer() {
    GetList();
    flushTime = parseInt($("#flushTime").val());
    timer = setInterval(function () {
        GetList();
    }, flushTime);
}

//获取列表
function GetList() {
    i = 0;
    name = '';
    backColor = '';
    type = 1;
    if (!isStart) {
        return;
    }
    $.ajax({
        url: "/Home/GetCoinList/",
        type: "POST",
        data: {
            coins: $("#coins").val(),
            count: $("#count").val(),
            percent: $("#percent").val()
        },
        beforeSend: function () {
            layerLoading();
        },
        success: function (data) {
            layerLoading({ close: true });
            var html = '';
            $(data.Data).each(function (i, item) {
                html += _.template($("#tempList").html())({
                    data: item
                });
            });
            if (html == '') {
                //清除内部时钟
                clearInterval(titleInter);
                document.title = "首页数据";
                $("#summry").text("");
                $("#tablist").html("<tr><td colspan='15' class='red'>暂无数据</td></tr>");
                return;
            }
            $("#tablist").html(html);
            $("#summry").text(data.Other);
           // UpdateTitle();
        },
        complete: function () {
            TranleteBack();
        }
    });
}

//修改标题
function UpdateTitle() {
    var count = 0;
    titleInter = setInterval(function () {
        count++;
        document.title = "";
        var title = "您有新的消息";
        var v = (count % 5) == 0 ? 5 : count % 5;
        for (var i = 0; i < v; i++) {
            title = '。' + title;
        }
        document.title = title;

    },3000);
}