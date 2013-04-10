﻿using System.Configuration;

namespace Mubox.Configuration
{
    [ConfigurationCollection(typeof(KeySetting))]
    public class KeySettingCollection
        : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return (new KeySetting()) as ConfigurationElement;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as KeySetting).InputKey;
        }

        internal KeySetting CreateNew(WinAPI.VK inputKey)
        {
            ConfigurationElement element = CreateNewElement();
            KeySetting settings = element as KeySetting;
            settings.InputKey = inputKey;
            settings.OutputKey = inputKey;
            base.BaseAdd(element);
            return settings;
        }

        public KeySetting GetOrCreateNew(WinAPI.VK inputKey)
        {
            foreach (KeySetting s in this)
            {
                if (s.InputKey.Equals(inputKey))
                {
                    return s;
                }
            }
            return CreateNew(inputKey);
        }

        public void Remove(WinAPI.VK inputKey)
        {
            base.BaseRemove(inputKey);
        }

        public bool TryGetKeySetting(WinAPI.VK vk, out KeySetting keySetting)
        {
            keySetting = base.BaseGet(vk) as KeySetting;
            return keySetting != null;
        }
    }
}