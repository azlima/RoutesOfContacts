using System;
using Microsoft.Phone.Tasks;
using RoutesOfContacts.Model;
using RoutesOfContacts.Model.Enums;
using RoutesOfContacts.Utils;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel.Validations;

namespace RoutesOfContacts.ViewModel
{
    public class ContactViewModel
    {
        public Contact originContact = new Contact();
        public Contact destinationContact = new Contact();
        public ContactType contactType;

        public void ShowChooserTask()
        {
            AddressChooserTask ad = new AddressChooserTask();
            ad.Completed += new EventHandler<AddressResult>(AddresCompleted);
            ad.Show();
        }

        private void AddresCompleted(object sender, AddressResult e)
        {
            if (contactType == ContactType.Origin)
            {
                originContact.Name = e.DisplayName;
                originContact.Address = e.Address;
                originContact.AddressDescription = e.Address;
                originContact.Type = contactType;
            }
            else
            {
                destinationContact.Name = e.DisplayName;
                destinationContact.Address = e.Address;
                destinationContact.AddressDescription = e.Address;
                destinationContact.Type = contactType;
            }
        }

        public void MyPositionChecked(bool isChecked)
        {
            if (contactType == ContactType.Origin)
            {
                originContact.Name = (isChecked) ? appResource.myPosition : appResource.originContact;
                originContact.Address = string.Empty;
                originContact.AddressDescription = (isChecked) ? string.Empty : appResource.selectContact;
                originContact.Type = contactType;
                originContact.IsMyPosition = isChecked;
            }
            else
            {
                destinationContact.Name = (isChecked) ? appResource.myPosition : appResource.destinationContact;
                destinationContact.Address = string.Empty;
                destinationContact.AddressDescription = (isChecked) ? string.Empty : appResource.selectContact;
                destinationContact.Type = contactType;
                destinationContact.IsMyPosition = isChecked;
            }
        }

        public void AlternateContacts(Contact oldOriginContact, Contact oldDestinationContact)
        {
            oldOriginContact.Type = ContactType.Destination;
            oldDestinationContact.Type = ContactType.Origin;

            originContact = oldDestinationContact;
            destinationContact = oldOriginContact;
        }

        public bool IsValidContacts(Contact origin, Contact destination)
        {
            ContactsValidation contactsValidation = new ContactsValidation();
            return contactsValidation.IsValid(origin, destination);
        }

        public void ShowRoute(Contact origin, Contact destination, ScreenViewModel screenViewModel)
        {
            GeoLocation geoLocation = new GeoLocation();
            geoLocation.originContact = origin;
            geoLocation.destinationContact = destination;
            geoLocation.screenViewModel = screenViewModel;

            if (!origin.IsMyPosition)
            {
                geoLocation.GetLocation(origin);
            }

            if (!destination.IsMyPosition)
            {
                geoLocation.GetLocation(destination);
            }
            else
            {
                geoLocation.GetMyPosition();
            }
        }
    }
}
