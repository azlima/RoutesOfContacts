using System.Windows;
using RoutesOfContacts.Model;
using RoutesOfContacts.View.Contents.Resources;

namespace RoutesOfContacts.ViewModel.Validations
{
    public class ContactsValidation
    {
        public bool IsValid(Contact origin, Contact destination)
        {
            bool isValid = true;
            string message = string.Empty;

            if (origin == null || destination == null)
            {
                message = appResource.fillContacts;
                isValid = false;
            }
            else
            {
                if ((string.IsNullOrEmpty(origin.Name) || string.IsNullOrEmpty(destination.Name)) ||
                    (origin.Name == appResource.originContact || destination.Name == appResource.destinationContact))
                {
                    message = appResource.fillContacts;
                    isValid = false;
                }

                if ((origin.IsMyPosition && destination.IsMyPosition) || 
                    ((origin.Address == destination.Address) &&
                     (!string.IsNullOrEmpty(origin.Address) && (!string.IsNullOrEmpty(destination.Address)))))
                {
                    message = appResource.sameOriginAndDestination;
                    isValid = false;
                }
            }

            if (!isValid)
            {
                MessageBox.Show(message);
            }

            return isValid;
        }
    }
}
