using System;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using RoutesOfContacts.Model;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel;
using RoutesOfContacts.Utils;

namespace RoutesOfContacts
{
    public partial class MainPage : PhoneApplicationPage
    {
        ScreenViewModel screenViewModel = new ScreenViewModel();
        ConfigValue configValue = new ConfigValue();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            screenViewModel.EnableScreen();
            this.DataContext = screenViewModel.screen;
            this.originContactView.DataContext = new Contact();
            this.destinationContactView.DataContext = new Contact();

            this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(MainPage_OrientationChanged);
        }

        void MainPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            switch (e.Orientation)
            {
                case PageOrientation.Landscape:
                    LoadLandscape(0, 0, 0, 0);
                    break;
                case PageOrientation.LandscapeLeft:
                    LoadLandscape(0, 0, 30, 0);
                    break;
                case PageOrientation.LandscapeRight:
                    LoadLandscape(80, 0, 0, 0);
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

        private void LoadPortrait(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.originContactView.PortraitStackPanel.Visibility = Visibility.Visible;
            this.originContactView.LandscapeStackPanel.Visibility = Visibility.Collapsed;
            this.destinationContactView.PortraitStackPanel.Visibility = Visibility.Visible;
            this.destinationContactView.LandscapeStackPanel.Visibility = Visibility.Collapsed;
            this.MainStackPanel.Width = 400;
        }

        private void LoadLandscape(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.originContactView.PortraitStackPanel.Visibility = Visibility.Collapsed;
            this.originContactView.LandscapeStackPanel.Visibility = Visibility.Visible;
            this.destinationContactView.PortraitStackPanel.Visibility = Visibility.Collapsed;
            this.destinationContactView.LandscapeStackPanel.Visibility = Visibility.Visible;
            this.MainStackPanel.Width = 620;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string culture;
            if ((Application.Current as App).isolatedStorageSettings.TryGetValue<string>("culture", out culture))
            {
                FillForm(culture);
            }
            else
            {
                FillForm(Thread.CurrentThread.CurrentCulture.Name);
            }
        }

        private void FillForm(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            appResource.Culture = new CultureInfo(culture);

            ApplicationTitle.Text = appResource.routesOfContacts.ToUpper();
            PageTitle.Text = appResource.contacts.ToLower();

            originContactView.FillForm(culture);
            destinationContactView.FillForm(culture);

            showRouteButtonContent.Text = appResource.showRoute;
            loadingText.Text = appResource.loading;

            FillApplicationBar();
        }

        private void FillApplicationBar()
        {
            screenViewModel.screen.AlternateButton = appResource.alternate;
            screenViewModel.screen.LanguagesMenuItem = appResource.languages;
            screenViewModel.screen.RateAndReviewMenuItem = appResource.rateAndReview;
            screenViewModel.screen.ContributeDonateMenuItem = appResource.contributeDonate;
            screenViewModel.screen.TalkToTheDeveloperMenuItem = appResource.talkToTheDeveloper;
            screenViewModel.screen.AboutMenuItem = appResource.about;

            SetApplicationBarCommands();
        }

        private void SetApplicationBarCommands()
        {
            screenViewModel.screen.AlternateButtonCommand = new DelegateCommand(p =>
                {
                    ContactViewModel contactViewModel = new ContactViewModel();
                    if (contactViewModel.IsValidContacts(originContactView.DataContext as Contact, destinationContactView.DataContext as Contact))
                    {
                        contactViewModel.AlternateContacts(originContactView.DataContext as Contact, destinationContactView.DataContext as Contact);
                        originContactView.DataContext = contactViewModel.originContact;
                        destinationContactView.DataContext = contactViewModel.destinationContact;
                    }
                }
            );

            screenViewModel.screen.LanguagesMenuItemCommand = new DelegateCommand(p =>
                {
                    string culture;
                    (Application.Current as App).isolatedStorageSettings.TryGetValue<string>("culture", out culture);
                    if (string.IsNullOrEmpty(culture))
                    {
                        culture = Thread.CurrentThread.CurrentCulture.Name;
                    }
                    NavigationService.Navigate(new Uri("/View/Setting/LanguageView.xaml?culture=" + culture, UriKind.RelativeOrAbsolute));
                }
            );

            screenViewModel.screen.RateAndReviewMenuItemCommand = new DelegateCommand(p =>
                {
                    MarketplaceReviewTask marketPlaceReviewTask = new MarketplaceReviewTask();
                    marketPlaceReviewTask.Show();
                }
            );

            screenViewModel.screen.ContributeDonateMenuItemCommand = new DelegateCommand(p =>
                {
                    WebBrowserTask webBrowserTask = new WebBrowserTask();

                    string business = configValue.DeveloperEmailTo;
                    string description = string.Format("[WP]%20{0}%20App", appResource.routesOfContacts);
                    string country = "BR";
                    string currency = "BRL";
                    string url = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=";
                    string uri = string.Format("{0}{1}&lc={2}&item_name={3}&currency_code={4}&bn=PP%2dDonationsBF", url, business, country, description, currency);

                    webBrowserTask.Uri = new Uri(uri, UriKind.RelativeOrAbsolute);
                    webBrowserTask.Show();
                }
            );

            screenViewModel.screen.TalkToTheDeveloperMenuItemCommand = new DelegateCommand(p =>
                {
                    EmailComposeTask emailComposeTask = new EmailComposeTask();
                    emailComposeTask.To = configValue.DeveloperEmailTo;
                    emailComposeTask.Subject = string.Format("[WP] {0} App", appResource.routesOfContacts);
                    emailComposeTask.Show();
                }
            );

            screenViewModel.screen.AboutMenuItemCommand = new DelegateCommand(p =>
                {
                    {
                        string culture;
                        (Application.Current as App).isolatedStorageSettings.TryGetValue<string>("culture", out culture);
                        if (string.IsNullOrEmpty(culture))
                        {
                            culture = Thread.CurrentThread.CurrentCulture.Name;
                        }
                        NavigationService.Navigate(new Uri("/View/About/AboutView.xaml?culture=" + culture, UriKind.RelativeOrAbsolute));
                    }
                }
            );
        }

        private void showRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            if (contactViewModel.IsValidContacts(originContactView.DataContext as Contact, destinationContactView.DataContext as Contact))
            {
                screenViewModel.DisableScreen();
                contactViewModel.ShowRoute(originContactView.DataContext as Contact, destinationContactView.DataContext as Contact, screenViewModel);
            }
        }
    }
}