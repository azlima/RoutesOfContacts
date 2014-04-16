using System.ComponentModel;

namespace RoutesOfContacts.Model
{
    public class About : INotifyPropertyChanged
    {
        public string VersionDescription
        {
            get
            {
                return _versionDescription;
            }
            set
            {
                if (value != _versionDescription)
                {
                    _versionDescription = value;
                    OnPropertyChanged("VersionDescription");
                }
            }
        }

        public string VersionValue
        {
            get
            {
                return _versionValue;
            }
            set
            {
                if (value != _versionValue)
                {
                    _versionValue = value;
                    OnPropertyChanged("VersionValue");
                }
            }
        }
        
        public string DevelopedByDescription
        {
            get
            {
                return _developedByDescription;
            }
            set
            {
                if (value != _developedByDescription)
                {
                    _developedByDescription = value;
                    OnPropertyChanged("DevelopedByDescription");
                }
            }
        }

        public string DevelopedByValue 
        {
            get
            {
                return _developedByValue;
            }
            set
            {
                if (value != _developedByValue)
                {
                    _developedByValue = value;
                    OnPropertyChanged("DevelopedByValue");
                }
            }
        }

        public string Copyrigth 
        {
            get
            {
                return _copyrigth;
            }
            set
            {
                if (value != _copyrigth)
                {
                    _copyrigth = value;
                    OnPropertyChanged("Copyrigth");
                }
            }
        }

        public string AllRightsReserved
        {
            get
            {
                return _allRightsReserved;
            }
            set
            {
                if (value != _allRightsReserved)
                {
                    _allRightsReserved = value;
                    OnPropertyChanged("AllRightsReserved");
                }
            }
        }

        public string CommentsAndSuggestionsToDescription 
        {
            get
            {
                return _commentsAndSuggestionsToDescription;
            }
            set
            {
                if (value != _commentsAndSuggestionsToDescription)
                {
                    _commentsAndSuggestionsToDescription = value;
                    OnPropertyChanged("CommentsAndSuggestionsToDescription");
                }
            }
        }
        public string CommentsAndSuggestionsToValue
        {
            get
            {
                return _commentsAndSuggestionsToValue;
            }
            set
            {
                if (value != _commentsAndSuggestionsToValue)
                {
                    _commentsAndSuggestionsToValue = value;
                    OnPropertyChanged("CommentsAndSuggestionsToValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _versionDescription;
        private string _versionValue;
        private string _developedByDescription;
        private string _developedByValue;
        private string _copyrigth;
        private string _allRightsReserved;
        private string _commentsAndSuggestionsToDescription;
        private string _commentsAndSuggestionsToValue;

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
