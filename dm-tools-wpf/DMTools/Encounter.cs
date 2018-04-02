using DMTools.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DMTools
{
    // Manages data for a single encounter.
    public class Encounter : PropertyObservableObject
    {
        // Carries data about a cluster of a single monster definition in an encounter.
        public class MonsterCluster : PropertyObservableObject
        {
            public int id;
            public int count;
        }

        // List of players in the encounter.
        public PropertyObservableCollection<PlayerCharacter> PlayerCharacters
        {
            get;
        } = new PropertyObservableCollection<PlayerCharacter>();

        // List of monster clusters in the encounter.
        public PropertyObservableCollection<MonsterDefinition> Monsters
        {
            get;
        } = new PropertyObservableCollection<MonsterDefinition>();

        // The average level of the players in the encounter.
        private int _averagePlayerLevel;
        public int AveragePlayerLevel
        {
            get
            {
                return _averagePlayerLevel;
            }
            private set
            {
                if(_averagePlayerLevel != value)
                {
                    _averagePlayerLevel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // The XP thresholds for this encounter.
        private int[] _experienceThresholds;
        public int[] ExperienceThresholds
        {
            get
            {
                return _experienceThresholds;
            }
            private set
            {
                if(_experienceThresholds != value)
                {
                    _experienceThresholds = value;
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
            // Prepare to recalculate player data.
            var pcs = sender as ObservableCollection<PlayerCharacter>;

            // Count player levels.
            int totalPlayerLevel = 0;

            // Measure XP threshold for each difficulty.
            var difficulties = Enum.GetValues(typeof(EncounterDifficulty)) as EncounterDifficulty[];
            int[] experienceThresholds = new int[difficulties.Length];

            // Do math on each player's data.
            foreach (PlayerCharacter pc in pcs)
            {

                // Update total level.
                totalPlayerLevel += pc.Level;

                // Update XP threshold.
                foreach(var difficulty in difficulties)
                {
                    int experienceThreshold = EncounterBalanceData.GetExperienceThreshold(pc.Level, difficulty);
                    experienceThresholds[(int)difficulty] += experienceThreshold;
                }
            }

            // Recalculate average player level.
            double preciseAveragePlayerLevel = (double)totalPlayerLevel / pcs.Count;
            AveragePlayerLevel = (int)Math.Round(preciseAveragePlayerLevel);

            // Update experience thresholds.
            ExperienceThresholds = experienceThresholds;
        }
    }
}
