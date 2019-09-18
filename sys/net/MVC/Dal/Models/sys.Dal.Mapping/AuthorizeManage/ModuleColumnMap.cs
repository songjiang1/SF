﻿
using sys.Dal.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    public class ModuleColumnMap : EntityTypeConfiguration<ModuleColumnEntity>
    {
        public ModuleColumnMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ModuleColumn");
            //主键
            this.HasKey(t => t.ModuleColumnId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
