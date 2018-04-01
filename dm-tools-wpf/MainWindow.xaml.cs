using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Encounter object that acts as data context.
        Encounter encounter = new Encounter();

        // Default constructor.
        public MainWindow()
        {
            InitializeComponent();
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

        // Called whe nthe player character add button is clicked.
        private void PlayerCharacterAddButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerCharacter pc = new PlayerCharacter();
            encounter.PlayerCharacters.Add(pc);
            var pcWindow = new EditPlayerCharacterWindow
            {
                DataContext = pc
            };
            pcWindow.Show();
        }
    }
}
