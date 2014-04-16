using System.ComponentModel;
using RoutesOfContacts.Model.Enums;

namespace RoutesOfContacts.Model
{
    public class Contact : INotifyPropertyChanged
    {
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Address 
        { 
            get
            {
                return _address;
            }
            set
            {
                if (value != _address)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public string AddressDescription
        {
            get
            {
                return _addressDescription;
            }
            set
            {
                if (value != _addressDescription)
                {
                    _addressDescription = value;
                    OnPropertyChanged("AddressDescription");
                }
            }
        }

        public ContactType Type 
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public double Lat 
        {
            get
            {
                return _lat;
            }
            set
            {
                if (value != _lat)
                {
                    _lat = value;
                    OnPropertyChanged("Lat");
                }
            }
        }

        public double Lng 
        {
            get
            {
                return _lng;
            }
            set
            {
                if (value != _lng)
                {
                    _lng = value;
                    OnPropertyChanged("Lng");
                }
            }
        }

        public bool IsMyPosition
        {
            get
            {
                return _isMyPosition;
            }
            set
            {
                if (value != _isMyPosition)
                {
                    _isMyPosition = value;
                    OnPropertyChanged("IsMyPosition");
                }
            }
        }

        public bool CanSelectContact
        {
            get
            {
                return !IsMyPosition;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _name;
        private string _address;
        private string _addressDescription;
        private ContactType _type;
        private double _lat;
        private double _lng;
        private  bool _isMyPosition;

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
