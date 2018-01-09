﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace SZHome.Common.com.szhome.inside.smsservice {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="sms_wsSoap", Namespace="http://smsservice.inside.szhome.com/")]
    public partial class sms_ws : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SendMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendTemplateMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendMessageMultiOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendMessageValidateCodeOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendValidateVoiceMessageOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendValidateCodeMessage_UcpaasOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendValidateCodeMessage_CCPRestOperationCompleted;
        
        private System.Threading.SendOrPostCallback VerifyValidateCodeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public sms_ws() {
            this.Url = global::SZHome.Common.Properties.Settings.Default.SZHome_Common_com_szhome_inside_smsservice_sms_ws;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SendMessageCompletedEventHandler SendMessageCompleted;
        
        /// <remarks/>
        public event SendTemplateMessageCompletedEventHandler SendTemplateMessageCompleted;
        
        /// <remarks/>
        public event SendMessageMultiCompletedEventHandler SendMessageMultiCompleted;
        
        /// <remarks/>
        public event SendMessageValidateCodeCompletedEventHandler SendMessageValidateCodeCompleted;
        
        /// <remarks/>
        public event SendValidateVoiceMessageCompletedEventHandler SendValidateVoiceMessageCompleted;
        
        /// <remarks/>
        public event SendValidateCodeMessage_UcpaasCompletedEventHandler SendValidateCodeMessage_UcpaasCompleted;
        
        /// <remarks/>
        public event SendValidateCodeMessage_CCPRestCompletedEventHandler SendValidateCodeMessage_CCPRestCompleted;
        
        /// <remarks/>
        public event VerifyValidateCodeCompletedEventHandler VerifyValidateCodeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendMessage", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendMessage(int appId, string token, string phoneNumber, string msgCotent) {
            object[] results = this.Invoke("SendMessage", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        msgCotent});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendMessageAsync(int appId, string token, string phoneNumber, string msgCotent) {
            this.SendMessageAsync(appId, token, phoneNumber, msgCotent, null);
        }
        
        /// <remarks/>
        public void SendMessageAsync(int appId, string token, string phoneNumber, string msgCotent, object userState) {
            if ((this.SendMessageOperationCompleted == null)) {
                this.SendMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageOperationCompleted);
            }
            this.InvokeAsync("SendMessage", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        msgCotent}, this.SendMessageOperationCompleted, userState);
        }
        
        private void OnSendMessageOperationCompleted(object arg) {
            if ((this.SendMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageCompleted(this, new SendMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendTemplateMessage", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendTemplateMessage(int appId, string token, string phoneNumber, string msgCotent) {
            object[] results = this.Invoke("SendTemplateMessage", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        msgCotent});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendTemplateMessageAsync(int appId, string token, string phoneNumber, string msgCotent) {
            this.SendTemplateMessageAsync(appId, token, phoneNumber, msgCotent, null);
        }
        
        /// <remarks/>
        public void SendTemplateMessageAsync(int appId, string token, string phoneNumber, string msgCotent, object userState) {
            if ((this.SendTemplateMessageOperationCompleted == null)) {
                this.SendTemplateMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendTemplateMessageOperationCompleted);
            }
            this.InvokeAsync("SendTemplateMessage", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        msgCotent}, this.SendTemplateMessageOperationCompleted, userState);
        }
        
        private void OnSendTemplateMessageOperationCompleted(object arg) {
            if ((this.SendTemplateMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendTemplateMessageCompleted(this, new SendTemplateMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendMessageMulti", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendMessageMulti(int appId, string token, string phoneNumbers, string msgCotent) {
            object[] results = this.Invoke("SendMessageMulti", new object[] {
                        appId,
                        token,
                        phoneNumbers,
                        msgCotent});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendMessageMultiAsync(int appId, string token, string phoneNumbers, string msgCotent) {
            this.SendMessageMultiAsync(appId, token, phoneNumbers, msgCotent, null);
        }
        
        /// <remarks/>
        public void SendMessageMultiAsync(int appId, string token, string phoneNumbers, string msgCotent, object userState) {
            if ((this.SendMessageMultiOperationCompleted == null)) {
                this.SendMessageMultiOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageMultiOperationCompleted);
            }
            this.InvokeAsync("SendMessageMulti", new object[] {
                        appId,
                        token,
                        phoneNumbers,
                        msgCotent}, this.SendMessageMultiOperationCompleted, userState);
        }
        
        private void OnSendMessageMultiOperationCompleted(object arg) {
            if ((this.SendMessageMultiCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageMultiCompleted(this, new SendMessageMultiCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendMessageValidateCode", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendMessageValidateCode(int appId, string token, string phoneNumber) {
            object[] results = this.Invoke("SendMessageValidateCode", new object[] {
                        appId,
                        token,
                        phoneNumber});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendMessageValidateCodeAsync(int appId, string token, string phoneNumber) {
            this.SendMessageValidateCodeAsync(appId, token, phoneNumber, null);
        }
        
        /// <remarks/>
        public void SendMessageValidateCodeAsync(int appId, string token, string phoneNumber, object userState) {
            if ((this.SendMessageValidateCodeOperationCompleted == null)) {
                this.SendMessageValidateCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageValidateCodeOperationCompleted);
            }
            this.InvokeAsync("SendMessageValidateCode", new object[] {
                        appId,
                        token,
                        phoneNumber}, this.SendMessageValidateCodeOperationCompleted, userState);
        }
        
        private void OnSendMessageValidateCodeOperationCompleted(object arg) {
            if ((this.SendMessageValidateCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageValidateCodeCompleted(this, new SendMessageValidateCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendValidateVoiceMessage", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendValidateVoiceMessage(int appId, string token, string phoneNumber) {
            object[] results = this.Invoke("SendValidateVoiceMessage", new object[] {
                        appId,
                        token,
                        phoneNumber});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendValidateVoiceMessageAsync(int appId, string token, string phoneNumber) {
            this.SendValidateVoiceMessageAsync(appId, token, phoneNumber, null);
        }
        
        /// <remarks/>
        public void SendValidateVoiceMessageAsync(int appId, string token, string phoneNumber, object userState) {
            if ((this.SendValidateVoiceMessageOperationCompleted == null)) {
                this.SendValidateVoiceMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendValidateVoiceMessageOperationCompleted);
            }
            this.InvokeAsync("SendValidateVoiceMessage", new object[] {
                        appId,
                        token,
                        phoneNumber}, this.SendValidateVoiceMessageOperationCompleted, userState);
        }
        
        private void OnSendValidateVoiceMessageOperationCompleted(object arg) {
            if ((this.SendValidateVoiceMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendValidateVoiceMessageCompleted(this, new SendValidateVoiceMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendValidateCodeMessage_Ucpaas", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendValidateCodeMessage_Ucpaas(int appId, string token, string phoneNumber) {
            object[] results = this.Invoke("SendValidateCodeMessage_Ucpaas", new object[] {
                        appId,
                        token,
                        phoneNumber});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendValidateCodeMessage_UcpaasAsync(int appId, string token, string phoneNumber) {
            this.SendValidateCodeMessage_UcpaasAsync(appId, token, phoneNumber, null);
        }
        
        /// <remarks/>
        public void SendValidateCodeMessage_UcpaasAsync(int appId, string token, string phoneNumber, object userState) {
            if ((this.SendValidateCodeMessage_UcpaasOperationCompleted == null)) {
                this.SendValidateCodeMessage_UcpaasOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendValidateCodeMessage_UcpaasOperationCompleted);
            }
            this.InvokeAsync("SendValidateCodeMessage_Ucpaas", new object[] {
                        appId,
                        token,
                        phoneNumber}, this.SendValidateCodeMessage_UcpaasOperationCompleted, userState);
        }
        
        private void OnSendValidateCodeMessage_UcpaasOperationCompleted(object arg) {
            if ((this.SendValidateCodeMessage_UcpaasCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendValidateCodeMessage_UcpaasCompleted(this, new SendValidateCodeMessage_UcpaasCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/SendValidateCodeMessage_CCPRest", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SendValidateCodeMessage_CCPRest(int appId, string token, string phoneNumber) {
            object[] results = this.Invoke("SendValidateCodeMessage_CCPRest", new object[] {
                        appId,
                        token,
                        phoneNumber});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SendValidateCodeMessage_CCPRestAsync(int appId, string token, string phoneNumber) {
            this.SendValidateCodeMessage_CCPRestAsync(appId, token, phoneNumber, null);
        }
        
        /// <remarks/>
        public void SendValidateCodeMessage_CCPRestAsync(int appId, string token, string phoneNumber, object userState) {
            if ((this.SendValidateCodeMessage_CCPRestOperationCompleted == null)) {
                this.SendValidateCodeMessage_CCPRestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendValidateCodeMessage_CCPRestOperationCompleted);
            }
            this.InvokeAsync("SendValidateCodeMessage_CCPRest", new object[] {
                        appId,
                        token,
                        phoneNumber}, this.SendValidateCodeMessage_CCPRestOperationCompleted, userState);
        }
        
        private void OnSendValidateCodeMessage_CCPRestOperationCompleted(object arg) {
            if ((this.SendValidateCodeMessage_CCPRestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendValidateCodeMessage_CCPRestCompleted(this, new SendValidateCodeMessage_CCPRestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://smsservice.inside.szhome.com/VerifyValidateCode", RequestNamespace="http://smsservice.inside.szhome.com/", ResponseNamespace="http://smsservice.inside.szhome.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int VerifyValidateCode(int appId, string token, string phoneNumber, string validateCode) {
            object[] results = this.Invoke("VerifyValidateCode", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        validateCode});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void VerifyValidateCodeAsync(int appId, string token, string phoneNumber, string validateCode) {
            this.VerifyValidateCodeAsync(appId, token, phoneNumber, validateCode, null);
        }
        
        /// <remarks/>
        public void VerifyValidateCodeAsync(int appId, string token, string phoneNumber, string validateCode, object userState) {
            if ((this.VerifyValidateCodeOperationCompleted == null)) {
                this.VerifyValidateCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnVerifyValidateCodeOperationCompleted);
            }
            this.InvokeAsync("VerifyValidateCode", new object[] {
                        appId,
                        token,
                        phoneNumber,
                        validateCode}, this.VerifyValidateCodeOperationCompleted, userState);
        }
        
        private void OnVerifyValidateCodeOperationCompleted(object arg) {
            if ((this.VerifyValidateCodeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.VerifyValidateCodeCompleted(this, new VerifyValidateCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendMessageCompletedEventHandler(object sender, SendMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendTemplateMessageCompletedEventHandler(object sender, SendTemplateMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendTemplateMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendTemplateMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendMessageMultiCompletedEventHandler(object sender, SendMessageMultiCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageMultiCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendMessageMultiCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendMessageValidateCodeCompletedEventHandler(object sender, SendMessageValidateCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageValidateCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendMessageValidateCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendValidateVoiceMessageCompletedEventHandler(object sender, SendValidateVoiceMessageCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendValidateVoiceMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendValidateVoiceMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendValidateCodeMessage_UcpaasCompletedEventHandler(object sender, SendValidateCodeMessage_UcpaasCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendValidateCodeMessage_UcpaasCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendValidateCodeMessage_UcpaasCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SendValidateCodeMessage_CCPRestCompletedEventHandler(object sender, SendValidateCodeMessage_CCPRestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendValidateCodeMessage_CCPRestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendValidateCodeMessage_CCPRestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void VerifyValidateCodeCompletedEventHandler(object sender, VerifyValidateCodeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class VerifyValidateCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal VerifyValidateCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591