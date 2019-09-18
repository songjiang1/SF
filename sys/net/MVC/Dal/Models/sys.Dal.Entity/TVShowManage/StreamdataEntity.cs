
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
    public class StreamdataEntity : BaseEntity
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
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// MenuOrganizeEntity 实体主键Id
        /// </summary>
        public string RefId { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string  Content { set; get; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int? PV { set; get; }
        /// <summary>
        /// 点赞
        /// </summary>
        public int? LikeCount { set; get; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public int? EnabledMark { set; get; }
         
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
