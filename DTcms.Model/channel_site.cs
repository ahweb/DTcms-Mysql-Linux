using System;
namespace DTcms.Model
{
    /// <summary>
    /// 频道分类
    /// </summary>
    [Serializable]
    public partial class channel_site
    {
        public channel_site()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _build_path = string.Empty;
        private string _templet_path = string.Empty;
        private string _domain = "";
        private string _name;
        private string _logo;
        private string _company;
        private string _address;
        private string _tel;
        private string _fax;
        private string _email;
        private string _crod;
        private string _copyright;
        private string _seo_title;
        private string _seo_keyword;
        private string _seo_description;
        private int _is_default = 0;
        private int _sort_id = 99;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 生成目录名
        /// </summary>
        public string build_path
        {
            set { _build_path = value; }
            get { return _build_path; }
        }
        /// <summary>
        /// 模板目录名
        /// </summary>
        public string templet_path
        {
            set { _templet_path = value; }
            get { return _templet_path; }
        }
        /// <summary>
        /// 绑定域名
        /// </summary>
        public string domain
        {
            set { _domain = value; }
            get { return _domain; }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 网站LOGO
        /// </summary>
        public string logo
        {
            set { _logo = value; }
            get { return _logo; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 备案号
        /// </summary>
        public string crod
        {
            set { _crod = value; }
            get { return _crod; }
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string copyright
        {
            set { _copyright = value; }
            get { return _copyright; }
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string seo_title
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string seo_keyword
        {
            set { _seo_keyword = value; }
            get { return _seo_keyword; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string seo_description
        {
            set { _seo_description = value; }
            get { return _seo_description; }
        }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int is_default
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        #endregion Model

    }
}