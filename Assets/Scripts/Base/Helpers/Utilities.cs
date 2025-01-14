using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Base.Helpers
{
    public static class Utilities
    {
        public static void ResetTransformLocals(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static void ResetTransformLocalsButScale(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

    }

    public static class CoinAbbreviationUtility
    {
        private static readonly SortedDictionary<int, string> Abbreviations = new SortedDictionary<int, string>
        {
            {1000,"k"},
            {1000000, "M" },
            {1000000000, "B" },
        };
     
        public static string AbbreviateNumber(float number)
        {
            var abbreviatedString = "";

            if (number < 1000)
            {
                return number + "";
            }

            int roundedNumber;
            for (var i = Abbreviations.Count - 1; i > 0; i--)
            {
                
                var pair = Abbreviations.ElementAt(i);
                if (!(Mathf.Abs(number) >= pair.Key)) continue;
                roundedNumber = Mathf.FloorToInt(number / pair.Key);
                    
                abbreviatedString += roundedNumber + pair.Value + " ";
                number -= roundedNumber * pair.Key;
            }

            if (number < 1)
            {
                return abbreviatedString;
            }
            
            var lastAbbreviation = Abbreviations.ElementAt(0);
            var lastValue = lastAbbreviation.Value;
            var lastKey = lastAbbreviation.Key;
            
            roundedNumber = Mathf.FloorToInt(number / lastKey);
            var unRoundedNumber = number / lastKey;
            abbreviatedString += roundedNumber;
            
            var first2DecimalPlaces = (int)(((decimal)unRoundedNumber % 1) * 100);

            if (first2DecimalPlaces > 1)
            {
                if(first2DecimalPlaces % 10 == 0)
                    abbreviatedString += "." + $"{first2DecimalPlaces / 10}";
                else
                    abbreviatedString += "." + $"{first2DecimalPlaces:00}";
            }
            return abbreviatedString + lastValue;
        }
    }
}