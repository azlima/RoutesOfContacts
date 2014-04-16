using System.Windows;
using RoutesOfContacts.Model;
using RoutesOfContacts.View.Contents.Resources;

namespace RoutesOfContacts.ViewModel
{
    public class ScreenViewModel
    {
        public Screen screen = new Screen();

        public void EnableScreen()
        {
            screen.Opacity = 1;
            screen.IsEnabled = true;
            screen.IsIndeterminate = false;
            screen.Visibility = Visibility.Collapsed;
            screen.AlternateButton = appResource.alternate;
            screen.LanguagesMenuItem = appResource.languages;
            screen.RateAndReviewMenuItem = appResource.rateAndReview;
            screen.ContributeDonateMenuItem = appResource.contributeDonate;
            screen.TalkToTheDeveloperMenuItem = appResource.talkToTheDeveloper;
            screen.AboutMenuItem = appResource.about;
        }

        public void DisableScreen()
        {
            screen.Opacity = 0.2;
            screen.IsEnabled = false;
            screen.IsIndeterminate = true;
            screen.Visibility = Visibility.Visible;
        }
    }
}
