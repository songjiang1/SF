 
using System;
using SqlSugar;
using System.Collections.Generic; 

namespace sys.Bll.Repository.SqlSugar
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class SugarRepositoryFactory
    {
        /// <summary>
        /// 初始化   var db = SugarHandler.Instance()
        /// </summary>
        /// <returns>值</returns>
        public  ISugarRepository BaseRepository()
        {
            return new SugarRepository();
        }
    }
}
