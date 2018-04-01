using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DMTools
{
    // Manages data for a single encounter.
    public class Encounter
    {
        // List of players in the encounter.
        public ObservableCollection<PlayerCharacter> PlayerCharacters
        {
            get;
        } = new ObservableCollection<PlayerCharacter>();

        // The average level of the players in the encounter.
        public int AveragePlayerLevel
        {
            get;
            private set;
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
