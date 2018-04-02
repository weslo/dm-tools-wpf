using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using DMTools.Model;

namespace DMTools
{
    // Serialized monster data.
    public struct SerializableMonsterDefinition
    {
        public string name;
        public string challenge_rating;
    }

    // List of serialized monster data.
    public struct SerializableMonsterDefinitionList
    {
        public SerializableMonsterDefinition[] monster_definitions;
    }

    // Definition of a monster's stats.
    public class MonsterDefinition : PropertyObservableObject
    {
        // ID of this monster (index in manifest).
        public int ID
        {
            get;
            private set;
        }

        // Name of the monster.
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

        // Challenge rating of the monster.
        private string _challengeRating;
        public string ChallengeRating
        {
            get
            {
                return _challengeRating;
            }
            set
            {
                if (_challengeRating != value)
                {
                    _challengeRating = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Constructor requires an ID.
        public MonsterDefinition(int id)
        {
            ID = id;
        }
    }

    // A manifest of monster definitions.
    public class MonsterManifest : PropertyObservableObject
    {
        // Key monster definitions against their string name.
        private MonsterDefinition[] monster_definitions;
        public MonsterDefinition[] MonsterDefinitions
        {
            get
            {
                return monster_definitions;
            }
            set
            {
                if(monster_definitions != value)
                {
                    monster_definitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Load a monster manifest from a file of definitions.
        public static MonsterManifest LoadFromFile(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                try
                {
                    // Load contents into an array of serialized monster data.
                    string contents = sr.ReadToEnd();
                    var serializedMonsterDataObject = JsonConvert.DeserializeObject<SerializableMonsterDefinitionList>(contents);
                    var serializedMonsterData = serializedMonsterDataObject.monster_definitions;

                    // Copy serialized data into dictionary of monster definitions.
                    var monsterDefinitions = new MonsterDefinition[serializedMonsterData.Length];
                    for(int i = 0; i < monsterDefinitions.Length; ++i)
                    {
                        var monster = serializedMonsterData[i];
                        monsterDefinitions[i] = new MonsterDefinition(i)
                        {
                            Name = monster.name,
                            ChallengeRating = monster.challenge_rating,
                        };
                    }

                    // Return manifest object with definitions assigned.
                    return new MonsterManifest
                    {
                        MonsterDefinitions = monsterDefinitions,
                    };
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
