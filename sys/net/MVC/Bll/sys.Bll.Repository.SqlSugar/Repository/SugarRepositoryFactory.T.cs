 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using SqlSugar; 

namespace sys.Bll.Repository.SqlSugar
{

    /// <summary>
    /// SqlSugar操作类型
    /// </summary> 
  public class SugarRepositoryFactory<T> where T : class, new()
    {
        /// <summary>
        /// 定义仓储（基础库）
        /// </summary>
        /// <returns></returns> 
        public ISugarRepository<T> BaseRepository()
        {
            return new SugarRepository<T>();
        }
    }
}