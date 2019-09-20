using System;
using System.Configuration;

namespace AppQuartz.Configuration
{
    public class ScheduleConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Group")]
        public GroupElementCollection Group
        {
            get { return this["Group"] as GroupElementCollection; }
        }

    }
}
