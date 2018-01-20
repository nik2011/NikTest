<%@ Page Language="VB" AutoEventWireup="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传图片到编辑器图片码中转</title>
    <script type="text/javascript" src="tiny_mce_popup.js"></script>
    <script type="text/javascript">
        function QueryString(fieldName) {
            var urlString = document.location.search;
            if (urlString == null) { return null; }
            var typeQu = fieldName + "=";
            var urlEnd = urlString.indexOf(typeQu);
            if (urlEnd == -1) { return null; }
            var paramsUrl = urlString.substring(urlEnd + typeQu.length);
            var isEnd = paramsUrl.indexOf('&');
            if (isEnd == -1) { return paramsUrl; }
            return paramsUrl.substring(0, isEnd);
        }

       
        var html = QueryString("code");
        if (html != null) {
            html = html.replace(/<[^>]+>/g, ""); //去除HTML标签
            html = '<p style="text-align: center;"><img src="' + html + '" alt=""/></p>';
            tinyMCEPopup.editor.execCommand('mceInsertContent', false, html);
            setTimeout("tinyMCEPopup.close();", 100);
        }
    </script>
</head>
<body>
</body>
</html>
