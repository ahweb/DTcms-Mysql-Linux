using System;
namespace DTcms.Web.Plugin.Link.Model
{
    /// <summary>
    /// 友情链接:实体类
    /// </summary>
    [Serializable]
    public partial class link
    {
        public link()
        { }
        #region Model
        private int _id;
        private string _site_path = string.Empty;
        private string _title = string.Empty;
        private string _user_name = string.Empty;
        private string _user_tel = string.Empty;
        private string _email = string.Empty;
        private string _site_url = string.Empty;
        private string _img_url = string.Empty;
        private int _is_image = 0;
        private int _sort_id = 99;
        private int _is_red = 0;
        private int _is_lock = 0;
        private DateTime _add_time = DateTime.Now;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 站点目录
        /// </summary>
        public string site_path
        {
            set { _site_path = value; }
            get { return _site_path; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string user_tel
        {
            set { _user_tel = value; }
            get { return _user_tel; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string site_url
        {
            set { _site_url = value; }
            get { return _site_url; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 是否为图片
        /// </summary>
        public int is_image
        {
            set { _is_image = value; }
            get { return _is_image; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int is_red
        {
            set { _is_red = value; }
            get { return _is_red; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion Model

    }
}