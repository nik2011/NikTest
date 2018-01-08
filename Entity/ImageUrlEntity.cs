using System;
namespace SZHome.Entity
{
    /// <summary>
    /// 数据表ImageUrl的实体类
    /// </summary>
    [Serializable]
    public class ImageUrlEntity
    {
        #region Private Parameters
        private int _Id;
        private string _WebUrl;
        private string _Host;
        private string _ImageUrl;
        private bool _IsUser;
        private DateTime _AddDate;
        #endregion

        #region Public Properties

        public int Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        /// <summary>
        /// 网页链接
        /// </summary>
        public string WebUrl
        {
            get { return this._WebUrl; }
            set { this._WebUrl = value; }
        }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host
        {
            get { return this._Host; }
            set { this._Host = value; }
        }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string ImageUrl
        {
            get { return this._ImageUrl; }
            set { this._ImageUrl = value; }
        }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUser
        {
            get { return this._IsUser; }
            set { this._IsUser = value; }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate
        {
            get { return this._AddDate; }
            set { this._AddDate = value; }
        }

        #endregion
    }
}
