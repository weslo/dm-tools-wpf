using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
