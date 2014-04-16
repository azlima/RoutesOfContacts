using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel;

namespace RoutesOfContacts.View.About
{
    public partial class AboutView : PhoneApplicationPage
    {
        AboutViewModel aboutViewModel = new AboutViewModel();

        public AboutView()
        {
            InitializeComponent();
            this.DataContext = aboutViewModel.about;
            LoadOrientation(this.Orientation);
            
            this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(AboutView_OrientationChanged);
        }

        private void LoadOrientation(PageOrientation pageOrientation)
        {
            switch (pageOrientation)
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

        void AboutView_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            LoadOrientation(e.Orientation);
        }

        private void LoadPortrait(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.PortraitStackPanel.Visibility = Visibility.Visible;
            this.LandscapeStackPanel.Visibility = Visibility.Collapsed;
            this.PortraitStackPanel.Width = 400;
        }

        private void LoadLandscape(double left, double top, double right, double botton)
        {
            this.Margin = new Thickness(left, top, right, botton);
            this.PortraitStackPanel.Visibility = Visibility.Collapsed;
            this.LandscapeStackPanel.Visibility = Visibility.Visible;
            this.LandscapeStackPanel.Width = 620;
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
            PageTitle.Text = appResource.about.ToLower();

            aboutViewModel.LoadViewModel();
        }

        private void HyperlinkButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = aboutViewModel.about.CommentsAndSuggestionsToValue;
            emailComposeTask.Subject = string.Format("[WP] {0} App", appResource.routesOfContacts);
            emailComposeTask.Show();
        }
    }
}