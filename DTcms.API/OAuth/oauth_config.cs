using System;
namespace DTcms.API.OAuth
{
    /// <summary>
    /// OAuth基本信息
    /// </summary>
    [Serializable]
    public class oauth_config
    {
        public oauth_config()
        { }

        private string _oauth_name = string.Empty;
        private string _oauth_app_id = string.Empty;
        private string _oauth_app_key = string.Empty;
        private string _return_uri = string.Empty;

        /// <summary>
        /// OAuth名称
        /// </summary>
        public string oauth_name
        {
            set { _oauth_name = value; }
            get { return _oauth_name; }
        }

        /// <summary>
        /// APP ID
        /// </summary>
        public string oauth_app_id
        {
            set { _oauth_app_id = value; }
            get { return _oauth_app_id; }
        }

        /// <summary>
        /// APP KEY
        /// </summary>
        public string oauth_app_key
        {
            set { _oauth_app_key = value; }
            get { return _oauth_app_key; }
        }

        /// <summary>
        /// 回传的URL
        /// </summary>
        public string return_uri
        {
            set { _return_uri = value; }
            get { return _return_uri; }
        }

    }
}
