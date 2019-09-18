  
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using SqlSugar;
using sys.Util.SqlSugar;

namespace sys.Bll.SqlSugar
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class BaseDbContext
    {

        /// <summary>
        /// 注意当前方法的类不能是静态的 public static class这么写是错误的
        /// </summary>
        public static SqlSugarClient Db
        {
            get
            {
                return GetIntance();
            }
        }


        /// <summary>
        ///  获得SqlSugarClient
        /// </summary>
        public static SqlSugarClient GetIntance()
        {
            var listConn = new List<string>();
            var list = new ConfigurationOperator().ALLConnectionStrings();
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    listConn.Add(item);
                }
            }
            if (listConn.Count < 1)
            {
                throw new Exception("数据库配置错误");
            }
            return InitDataBase(listConn);
        }
        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="listConn">连接字符串</param>
        private static SqlSugarClient InitDataBase(List<string> listConn)
        {
            var connStr = "";//主库
            var slaveConnectionConfigs = new List<SlaveConnectionConfig>();//从库集合
            for (var i = 0; i < listConn.Count; i++)
            {
                if (i == 0)
                {
                    connStr = listConn[i];//主数据库连接
                }
                else
                {
                    slaveConnectionConfigs.Add(new SlaveConnectionConfig()
                    {
                        HitRate = i * 2,
                        ConnectionString = listConn[i]
                    });
                }
            }

            //如果配置了 SlaveConnectionConfigs那就是主从模式,所有的写入删除更新都走主库，查询走从库，
            //事务内都走主库，HitRate表示权重 值越大执行的次数越高，如果想停掉哪个连接可以把HitRate设为0 
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connStr,
                DbType =(DbType)Enum.Parse(typeof(DbType), ConfigurationManager.AppSettings["DbType"]),
                IsAutoCloseConnection = true,
                SlaveConnectionConfigs = slaveConnectionConfigs,
                IsShardSameThread = true ,
                InitKeyType= InitKeyType.SystemTable
            });
            db.Ado.CommandTimeOut = 30000;//设置超时时间
            db.Aop.OnLogExecuted = (sql, pars) => //SQL执行完事件
            {
                //LogHelper.WriteLog($"执行时间：{db.Ado.SqlExecutionTime.TotalMilliseconds}毫秒 \r\nSQL如下：{sql} \r\n参数：{GetParams(pars)} ", "SQL执行");
            };
            db.Aop.OnLogExecuting = (sql, pars) => //SQL执行前事件
            {
                if (db.TempItems == null) db.TempItems = new Dictionary<string, object>();
            };
            db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                //LogHelper.WriteLog($"SQL错误:{exp.Message}\r\nSQL如下：{exp.Sql}", "SQL执行");
                throw new Exception(exp.Message);
            };
            db.Aop.OnExecutingChangeSql = (sql, pars) => //SQL执行前 可以修改SQL
            {
                return new KeyValuePair<string, SugarParameter[]>(sql, pars);
            };
            db.Aop.OnDiffLogEvent = (it) => //可以方便拿到 数据库操作前和操作后的数据变化。
            {
                //var editBeforeData = it.BeforeData;
                //var editAfterData = it.AfterData;
                //var sql = it.Sql;
                //var parameter = it.Parameters;
                //var data = it.BusinessData;
                //var time = it.Time;
                //var diffType = it.DiffType;//枚举值 insert 、update 和 delete 用来作业务区分 
                //你可以在这里面写日志方法
            };
            return db;
        }
        /// <summary>
        /// 获取参数信息
        /// </summary>
        /// <param name="pars"></param>
        /// <returns></returns>
        private static string GetParams(SugarParameter[] pars)
        {
            return pars.Aggregate("", (current, p) => current + $"{p.ParameterName}:{p.Value}, ");
        }
    }
}
 