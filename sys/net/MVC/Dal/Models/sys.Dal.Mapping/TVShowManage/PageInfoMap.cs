﻿using sys.Dal.Entity;
using sys.Dal.Entity.TVShowManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Mapping
{
  public class PageInfoMap : EntityTypeConfiguration<PageInfoEntity>
    { 
        public PageInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("b_tv_pageinfo");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        } 
    }
}
