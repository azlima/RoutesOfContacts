using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Resources;
using System.Xml;
using RoutesOfContacts.Model;

namespace RoutesOfContacts.Utils
{
    public class ConfigValue
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string _appVersion, _appCopyright, _appDescription, _appDeveloper, _developerEmailTo, _applicationIdForBingMaps;
        IList<Language> _languages = new List<Language>();

        #region Properties

        public string AppVersion
        {
            get
            {
                return GetAppVersion();
            }
        }

        public string AppCopyright
        {
            get
            {
                return GetAppCopyright();
            }
        }

        public string AppDescription
        {
            get
            {
                return GetAppDescription();
            }
        }

        public string AppDeveloper
        {
            get
            {
                return GetAppDeveloper();
            }
        }

        public string DeveloperEmailTo
        {
            get
            {
                return GetDeveloperEmailTo();
            }
        }

        public string ApplicationIdForBingMaps
        {
            get
            {
                return GetApplicationIdForBingMaps();
            }
        }

        public IList<Language> SupportedCultures
        {
            get
            {
                return GetSupportedCultures();
            }
        }

        #endregion

        #region Methods

        private string GetAppVersion()
        {
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    _appVersion = ((AssemblyFileVersionAttribute)customAttributes[0]).Version;
                }
            }
            return _appVersion;
        }

        private string GetAppCopyright()
        {
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    _appCopyright = ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
                }
            }
            return _appCopyright;
        }

        private string GetAppDescription()
        {
            if (assembly != null)
            {
                object[] customAttributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if ((customAttributes != null) && (customAttributes.Length > 0))
                {
                    _appDescription = ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
                }
            }
            return _appDescription;
        }

        private string GetAppDeveloper()
        {
            StreamResourceInfo xml = App.GetResourceStream(new Uri("WMAppManifest.xml", UriKind.Relative));
            XmlReader reader = XmlReader.Create(xml.Stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "App")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "Author")
                            {
                                _appDeveloper = reader.Value;
                            }
                        }
                    }
                }
            }
            return _appDeveloper;
        }

        private string GetDeveloperEmailTo()
        {
            StreamResourceInfo xml = App.GetResourceStream(new Uri("Config.xml", UriKind.Relative));
            XmlReader reader = XmlReader.Create(xml.Stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "Emails")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "DeveloperEmailTo")
                            {
                                _developerEmailTo = reader.Value;
                            }
                        }
                    }
                }
            }
            return _developerEmailTo;
        }

        private string GetApplicationIdForBingMaps()
        {
            StreamResourceInfo xml = App.GetResourceStream(new Uri("Config.xml", UriKind.Relative));
            XmlReader reader = XmlReader.Create(xml.Stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "App")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.MoveToAttribute("ApplicationId"))
                            {
                                _applicationIdForBingMaps = reader.Value;
                            }
                        }
                    }
                }
            }
            return _applicationIdForBingMaps;
        }

        private IList<Language> GetSupportedCultures()
        {
            StreamResourceInfo xml = App.GetResourceStream(new Uri("Config.xml", UriKind.Relative));
            XmlReader reader = XmlReader.Create(xml.Stream);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "SupportedCulture")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            Language language = new Language();
                            if (reader.MoveToAttribute("Culture"))
                            {
                                language.Culture = reader.Value;
                            }
                            if (reader.MoveToAttribute("Description"))
                            {
                                language.Description = reader.Value;
                            }
                            if (reader.MoveToAttribute("ImageFlagPath"))
                            {
                                language.ImageFlagPath = reader.Value;
                            }
                            _languages.Add(language);
                        }
                    }
                }
            }
            return _languages;
        }

        #endregion
    }
}
