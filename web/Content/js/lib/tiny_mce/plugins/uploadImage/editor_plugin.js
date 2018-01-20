function QueryString(fieldName) {
    var urlString = window.location.search;
    if (urlString == null) { return null; }
    var typeQu = fieldName + "=";
    var urlEnd = urlString.indexOf(typeQu);
    if (urlEnd == -1) { return null; }
    var paramsUrl = urlString.substring(urlEnd + typeQu.length);
    var isEnd = paramsUrl.indexOf('&');
    if (isEnd == -1) { return paramsUrl; }
    return paramsUrl.substring(0, isEnd);
}

/**
* $Id: editor_plugin_src.js 201 2007-02-12 15:56:56Z spocke $
*
* @author Moxiecode
* @copyright Copyright ?2004-2008, Moxiecode Systems AB, All rights reserved.
*/
(function() {
    // Load plugin specific language pack
    //tinymce.PluginManager.requireLangPack('uploadImage');

    tinymce.create('tinymce.plugins.uploadImagePlugin', {
        /**
        * Initializes the plugin, this will be executed after the plugin has been created.
        * This call is done before the editor instance has finished it's initialization so use the onInit event
        * of the editor instance to intercept that event.
        *
        * @param {tinymce.Editor} ed Editor instance that the plugin is initialized in.
        * @param {string} url Absolute URL to where the plugin is located.
        */
        init: function(ed, url) {
            // Register the command so that it can be invoked by using tinyMCE.activeEditor.execCommand('mceExample');
            ed.addCommand('mceuploadImage', function() {
                var _selfurl = window.location.toString();
                var uploadUrl ='/Content/js/lib/tiny_mce/plugins/uploadImage/uploadImg.htm?v=3.0'; //上传到本地
                
                var width = 380;
                var height =150;

                if (tinymce.isIE) {
                    leftVal = (screen.width - width) / 2;
                    topVal = (screen.height - height) / 2;
                    var sReturn = window.open(uploadUrl, '_blank', 'width=' + width + ',height=' + height + ',toolbars=0,resizable=1,left=' + leftVal + ',top=' + topVal);
                    if (sReturn == null) {
                        alert("本站弹出窗口被屏蔽,请修改浏览器相关配置!");
                    }
                } else {
                    ed.windowManager.open({
                        file: uploadUrl,
                        width: width,
                        height: height,
                        inline: 1
                    }, {
                        plugin_url: url, // Plugin absolute URL
                        some_custom_arg: 'custom arg' // Custom argument
                    });
                }

            });

            // Register example button
            ed.addButton('uploadImage', {
                title: '上传图片',
                cmd: 'mceuploadImage',
                image: url + '/img/img.gif'
            });

            // Add a node change handler, selects the button in the UI when a image is selected
            ed.onNodeChange.add(function(ed, cm, n) {
                cm.setActive('uploadImage', n.nodeName == 'IMG');
            });
        },

        /**
        * Creates control instances based in the incomming name. This method is normally not
        * needed since the addButton method of the tinymce.Editor class is a more easy way of adding buttons
        * but you sometimes need to create more complex controls like listboxes, split buttons etc then this
        * method can be used to create those.
        *
        * @param {String} n Name of the control to create.
        * @param {tinymce.ControlManager} cm Control manager to use inorder to create new control.
        * @return {tinymce.ui.Control} New control instance or null if no control was created.
        */
        createControl: function(n, cm) {
            return null;
        },

        /**
        * Returns information about the plugin as a name/value array.
        * The current keys are longname, author, authorurl, infourl and version.
        *
        * @return {Object} Name/value array containing information about the plugin.
        */
        getInfo: function() {
            return {
                longname: 'uploadImage plugin',
                author: 'MiCheng',
                authorurl: 'http://www.szhome.com',
                infourl: 'http://www.szhome.com',
                version: "1.0"
            };
        }
    });

    // Register plugin
    tinymce.PluginManager.add('uploadImage', tinymce.plugins.uploadImagePlugin);
})();