using DMTools.Data;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DMTools
{
    // Manages data for a single encounter.
    public class Encounter : INotifyPropertyChanged
    {
        // Notify property changed.
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // List of players in the encounter.
        public PropertyObservableCollection<PlayerCharacter> PlayerCharacters
        {
            get;
        } = new PropertyObservableCollection<PlayerCharacter>();

        // The average level of the players in the encounter.
        private int _averagePlayerLevel;
        public int AveragePlayerLevel
        {
            get
            {
                return _averagePlayerLevel;
            }
            set
            {
                if(_averagePlayerLevel != value)
                {
                    _averagePlayerLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Default constructor.
        public Encounter()
        {
            PlayerCharacters.CollectionChanged += OnPlayerCharactersChanged;
        }

        // Called when the PlayerCharacters collection changes.
        private void OnPlayerCharactersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Recalculate average player level.
            var pcs = sender as ObservableCollection<PlayerCharacter>;
            int totalPlayerLevel = 0;
            foreach(PlayerCharacter pc in pcs)
            {
                totalPlayerLevel += pc.Level;
            }
            double preciseAveragePlayerLevel = (double)totalPlayerLevel / pcs.Count;
            AveragePlayerLevel = (int)Math.Round(preciseAveragePlayerLevel);
        }
    }
}
