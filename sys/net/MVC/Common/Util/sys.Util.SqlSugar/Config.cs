using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;

namespace sys.Util.SqlSugar
{
    #region 配置信息的操作类
    /// <summary>
    /// 配置信息的操作
    /// </summary>
    public class ConfigurationOperator : IDisposable
    {
        #region 变量的声明
        /// <summary>
        /// Configuration Object
        /// </summary>
        private Configuration config;
        #endregion

        #region 构造函数，有参数(当前应用程序的虚拟路径)
        /// <summary>
        /// 构造函数，有参数(当前应用程序的虚拟路径)
        /// </summary>
        public ConfigurationOperator()
            : this(HttpContext.Current.Request.ApplicationPath)
        {

        }
        #endregion

        #region 构造函数，有参数(其他应用程序的虚拟路径)
        /// <summary>
        /// 构造函数，有参数(其他应用程序的虚拟路径)
        /// </summary>
        /// <param name="path">其他应用程序的虚拟路径</param>
        public ConfigurationOperator(string path)
        {
            config = WebConfigurationManager.OpenWebConfiguration(path);
        }
        #endregion

        #region 获取当前或其他应用程序配置文件中appSettings的所有keyName方法
        /// <summary>
        /// 定义获取当前或其他应用程序appSettings的所有keyName方法
        /// </summary>
        /// <returns>返回appSettings的所有keyName</returns>
        public string[] ActiveALLAppSettingsSection()
        {
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
            string[] appKeys = appSettings.Settings.AllKeys;
            return appKeys;
        }
        #endregion

        #region 设置当前或者其他应用程序配置文件中的appSettings节点
        /// <summary>
        /// 定义设置当前或者其他应用程序配置文件中的appSettings节点
        /// </summary>
        /// <param name="key">keyName</param>
        /// <param name="value">keyValue</param>
        public void SetAppSettingsSection(string key, string value)
        {
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
            if (appSettings.Settings[key] != null)
            {
                appSettings.Settings[key].Value = value;
                this.Save();
            }
            else
            {
                appSettings.Settings.Add(key, value);
                this.Save();
            }
        }
        #endregion

        #region 删除当前或者其他应用程序配置文件中的appSettings节点
        /// <summary>
        /// 定义删除当前或者其他应用程序配置文件中的appSettings节点
        /// </summary>
        /// <param name="key">keyName</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        public bool RemoveAppSettingsSection(string key)
        {
            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");
            if (appSettings.Settings[key] != null)
            {
                appSettings.Settings.Remove(key);
                this.Save();
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 获取当前或其他应用程序配置文件中connectionStrings节点的所有ConnectionString
        /// <summary>
        /// 定义获取当前或其他应用程序配置文件中connectionStrings节点的所有ConnectionString
        /// </summary>
        /// <returns>返回connectionStrings节点的所有ConnectionString</returns>
        public List<string> ALLConnectionStrings()
        {
            ConnectionStringsSection conSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            ConnectionStringSettingsCollection conCollection = conSection.ConnectionStrings;
            List<string> conList = new List<string>();
            int i = 0;
            foreach (ConnectionStringSettings conSetting in conCollection)
            {
                if (conSetting.Name != "LocalSqlServer" && conSetting.Name != "LocalMySqlServer")
                {
                    conList.Add(conSetting.ConnectionString);
                    //conStrings[i++] = conSetting.ConnectionString;
                }
            }  
            return conList;
        }
        #endregion

        #region 设置当前或其他应用程序配置文件中ConnectionString节点
        /// <summary>
        /// 定义设置当前或其他应用程序配置文件中ConnectionString节点
        /// </summary>
        /// <param name="name">connectionStrings Name</param>
        /// <param name="ConnectionString">connectionStrings ConnectionString</param>
        /// <param name="providerName">connectionStrings ProviderName</param>
        public void SetConnectionStringsSection(string name, string ConnectionString, string providerName)
        {
            ConnectionStringsSection conSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            if (conSection.ConnectionStrings[name] != null)
            {
                conSection.ConnectionStrings[name].ConnectionString = ConnectionString;
                conSection.ConnectionStrings[name].ProviderName = providerName;
                this.Save();
            }
            else
            {

                ConnectionStringSettings conSettings = new ConnectionStringSettings(name, ConnectionString, providerName);
                conSection.ConnectionStrings.Add(conSettings);
                this.Save();
            }
        }
        #endregion

        #region 删除当前或其他应用程序配置文件中ConnectionString节点
        /// <summary>
        /// 定义删除当前或其他应用程序配置文件中ConnectionString节点
        /// </summary>
        /// <param name="name">ConnectionStrings Name</param>
        /// <returns>删除成功返回true，删除失败返回false</returns>
        public bool RemoveConnectionStringsSection(string name)
        {
            ConnectionStringsSection conSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            if (conSection.ConnectionStrings[name] != null)
            {
                conSection.ConnectionStrings.Remove(name);
                this.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 保存配置文件，并重新赋值config为null
        /// <summary>
        /// 定义保存配置文件的方法
        /// </summary>
        public void Save()
        {
            config.Save();
            config = null;
        }
        #endregion

        #region 释放配置文件对象
        /// <summary>
        /// 释放配置文件对象
        /// </summary>
        public void Dispose()
        {
            if (config != null)
            {
                config.Save();
            }
        }
        #endregion
    }
    #endregion
}