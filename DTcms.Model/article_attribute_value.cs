using System;
namespace DTcms.Model
{
    /// <summary>
    /// 扩展属性表
    /// </summary>
    [Serializable]
    public partial class article_attribute_value
    {
        public article_attribute_value()
        { }
        #region Model
        private int _article_id;
        private string _sub_title;
        private string _source = "";
        private string _author = "";
        private string _goods_no;
        private int _stock_quantity = 0;
        private decimal _market_price = 0M;
        private decimal _sell_price = 0M;
        private int _point = 0;
        /// <summary>
        /// 父表ID
        /// </summary>
        public int article_id
        {
            set { _article_id = value; }
            get { return _article_id; }
        }
        /// <summary>
        /// 副标题
        /// </summary>
        public string sub_title
        {
            set { _sub_title = value; }
            get { return _sub_title; }
        }
        /// <summary>
        /// 来源
        /// </summary>
        public string source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 货号
        /// </summary>
        public string goods_no
        {
            set { _goods_no = value; }
            get { return _goods_no; }
        }
        /// <summary>
        /// 库存数量
        /// </summary>
        public int stock_quantity
        {
            set { _stock_quantity = value; }
            get { return _stock_quantity; }
        }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal market_price
        {
            set { _market_price = value; }
            get { return _market_price; }
        }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal sell_price
        {
            set { _sell_price = value; }
            get { return _sell_price; }
        }
        /// <summary>
        /// 积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        #endregion Model

    }
}