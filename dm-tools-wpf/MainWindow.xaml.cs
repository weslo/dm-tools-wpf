using System.Windows;
using System.Windows.Controls;

namespace DMTools
{
    // Interaction logic for main window.
    public partial class MainWindow : Window
    {
        // Encounter object that acts as data context.
        Encounter encounter = new Encounter();

        // Monster manual data.
        MonsterManifest monsterManual;

        // Default constructor.
        public MainWindow()
        {
            InitializeComponent();

            // Load monster manual data and insert into monsters list.
            monsterManual = MonsterManifest.LoadFromFile("Resources/monster-manual.json");
            itemsControlMonsterManifest.ItemsSource = monsterManual.MonsterDefinitions;

            // Initialize encounter as data context.
            this.DataContext = encounter;
            encounter.PlayerCharacters.Add(new PlayerCharacter
            {
                Name = "Caden Sunwalker",
                Level = 4
            });
        }

        // Called when a player character edit button is clicked.
        private void PlayerCharacterEditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PlayerCharacter pc = button.DataContext as PlayerCharacter;
            var pcWindow = new EditPlayerCharacterWindow
            {
                DataContext = pc
            };
            pcWindow.Show();
        }

        // Called when a player character remove button is clicked.
        private void PlayerCharacterRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PlayerCharacter pc = button.DataContext as PlayerCharacter;
            encounter.PlayerCharacters.Remove(pc);
        }

        // Called when the player character add button is clicked.
        private void PlayerCharacterAddButton_Click(object sender, RoutedEventArgs e)
        {
            var pc = new PlayerCharacter
            {
                Name = "New Player Character",
                Level = 1
            };
            encounter.PlayerCharacters.Add(pc);

            var pcWindow = new EditPlayerCharacterWindow
            {
                DataContext = pc
            };
            pcWindow.Show();
        }

        // Called when the monsters add button is clicked.
        private void MonstersAddButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MonsterDefinition monster = button.DataContext as MonsterDefinition;
            encounter.Monsters.Add(monster);
        }

        // Called when the monsters remove button is clicked.
        private void MonstersRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MonsterDefinition monster = button.DataContext as MonsterDefinition;
            encounter.Monsters.Remove(monster);
        }
    }
}
