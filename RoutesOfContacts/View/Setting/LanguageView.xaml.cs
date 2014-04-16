using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using RoutesOfContacts.Model;
using RoutesOfContacts.Utils;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel;

namespace RoutesOfContacts.View.Setting
{
    public partial class LanguageView : PhoneApplicationPage
    {
        LanguageViewModel languageViewModel = new LanguageViewModel();
        ConfigValue configValue = new ConfigValue();

        public LanguageView()
        {
            InitializeComponent();
            LoadOrientation(this.Orientation);

            this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(LanguageView_OrientationChanged);
        }

        private void LoadOrientation(PageOrientation pageOrientation)
        {
            switch (pageOrientation)
            {
                case PageOrientation.Landscape:
                    LoadLandscape(0, 0, 0, 0);
                    break;
                case PageOrientation.LandscapeLeft:
                    LoadLandscape(0, 0, 10, 0);
                    break;
                case PageOrientation.LandscapeRight:
                    LoadLandscape(30, 0, 0, 0);
                    break;
                case PageOrientation.None:
                    LoadPortrait(0, 0, 0, 0);
                    break;
                case PageOrientation.Portrait:
                    LoadPortrait(0, 0, 0, 0);
                    break;
                case PageOrientation.PortraitDown:
                    LoadPortrait(0, 0, 0, 0);
                    break;
                case PageOrientation.PortraitUp:
                    LoadPortrait(0, 0, 0, 0);
                    break;
                default:
                    LoadPortrait(0, 0, 0, 0);
                    break;
            }
        }

        void LanguageView_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            LoadOrientation(e.Orientation);
        }

        private void LoadPortrait(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.LanguagesListStackPanel.Width = 400;
        }

        private void LoadLandscape(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.LanguagesListStackPanel.Width = 620;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string culture;
            NavigationContext.QueryString.TryGetValue("culture", out culture);

            if (string.IsNullOrEmpty(culture))
            {
                culture = Thread.CurrentThread.CurrentCulture.Name;
            }
            FillForm(culture);
        }

        private void FillForm(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            appResource.Culture = new CultureInfo(culture);

            ApplicationTitle.Text = appResource.routesOfContacts.ToUpper();
            PageTitle.Text = appResource.languages.ToLower();

            if (languagesList.Items.Count == 0)
            {
                FillLanguagesListBox();
            }
        }

        private void FillLanguagesListBox()
        {
            int i = 0;
            int selectedCultureIndex = 0;
            string culture;
            IList<Language> languages = configValue.SupportedCultures;

            (Application.Current as App).isolatedStorageSettings.TryGetValue<string>("culture", out culture);
            if (string.IsNullOrEmpty(culture))
            {
                culture = Thread.CurrentThread.CurrentCulture.Name;
            }

            languagesList.DataContext = languages.OrderBy(x => x.Description);

            foreach (var itemLanguage in languages.OrderBy(x => x.Description))
            {
                if (culture == itemLanguage.Culture)
                {
                    selectedCultureIndex = i;
                }
                i++;
            }

            languagesList.SelectedIndex = selectedCultureIndex;
        }

        private void languagesList_Tap(object sender, GestureEventArgs e)
        {
            Language language = languagesList.SelectedItem as Language;
            languageViewModel.SaveIsolatedStorageSettings("culture", language.Culture);
            FillForm(language.Culture);
            NavigationService.GoBack();
        }
    }
}