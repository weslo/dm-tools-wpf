using System.Windows;

namespace DMTools
{
    // Enum for encounter difficulty.
    public enum EncounterDifficulty
    {
        Easy,
        Medium,
        Hard,
        Deadly,
    }

    // Static class that holds balance data from the DMG.
    public static class EncounterBalanceData
    {
        // Readonly array of dictionaries that store XP threshold against encounter difficulty for each player level.
        private static readonly int[,] xpThresholdMatrix = new int[,]
        {
            { 25, 50, 75, 100 },        // 1
            { 50, 100, 150, 200 },      // 2
            { 75, 150, 225, 400 },      // 3
            { 125, 250, 375, 500 },     // 4
            { 250, 500, 750, 1100 },    // 5
            { 300, 600, 900, 1400 },    // 6
            { 350, 750, 1100, 1700 },   // 7
            { 450, 900, 1400, 2100 },   // 8
            { 550, 1100, 1600, 2400 },  // 9
            { 600, 1200, 1900, 2800 },  // 10
        };

        // Get the experience threshold for the specified level and difficulty.
        public static int GetExperienceThreshold(int level, EncounterDifficulty difficulty)
        {
            return xpThresholdMatrix[level, (int)difficulty];
        }

        // Get the experience reward for the specified challenge rating.
        public static int GetExperienceReward(string challengeRating)
        {
            var rewards = Application.Current.Resources["ExperienceRewardsByChallengeRating"] as ResourceDictionary;
            return (int)rewards[challengeRating];
        }
    }
}
