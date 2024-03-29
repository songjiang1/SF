﻿using sys.Dal.Entity.AppManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class SurveyQuestionMap : EntityTypeConfiguration<SurveyQuestionEntity>
    {
        public SurveyQuestionMap()
        {
            #region 表、主键
            //表
            this.ToTable("b_survey_question");
            //主键
            this.HasKey(t => t.QuestionId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
