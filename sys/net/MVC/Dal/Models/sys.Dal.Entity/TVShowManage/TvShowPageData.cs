using System;
using System.Collections.Generic;
using sys.Application.Code;
using sys.Dal.Entity.BaseManage;

namespace sys.Dal.Entity.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：TV展示屏显示内容
    /// </summary>
    public class TvShowPageData : BaseEntity
    {
        public List<RegisterUserEntity> Rows { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }

        public int Records { get; set; }

        public int PageSize { get; set; }

        public int TotalPage
        {
            get
            {
                int num = this.Records / this.PageSize;
                if (this.Records % this.PageSize != 0)
                    ++num;
                return num;
            }
        }
    }
}