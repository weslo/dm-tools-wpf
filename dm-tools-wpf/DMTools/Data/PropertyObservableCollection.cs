using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DMTools.Data
{
    /// <summary>
    /// Observable collection that emits changed events when the properties of its contents notify of changes.
    /// </summary>
    public class PropertyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Called when the collection notifies of a change.
        /// </summary>
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

        /// <summary>
        /// Handler for when an item property changes.
        /// </summary>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
