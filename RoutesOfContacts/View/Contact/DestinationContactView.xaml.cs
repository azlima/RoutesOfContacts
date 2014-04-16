using System.Windows;
using System.Windows.Controls;
using RoutesOfContacts.Model.Enums;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel;
using ContactModel = RoutesOfContacts.Model.Contact;

namespace RoutesOfContacts.View.Contact
{
    public partial class DestinationContactView : UserControl
    {
        public DestinationContactView()
        {
            InitializeComponent();
        }

        public void FillForm(string culture)
        {
            if (this.DataContext == null)
            {
                nameTextBlock.Text = appResource.destinationContact;
                nameLandscapeTextBlock.Text = appResource.destinationContact;
                contactsButton.Content = appResource.selectContact;
                contactsLandscapeButton.Content = appResource.selectContact;
            }
            else if (string.IsNullOrEmpty((this.DataContext as ContactModel).Address))
            {
                nameTextBlock.Text = ((this.DataContext as ContactModel).IsMyPosition) ? appResource.myPosition : appResource.destinationContact;
                nameLandscapeTextBlock.Text = ((this.DataContext as ContactModel).IsMyPosition) ? appResource.myPosition : appResource.destinationContact;
                contactsButton.Content = appResource.selectContact;
                contactsLandscapeButton.Content = ((this.DataContext as ContactModel).IsMyPosition) ? string.Empty : appResource.selectContact;
            }

            myGeoLocationCheckBox.Content = appResource.myPosition; 
            myGeoLocationLandscapeCheckBox.Content = appResource.myPosition;
        }

        private void contactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.contactType = ContactType.Destination;
            contactViewModel.ShowChooserTask();
            this.DataContext = contactViewModel.destinationContact;
        }

        private void myGeoLocationCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isPortraitChecked = (myGeoLocationCheckBox.IsChecked) ?? false;
            bool isLandscapeChecked = (myGeoLocationLandscapeCheckBox.IsChecked) ?? false;
            bool isChecked = isPortraitChecked || isLandscapeChecked;
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.contactType = ContactType.Destination;
            contactViewModel.MyPositionChecked(isChecked);
            this.DataContext = contactViewModel.destinationContact;
        }
    }
}
