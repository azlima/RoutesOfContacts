using System;
using System.Collections.Generic;
using System.Windows;

namespace RoutesOfContacts.ViewModel
{
    public class LanguageViewModel
    {
        public void SaveIsolatedStorageSettings(string key, string value)
        {
            if (!(Application.Current as App).isolatedStorageSettings.Contains(key))
            {
                (Application.Current as App).isolatedStorageSettings.Add(key, value);
            }
            else
            {
                (Application.Current as App).isolatedStorageSettings[key] = value;
            }
            (Application.Current as App).isolatedStorageSettings.Save();
        }
    }
}
