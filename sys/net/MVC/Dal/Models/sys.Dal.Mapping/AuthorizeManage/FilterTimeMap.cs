﻿
using sys.Dal.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤IP
    /// </summary>
    public class FilterTimeMap : EntityTypeConfiguration<FilterTimeEntity>
    {
        public FilterTimeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_FilterTime");
            //主键
            this.HasKey(t => t.FilterTimeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
