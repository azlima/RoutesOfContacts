using RoutesOfContacts.Model;
using RoutesOfContacts.Utils;
using RoutesOfContacts.View.Contents.Resources;

namespace RoutesOfContacts.ViewModel
{
    public class AboutViewModel
    {
        public About about = new About();
        ConfigValue configValue = new ConfigValue();

        public void LoadViewModel()
        {
            about.VersionDescription = appResource.version;
            about.VersionValue = configValue.AppVersion;
            about.DevelopedByDescription = appResource.developedBy;
            about.DevelopedByValue = configValue.AppDeveloper;
            about.Copyrigth = configValue.AppCopyright;
            about.AllRightsReserved = appResource.allRightsReserved;
            about.CommentsAndSuggestionsToDescription = appResource.commentsAndSuggestionsTo;
            about.CommentsAndSuggestionsToValue = configValue.DeveloperEmailTo;
        }
    }
}
