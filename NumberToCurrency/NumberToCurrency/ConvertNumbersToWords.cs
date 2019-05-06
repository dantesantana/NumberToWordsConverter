using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToCurrency
{
    //Code adapted from http://www.blackwasp.co.uk/NumberToWords.aspx
    class ConverterClass
    {
        // Single-digit and small number names
        private string[] _smallNumbers = new string[]
            { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE",
      "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN",
      "EIGHTEEN", "NINETEEN"};

        // Tens excluding small numbers
        private string[] _tens = new string[] { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        // Scale number names for use during recombination
        private string[] _scaleNumbers = new string[] { "", "THOUSAND", "MILLION", "BILLION" };

        //returns a 3 digit number in words e.g. 123 --> ONE HUNDRED AND TWENTY THREE
        private string GroupToWords(int groupInt)
        {
            string groupText = "";

            // Calculate the number of "hundreds" in groupInt 
            int hundreds = groupInt / 100;
            int tensUnits = groupInt % 100;

            // Hundreds Rule
            if (hundreds != 0)
            {
                groupText += _smallNumbers[hundreds] + " HUNDRED";
                if (tensUnits != 0)
                {
                    groupText += " AND ";
                }
            }

            // Similar to hundreds
            int tens = tensUnits / 10;
            int units = tensUnits % 10;

            // Tens Rule
            if (tens >= 2)
            {
                groupText += _tens[tens];
                if (units != 0)
                {
                    groupText += " " + _smallNumbers[units];
                }
            }
            else if (tensUnits != 0)
            {
                groupText += _smallNumbers[tensUnits];
            }
            return groupText;
        }

        private string Convert(string NumberString)
        {
            if (NumberString == "0" || NumberString == "00")
            {
                return _smallNumbers[0];
            }

            //convert to numeric for manipulation
            int NumberInt = int.Parse(NumberString);

            //only 4 digit groups required due to the limitations of using 32-bit integers i.e. numbers will never be larger than this
            int[] digitGroups = new int[4];
            int positiveNumber = Math.Abs(NumberInt);
            for (int i = 0; i < 4; i++)
            {
                //Modulus 1000 of a given number returns the last 3 digits of that number
                digitGroups[i] = positiveNumber % 1000;
                positiveNumber /= 1000;
            }

            //convert each 3 digit group to words
            string[] groupText = new string[4];
            for (int i = 0; i < 4; i++)
            {
                groupText[i] = GroupToWords(digitGroups[i]);
            }

            //Recombine all 3-digit groups
            string combined = groupText[0];
            //used to describe sentence structure e.g. FIVE HUNDRED "AND" ONE
            bool appendAnd = (digitGroups[0] > 0) && (digitGroups[0] < 100);

            for (int i = 1; i < 4; i++)
            {
                if (digitGroups[i] != 0)
                {
                    string prefix = groupText[i] + " " + _scaleNumbers[i];
                    if (combined.Length != 0)
                    {
                        prefix += appendAnd ? " and " : ", ";
                    }
                    //double check if this is needed
                    appendAnd = false;
                    combined = prefix + combined;
                }
            }

            if (NumberInt < 0)
            {
                combined = "NEGATIVE " + combined;
            }

            return combined;
        }

        public string ConvertNumbersToWords(string NumberString)
        {
            //Works with numbers from -2147483647.99 to 2147483647.99 due to the limitations of 32 bit integers
            string input = NumberString;
            if (!input.Contains('.'))
            {
                return Convert(input) + " DOLLARS";
            }
            else
            {
                //separate dollar and cents values for individual conversions
                string dollars = input.Substring(0, input.IndexOf('.'));
                string cents = input.Substring(input.IndexOf('.') + 1);
                return Convert(dollars) + " DOLLARS AND " + Convert(cents) + " CENTS";
            }
        }
    }
}
