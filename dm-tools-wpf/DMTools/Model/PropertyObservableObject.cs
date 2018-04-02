using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DMTools.Model
{
    // Abstract implementation of INotifyPropertyChanged.
    public abstract class PropertyObservableObject : INotifyPropertyChanged
    {
        // Notify property changed.
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
