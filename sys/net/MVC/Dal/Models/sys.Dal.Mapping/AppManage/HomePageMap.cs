
using sys.Dal.Entity.AppManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：张念
    /// 日 期：2019.05.22 10:40
    /// 描 述：系统主页
    /// </summary>
    public class HomePageMap : EntityTypeConfiguration<HomePageEntity>
    {
        public HomePageMap()
        {
            #region 表、主键
            //表
            this.ToTable("b_apphomepage");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
