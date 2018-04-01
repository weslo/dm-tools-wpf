using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DMTools
{
    // Manages data for a single player character.
    public class PlayerCharacter : INotifyPropertyChanged
    {
        // Notify property changed.
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // The name of this character.
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // This character's level.
        private int _level;
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                    NotifyPropertyChanged();
            }
        }
    }
}
