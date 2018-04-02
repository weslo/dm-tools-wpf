using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DMTools.Model
{
    // Observable collection that emits changed events when the properties of its contents notify of changes.
    public class PropertyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        // Called when the collection notifies of a change.
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // Remove event listeners from old items.
            if(e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach(INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= OnItemPropertyChanged;
                }
            }

            // Add event listeners to new items.
            if(e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach(INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }
            base.OnCollectionChanged(e);
        }
        
        // Handler for when an item property changes.
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
