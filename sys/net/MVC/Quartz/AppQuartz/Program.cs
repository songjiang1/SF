using Quartz;
using System;
using Supermore;
using Supermore.Data;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Data;
using Appweb; 
namespace AppQuartz
{
    class Program
    {
        static void Main(string[] args)
        { 
              SendMessageService.DoInteralThing();
            //new ChangzhiImportSalary().Run();
            //new SychronizeDeptUserAM().CeShi();
            // new SendBroadcastMessageJob().Execute(null);
             
            //WeChatService.Execute();
            new SendWeChatMessageJob().Execute(null);
            //SendWeChatMessage.SendPushMessage("1B525FDC-308B-43A0-96D8-B818F2BC4C5A");

            //new yxtSmsConnector().starPushYxtMsg("3E5C15FA-6B85-4FC4-8BBA-DCC9FA416CF5");
    
        }
    }
}
