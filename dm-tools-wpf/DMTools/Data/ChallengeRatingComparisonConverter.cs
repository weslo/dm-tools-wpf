using System;
using System.Globalization;
using System.Windows.Data;

namespace DMTools.Data
{
    // Converter for comparing challenge rating to average party level.
    public class ChallengeRatingComparisonConverter : IMultiValueConverter
    {
        // Return true if the challenge rating is too dangerous for the party level.
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert challenge rating to max safe level.
            string cr = values[0] as string;
            double crValue = Utilities.Math.ParseFraction(cr);
            int maxLevel = (int)Math.Floor(crValue);

            // Get average party level from parameter.
            int partyLevel = (int)values[1];
            return maxLevel > partyLevel;
        }

        // Cannot convert back.
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
