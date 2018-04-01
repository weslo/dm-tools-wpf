using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace DMTools.Data
{
    // Definition of a monster's stats.
    public class MonsterDefinition : INotifyPropertyChanged
    {
        // Notify property changed.
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Name of the monster
        public string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Challenge rating of the monster.
        public string challenge_rating;
        public string ChallengeRating
        {
            get
            {
                return challenge_rating;
            }
            set
            {
                if (challenge_rating != value)
                {
                    challenge_rating = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    // A manifest of monster definitions.
    public class MonsterManifest
    {
        // Array of monster definitions.
        [JsonProperty]
        private MonsterDefinition[] monster_definitions;
        public MonsterDefinition[] MonsterDefinitions
        {
            get
            {
                return monster_definitions;
            }
        }

        // Load a monster manifest from a file of definitions.
        public static MonsterManifest LoadFromFile(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                try
                {
                    string contents = sr.ReadToEnd();
                    var monsterManifest = JsonConvert.DeserializeObject<MonsterManifest>(contents);
                    return monsterManifest;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
