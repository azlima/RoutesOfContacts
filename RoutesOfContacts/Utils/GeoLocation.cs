using System;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using Microsoft.Phone.Tasks;
using RoutesOfContacts.GeocodeService;
using RoutesOfContacts.Model;
using RoutesOfContacts.Model.Enums;
using RoutesOfContacts.View.Contents.Resources;
using RoutesOfContacts.ViewModel;

namespace RoutesOfContacts.Utils
{
    public class GeoLocation
    {
        GeoCoordinateWatcher watcher;
        ConfigValue configValue = new ConfigValue();

        public Contact originContact = new Contact();
        public Contact destinationContact = new Contact();
        public ScreenViewModel screenViewModel = new ScreenViewModel();

        public void GetLocation(Contact contact)
        {
            GeocodeRequest geocodeRequest = new GeocodeRequest();

            // Set the credentials using a valid Bing Maps key
            geocodeRequest.Credentials = new GeocodeService.Credentials();
            geocodeRequest.Credentials.ApplicationId = configValue.ApplicationIdForBingMaps;

            // Set the full address query
            geocodeRequest.Query = contact.Address;

            // Set the options to only return high confidence results 
            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;

            // Add the filters to the options
            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = new ObservableCollection<FilterBase>() { filters[0] };
            geocodeRequest.Options = geocodeOptions;
            ExecutionOptions execOptions = new ExecutionOptions();

            // Make the geocode request
            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");

            geocodeService.GeocodeCompleted += (sender, e) =>
                {
                    if (e.Result != null && e.Result.Results.Any(o => o.Locations != null && o.Locations.Any()))
                    {
                        contact.Lat = e.Result.Results.FirstOrDefault().Locations.FirstOrDefault().Latitude;
                        contact.Lng = e.Result.Results.FirstOrDefault().Locations.FirstOrDefault().Longitude;
                        if (contact.Type == ContactType.Origin)
                        {
                            originContact = contact;
                        }
                        else if (contact.Type == ContactType.Destination)
                        {
                            destinationContact = contact;
                        }
                        if (CanShowMapRoute())
                        {
                            ShowMapRoute();
                        }
                    }
                    else if (e.Error != null)
                    {
                        screenViewModel.EnableScreen();
                        MessageBox.Show(string.Format("{0} \n{1}", appResource.locationError, e.Error.Message));
                    }
                    else
                    {
                        screenViewModel.EnableScreen();
                        MessageBox.Show(appResource.locationNotFound);
                    }
                };

            geocodeService.GeocodeAsync(geocodeRequest);
        }

        public void GetMyPosition()
        {
            if (watcher == null)
            {
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                watcher.MovementThreshold = 100;
                watcher.StatusChanged += (sender, e) =>
                    {
                        switch (e.Status)
                        {
                            case GeoPositionStatus.Disabled:
                                screenViewModel.EnableScreen();
                                MessageBox.Show(appResource.locationServiceNotEnabled);
                                break;

                            case GeoPositionStatus.NoData:
                                screenViewModel.EnableScreen();
                                MessageBox.Show(appResource.locationServiceNoData);
                                break;
                        }
                    };
                watcher.PositionChanged += (sender, e) =>
                    {
                        GetWatcherPositionLocation(e.Position.Location);
                    };
            }

            watcher.Start();
        }

        private void GetWatcherPositionLocation(GeoCoordinate geoCoordinate)
        {
            destinationContact.Lat = geoCoordinate.Latitude;
            destinationContact.Lng = geoCoordinate.Longitude;

            if (CanShowMapRoute())
            {
                ShowMapRoute();
            }
        }

        private bool CanShowMapRoute()
        {
            bool canShow = true;
            if (!originContact.IsMyPosition)
            {
                if (originContact.Lat == 0 || originContact.Lng == 0)
                {
                    canShow = false;
                }
            }

            if (destinationContact.Lat == 0 || destinationContact.Lng == 0)
            {
                canShow = false;
            }
            return canShow;
        }

        private void ShowMapRoute()
        {
            BingMapsDirectionsTask direction = new BingMapsDirectionsTask();
            if (!originContact.IsMyPosition)
            {
                direction.Start = new LabeledMapLocation(originContact.Name, new GeoCoordinate(originContact.Lat, originContact.Lng));
            }

            direction.End = new LabeledMapLocation(destinationContact.Name, new GeoCoordinate(destinationContact.Lat, destinationContact.Lng));
            direction.Show();
            screenViewModel.EnableScreen();

            if (watcher != null)
            {
                watcher.Stop();
            }
        }
    }
}
