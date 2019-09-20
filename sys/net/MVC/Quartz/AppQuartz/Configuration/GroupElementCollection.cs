using System;
using System.Configuration;

namespace AppQuartz.Configuration
{
    public class GroupElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupElement)element).Method;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "add"; }
        }

        public GroupElement this[int index]
        {
            get { return (GroupElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public GroupElement this[string name]
        {
            get { return (GroupElement)BaseGet(name); }
        }
    }
}
