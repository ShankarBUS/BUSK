using System;
using System.Xml;
using System.Xml.Serialization;

namespace BUSK.Core
{
    [Serializable]
    public abstract class SettingBase
    {
        [XmlIgnore]
        internal string ID { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public abstract object GetValue();
    }

    [Serializable]
    [XmlRoot]
    public sealed class Setting<TValue> : SettingBase
    {
        public Setting()
        {
            ID = Guid.NewGuid().ToString();
        }

        public Setting(string name, TValue value) : this()
        {
            Name = name;
            Value = value;
        }

        public TValue Value { get; set; }

        public override object GetValue() => Value;
    }

    [Serializable]
    public sealed class SettingWrapper
    {
        public SettingWrapper()
        {

        }

        public SettingWrapper(SettingBase settingBase)
        {
            var settingType = settingBase.GetType();
            SettingTypeString = settingType.AssemblyQualifiedName;
            ID = settingBase.ID;
        }

        public SettingWrapper(string id, Type settingType)
        {
            SettingTypeString = settingType.AssemblyQualifiedName;
            ID = id;
        }

        [XmlAttribute]
        public string SettingTypeString { get; set; }
        
        [XmlAttribute]
        public string ID { get; set; }
    }
}
