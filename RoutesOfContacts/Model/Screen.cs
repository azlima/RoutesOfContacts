using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace RoutesOfContacts.Model
{
    public class Screen : INotifyPropertyChanged
    {
        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (value != _opacity)
                {
                    _opacity = value;
                    OnPropertyChanged("Opacity");
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public bool IsIndeterminate
        {
            get
            {
                return _isIndeterminate;
            }
            set
            {
                if (value != _isIndeterminate)
                {
                    _isIndeterminate = value;
                    OnPropertyChanged("IsIndeterminate");
                }
            }
        }

        public Visibility Visibility 
        {
            get
            {
                return _visibility;
            }
            set
            {
                if (value != _visibility)
                {
                    _visibility = value;
                    OnPropertyChanged("Visibility");
                }
            }
        }

        #region ApplicationBarItems

        public string AlternateButton
        {
            get
            {
                return _alternateButton;
            }
            set
            {
                if (value != _alternateButton)
                {
                    _alternateButton = value;
                    OnPropertyChanged("AlternateButton");
                }
            }
        }

        public string LanguagesMenuItem
        {
            get
            {
                return _languagesMenuItem;
            }
            set
            {
                if (value != _languagesMenuItem)
                {
                    _languagesMenuItem = value;
                    OnPropertyChanged("LanguagesMenuItem");
                }
            }
        }

        public string RateAndReviewMenuItem
        {
            get
            {
                return _rateAndReviewMenuItem;
            }
            set
            {
                if (value != _rateAndReviewMenuItem)
                {
                    _rateAndReviewMenuItem = value;
                    OnPropertyChanged("RateAndReviewMenuItem");
                }
            }
        }

        public string ContributeDonateMenuItem
        {
            get
            {
                return _contributeDonateMenuItem;
            }
            set
            {
                if (value != _contributeDonateMenuItem)
                {
                    _contributeDonateMenuItem = value;
                    OnPropertyChanged("ContributeDonateMenuItem");
                }
            }
        }

        public string TalkToTheDeveloperMenuItem
        {
            get
            {
                return _talkToTheDeveloperMenuItem;
            }
            set
            {
                if (value != _talkToTheDeveloperMenuItem)
                {
                    _talkToTheDeveloperMenuItem = value;
                    OnPropertyChanged("TalkToTheDeveloperMenuItem");
                }
            }
        }

        public string AboutMenuItem
        {
            get
            {
                return _aboutMenuItem;
            }
            set
            {
                if (value != _aboutMenuItem)
                {
                    _aboutMenuItem = value;
                    OnPropertyChanged("AboutMenuItem");
                }
            }
        }

        #endregion

        #region ApplicationBarCommands

        public ICommand AlternateButtonCommand
        {
            get
            {
                return _alternateButtonCommand;
            }
            set
            {
                if (value != _alternateButtonCommand)
                {
                    _alternateButtonCommand = value;
                    OnPropertyChanged("AlternateButtonCommand");
                }
            }
        }

        public ICommand LanguagesMenuItemCommand
        {
            get
            {
                return _languagesMenuItemCommand;
            }
            set
            {
                if (value != _languagesMenuItemCommand)
                {
                    _languagesMenuItemCommand = value;
                    OnPropertyChanged("LanguagesMenuItemCommand");
                }
            }
        }

        public ICommand RateAndReviewMenuItemCommand
        {
            get
            {
                return _rateAndReviewMenuItemCommand;
            }
            set
            {
                if (value != _rateAndReviewMenuItemCommand)
                {
                    _rateAndReviewMenuItemCommand = value;
                    OnPropertyChanged("RateAndReviewMenuItemCommand");
                }
            }
        }

        public ICommand ContributeDonateMenuItemCommand
        {
            get
            {
                return _contributeDonateMenuItemCommand;
            }
            set
            {
                if (value != _contributeDonateMenuItemCommand)
                {
                    _contributeDonateMenuItemCommand = value;
                    OnPropertyChanged("ContributeDonateMenuItemCommand");
                }
            }
        }

        public ICommand TalkToTheDeveloperMenuItemCommand
        {
            get
            {
                return _talkToTheDeveloperMenuItemCommand;
            }
            set
            {
                if (value != _talkToTheDeveloperMenuItemCommand)
                {
                    _talkToTheDeveloperMenuItemCommand = value;
                    OnPropertyChanged("TalkToTheDeveloperMenuItemCommand");
                }
            }
        }

        public ICommand AboutMenuItemCommand
        {
            get
            {
                return _aboutMenuItemCommand;
            }
            set
            {
                if (value != _aboutMenuItemCommand)
                {
                    _aboutMenuItemCommand = value;
                    OnPropertyChanged("AboutMenuItemCommand");
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private double _opacity;
        private bool _isEnabled;
        private bool _isIndeterminate;
        private Visibility _visibility;
        private string _alternateButton;
        private string _languagesMenuItem;
        private string _rateAndReviewMenuItem;
        private string _contributeDonateMenuItem;
        private string _talkToTheDeveloperMenuItem;
        private string _aboutMenuItem;
        private ICommand _alternateButtonCommand;
        private ICommand _languagesMenuItemCommand;
        private ICommand _rateAndReviewMenuItemCommand;
        private ICommand _contributeDonateMenuItemCommand;
        private ICommand _talkToTheDeveloperMenuItemCommand;
        private ICommand _aboutMenuItemCommand;

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
