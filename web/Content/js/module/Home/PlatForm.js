
var timer;
var titleInter;
var flushTime;
var isStart = true;

$(function () {
    $("#btnSearch").click(function () {
        GetList();
    });

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
            document.title = "平台数据";
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
    i= 0;
    if (!isStart) {
        return;
    }
    var platForms='';
    $("input[name=plat]:checked").each(function () {
        platForms += $(this).val()+",";
    });
    if(platForms!='')
    {
        platForms = platForms.substr(0,platForms.length-1);
    }
    var arr = platForms.split(',');
    if(arr.length!=2)
    {
      layermsg("只能比较两个交易所数据");
      return;
    }
    $.ajax({
        url: "/Home/GetPlatFormList/",
        type: "POST",
        data: {
            platForms: platForms,
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
                document.title = "平台数据";
                $("#tablist").html("<tr><td colspan='15' class='red'>暂无数据</td></tr>");
                return;
            }
            $("#tablist").html(html);
            //UpdateTitle();
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

    }, 3000);
}