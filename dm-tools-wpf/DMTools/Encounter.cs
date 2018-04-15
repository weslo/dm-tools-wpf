using DMTools.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace DMTools
{
    // Manages data for a single encounter.
    public class Encounter : PropertyObservableObject
    {
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

        // The difficulty of the encounter.
        private EncounterDifficulty _difficulty;
        public EncounterDifficulty Difficulty
        {
            get
            {
                return _difficulty;
            }
            private set
            {
                if(_difficulty != value)
                {
                    _difficulty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // The total experience awarded for this encounter.
        private int _totalExperienceReward;
        public int TotalExperienceReward
        {
            get
            {
                return _totalExperienceReward;
            }
            private set
            {
                if(_totalExperienceReward != value)
                {
                    _totalExperienceReward = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Default constructor.
        public Encounter()
        {
            PlayerCharacters.CollectionChanged += OnPlayerCharactersChanged;
            Monsters.CollectionChanged += OnMonstersChanged;
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

            // Reevaluate the difficulty of the encounter.
            RecalculateDifficulty();
        }

        // Called when the Monsters collection changes.
        private void OnMonstersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Prepare to recalculate monster data.
            var monsters = sender as ObservableCollection<MonsterDefinition>;

            // Recalulate total xp.
            int totalXp = 0;
            foreach(MonsterDefinition monster in monsters)
            {
                totalXp += EncounterBalanceData.GetExperienceReward(monster.ChallengeRating);
            }

            // Apply experience adjustments.
            int multiplier = EncounterBalanceData.GetExperienceMultiplier(PlayerCharacters.Count, monsters.Count);
            totalXp = totalXp * multiplier / 100;

            // Apply total experience reward.
            TotalExperienceReward = totalXp;

            // Reevaluate the difficulty of the encounter.
            RecalculateDifficulty();
        }

        // Recalculate the difficulty of the encounter.
        private void RecalculateDifficulty()
        {
            int difficultyIndex = 0;
            for(int i = 0; i < ExperienceThresholds.Length; i++)
            {
                // Check if reward exceeds threshold.
                int xpThreshold = ExperienceThresholds[i];
                if(TotalExperienceReward >= xpThreshold)
                {
                    difficultyIndex = i;
                }

                // Otherwise exit loop early.
                else
                {
                    i = ExperienceThresholds.Length;
                }
            }
            Difficulty = (EncounterDifficulty)difficultyIndex;
        }
    }
}
