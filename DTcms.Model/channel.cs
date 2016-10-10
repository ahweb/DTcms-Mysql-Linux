using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    [Serializable]
    public partial class channel
    {
        public channel()
        { }
        #region Model
        private int _id;
        private int _site_id;
        private string _name = "";
        private string _title = "";
        private int _is_albums = 0;
        private int _is_attach = 0;
        private int _is_spec = 0;
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
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 频道标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 是否开启相册功能
        /// </summary>
        public int is_albums
        {
            set { _is_albums = value; }
            get { return _is_albums; }
        }
        /// <summary>
        /// 是否开启附件功能
        /// </summary>
        public int is_attach
        {
            set { _is_attach = value; }
            get { return _is_attach; }
        }
        /// <summary>
        /// 是否开启规格
        /// </summary>
        public int is_spec
        {
            set { _is_spec = value; }
            get { return _is_spec; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }

        private List<channel_field> _channel_fields;
        /// <summary>
        /// 扩展字段 
        /// </summary>
        public List<channel_field> channel_fields
        {
            set { _channel_fields = value; }
            get { return _channel_fields; }
        }
        #endregion Model

    }
}