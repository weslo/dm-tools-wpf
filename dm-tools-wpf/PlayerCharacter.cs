using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMTools
{
    // Manages data for a single player character.
    public class PlayerCharacter
    {
        // The name of this character.
        public string Name
        {
            get;
            set;
        }

        // This character's level.
        public int Level
        {
            get;
            set;
        }
    }
}
