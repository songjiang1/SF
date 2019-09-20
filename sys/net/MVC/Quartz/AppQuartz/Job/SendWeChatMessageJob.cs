using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Net;
using System.IO;
using System.Text;
using AppQuartz;
using System.Web.Script.Serialization;
using Youyou;
using Supermore.Configuration;
using System.Net.Http;

namespace Appweb
{
    public class SendWeChatMessageJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //推送 
            NetLog.WriteTextLog(string.Format("推模板送消息到微信{0}", DateTime.Now));
            Console.WriteLine(string.Format("推模板送消息到微信{0}", DateTime.Now));
            //SendWeChatMessage.SendPushMessage();

        }


    }
} 