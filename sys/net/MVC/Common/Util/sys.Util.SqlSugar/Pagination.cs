using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace sys.Util.SqlSugar
{
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// output
        /// </summary>
        public int PageCount { get; set; } 
        /// 排序类型
        /// </summary>
        public string PageSord { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal
        {
            get
            {
                if (PageCount > 0)
                {
                    return PageCount % this.PageSize == 0 ? PageCount / this.PageSize : PageCount / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public List<OrderByClause> OrderBys { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public List<QueryCondition> Conditions { get; set; } 

    }
    public class OrderByClause
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public OrderSequence Order { get; set; }
    }
    public enum OrderSequence
    {
        /// <summary>
        /// 正序
        /// </summary>
        Asc,
        /// <summary>
        /// 倒序
        /// </summary>
        Desc
    }
    public class QueryCondition
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 查询操作
        /// </summary>
        public QueryOperator Operator { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

    }
    public enum QueryOperator
    {
        /// <summary>
        /// 相等
        /// </summary>
        Equal,
        /// <summary>
        /// 匹配
        /// </summary>
        Like,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于或等于
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于或等于
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 等于集合
        /// </summary>
        In,
        /// <summary>
        /// 不等于集合
        /// </summary>
        NotIn,
        /// <summary>
        /// 左边匹配
        /// </summary>
        LikeLeft,
        /// <summary>
        /// 右边匹配
        /// </summary>
        LikeRight,
        /// <summary>
        /// 不相等
        /// </summary>
        NoEqual,
        /// <summary>
        /// 为空或空
        /// </summary>
        IsNullOrEmpty,
        /// <summary>
        /// 不为空
        /// </summary>
        IsNot,
        /// <summary>
        /// 不匹配
        /// </summary>
        NoLike,
        /// <summary>
        /// 时间段 值用 "|" 隔开
        /// </summary>
        DateRange
    } 
}
