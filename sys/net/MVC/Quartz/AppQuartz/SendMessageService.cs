using AppQuartz.Configuration;
using Common.Logging; 
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace AppQuartz
{
    public static class SendMessageService
    {
        static string cronSchedule = ConfigurationSettings.AppSettings["DefaultCronSchedule"];
        static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        static JsonSerializer serializer = new JsonSerializer();

        public static bool DoInteralThing()
        {
            var result = false;
            try
            {
                LogManager.Adapter = new Common.Logging.Simple.TraceLoggerFactoryAdapter()
                {
                    Level = LogLevel.All
                };
                
                var config = ((ScheduleConfiguration)ConfigurationManager.GetSection("Schedule"));
                    if (config == null)
                    {
                        throw new Exception("The Sample section is missing from the App.Config");
                    }
                    scheduler.Start();
                    foreach (GroupElement item in config.Group)
                    {
                        if(!string.IsNullOrEmpty(item.Method) && !string.IsNullOrEmpty(item.Cron))
                        {
                            try
                            {
                                
                                var trigger = TriggerBuilder.Create().WithCronSchedule(item.Cron).Build();
                                var type = Assembly.GetCallingAssembly().GetExportedTypes().FirstOrDefault(a => a.Name == item.Method);
                                if (type == null)
                                {
                                    throw new Exception("can not find the job method");
                                }
                                var job = JobBuilder.Create(type)
                                                    .Build();
                                scheduler.ScheduleJob(job, trigger);
                            }
                            catch (Exception ex)
                            {
                                var str = ex.Message;
                                Console.WriteLine(str);
                            }
                        }
                }
              

               
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
