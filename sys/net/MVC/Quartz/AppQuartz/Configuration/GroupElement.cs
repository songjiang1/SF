using System;
using System.Configuration;

namespace AppQuartz.Configuration
{
    public class GroupElement : ConfigurationElement
    {
        [ConfigurationProperty("Method", IsRequired = true, IsKey = true)]
        public string Method
        {
            get
            {
                return this["Method"] as string;
            }
            set
            {
                this["Method"] = value;
            }
        }

        [ConfigurationProperty("Cron")]
        public string Cron
        {
            get
            {
                return this["Cron"] as string;
            }
            set
            {
                this["Cron"] = value;
            }
        }

    }
}
