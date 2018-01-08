tinyMCE.init({
    language: "zh-cn",
    mode:"exact",// "textareas",
    elements: "Editor_SzhomeEditorBody",
    width: "625px",
   // height:"400px",
    theme: "advanced",
    dialog_type: "modal", //"window",
    accessibility_warnings: false,
    keep_styles: true, //This option enables keeping the current text style when you press enter/return on non-IE browsers. This feature is enabled by default but can be disabled by setting the value to false.
    plugins: "uploadImage,paste,autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist",
    // plugins: "uploadImage",

    theme_advanced_buttons1: "bold,italic,underline,|,image,uploadImage,|,formatselect,fontselect,fontsizeselect,|,justifyleft, justifycenter, justifyright,|,link,unlink",
    theme_advanced_buttons2: "", //
    theme_advanced_buttons3: "",
    theme_advanced_resizing: true,
    theme_advanced_resize_horizontal: false,
    theme_advanced_toolbar_location: "top",
    theme_advanced_toolbar_align: "left",
    theme_advanced_statusbar_location: "bottom",
    theme_advanced_fonts: "宋体=宋体;黑体=黑体;仿宋=仿宋;楷体=楷体;隶书=隶书;幼圆=幼圆;Arial=arial,helvetica,sans-serif;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Tahoma=tahoma,arial,helvetica,sans-serif;Times New Roman=times new roman,times;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats",
    theme_advanced_font_sizes: "12px=12px,13px=13px,14px=14px,15px=15px,16px=16px,4(14pt)=14pt,5(18pt)=18pt",
    forced_root_block: "p",
    convert_fonts_to_spans: true,
    remove_trailing_nbsp: true,
    convert_newlines_to_brs: false,
    force_br_newlines: false,
    force_p_newlines: true,
    remove_linebreaks: true,
    verify_html: true,
 
    relative_urls: false,
    remove_script_host: false,
    paste_auto_cleanup_on_paste: true,
    extended_valid_elements: "pre[name|class],style", //"pre[name|class],style",
    //handle_event_callback: "MCECheckIndent",
    whitespace_elements: "pre,script,textarea", //"span,pre,script,style,textarea",
    cleanup: true,
    setup: function (ed) {
        ed.onInit.add(function () {
            var loadingText = "编辑器加载中...";
            var content = ed.getContent();
            if (content == loadingText || content == '<p>' + loadingText + '</p>') {
                ed.setContent('');
            }
        });
    }
});



function MCECheckIndent(e) {
    if (e.type == 'keydown' && e.keyCode == 9) {
        //tinyMCE.activeEditor.selection.setContent('　　');
        tinyMCE.execCommand('mceInsertContent', false, '　　');
        return false;
    }
}

function ContentToEditor(content) {
    tinyMCE.execCommand('mceSetContent', false, content);
}

function InsertToEditor(content) {
    tinyMCE.execCommand('mceInsertContent', false, content);
}

 