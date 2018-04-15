using System.Windows;
using DMTools.Utilities;

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

        // Return the experience multiplier for a given number of encounter participants.
        public static int GetExperienceMultiplier(int numPlayers, int numMonsters)
        {
            // Handle weird base case.
            if (numMonsters <= 0)
            {
                return 0;
            }

            // Determine experience multiplier tier.
            int multiplierTier;
            if(numMonsters == 1)
            {
                multiplierTier = 1;
            }
            else if(numMonsters == 2)
            {
                multiplierTier = 2;
            }
            else if(numMonsters <= 6)
            {
                multiplierTier = 3;
            }
            else if(numMonsters <= 10)
            {
                multiplierTier = 4;
            }
            else if(numMonsters <= 14)
            {
                multiplierTier = 5;
            }
            else
            {
                multiplierTier = 6;
            }

            // Adjust multiplier tier for the number of players.
            if(numPlayers < 3)
            {
                multiplierTier++;
            }
            else if(numPlayers > 5)
            {
                multiplierTier--;
            }

            // Clamp to defined bounds.
            var multipliers = Application.Current.Resources["ExperienceRewardMultipliers"] as ResourceDictionary;
            int minTier = (int)multipliers["CONST_MIN_TIER"];
            int maxTier = (int)multipliers["CONST_MAX_TIER"];
            multiplierTier = multiplierTier.Clamp(minTier, maxTier);

            // Get experience multiplier
            return (int)multipliers[multiplierTier.ToString()];
        }
    }
}
