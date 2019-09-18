
using sys.Application.Code;
using System;

namespace sys.Dal.Entity.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：TV系统功能
    /// </summary>
    public class MenuOrganizeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 机构/组织
        /// </summary>
        public string OrganizeId { set; get; }

        /// <summary>
        /// 系统菜单主键
        /// </summary>
        public string MenuId { set; get; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string ParentId { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { set; get; }
        /// <summary>
        /// 名称拼音
        /// </summary>
        public string SimpleSpelling { set; get; }
        /// <summary>
        /// 封面
        /// </summary>
        public string Cover { set; get; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { set; get; }
        /// <summary>
        /// 布局
        /// </summary>
        public string Layout { set; get; } 

        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? DeleteMark { set; get; }
        /// <summary>
        /// 自定义文章标记
        /// </summary>
        public int? ArticleMark { set; get; }
        /// <summary>
        /// 是否主页
        /// </summary>
        public int? IsHome { set; get; }


        /// <summary>
        /// 有效标志
        /// </summary>
        public int? EnabledMark { set; get; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { set; get; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string CreateUserId { set; get; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserName { set; get; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { set; get; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string ModifyUserId { set; get; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string ModifyUserName { set; get; }
        /// <summary>
        /// 导航地址 后台管理地址
        /// </summary>
        public string UrlAddress { set; get; }

        /// <summary>
        /// 导航地址
        /// </summary>
        public string LookUrlAddress { set; get; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
