﻿
using sys.Dal.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.12 16:40
    /// 描 述：区域管理
    /// </summary>
    public class AreaMap : EntityTypeConfiguration<AreaEntity>
    {
        public AreaMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Area");
            //主键
            this.HasKey(t => t.AreaId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
