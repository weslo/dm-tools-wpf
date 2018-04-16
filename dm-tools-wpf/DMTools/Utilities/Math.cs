using System;

namespace DMTools.Utilities
{
    // Math extension functions.
    public static class Math
    {
        // Clamp an object between two bounds.
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if(value.CompareTo(min) < 0)
            {
                return min;
            }
            if(value.CompareTo(max) > 0)
            {
                return max;
            }
            return value;
        }

        // Parse a double from a fraction string.
        public static double ParseFraction(string fractionString)
        {
            // Try parsing a normal double first.
            if(double.TryParse(fractionString, out double result))
            {
                return result;
            }

            // Split the string on the forward slash.
            string[] split = fractionString.Split('/');

            // Handle improperly formatted fractions.
            if(split.Length != 2)
            {
                throw new FormatException("Input string must be in the format 'a/b'.");
            }

            // Parse parameters.
            if(double.TryParse(split[0], out double a) && double.TryParse(split[1], out double b))
            {
                return a / b;
            }
            else
            {
                throw new FormatException("In input string 'a/b', a and b must be valid doubles.");
            }
        }
    }
}
