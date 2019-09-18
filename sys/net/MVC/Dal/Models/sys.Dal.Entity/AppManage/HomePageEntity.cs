using System;
using sys.Application.Code;
using sys.Dal.Entity;


namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.05.22 10:58
    /// 描 述：主页信息实体
    /// </summary>
    public class HomePageEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>		
        public string Cover { get; set; }


        /// <summary>
        /// 缩略图链接
        /// </summary>		
        public string CoverLink { get; set; }
        /// <summary>
        /// 标题   
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 标题   Color
        /// </summary>
        public string TitleColor { get; set; }
        

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 首页显示
        /// </summary>		
        public int? IsShow { get; set; }


        /// <summary>
        /// 发布状态
        /// </summary>
        public int? IsPublished { get; set; }
        /// <summary>
        /// 小图状态
        /// </summary>		
        public int? HaveSmallGraph { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>		
        public string Content { get; set; }

        /// <summary>
        /// 排序编码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>		
        public string OrganizeId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>		
        public string OrganizeName { get; set; }
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
            this.OrganizeId = OperatorProvider.Provider.Current().CompanyId;
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
