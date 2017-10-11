using System;
namespace AKAQ.CodeChallenge.Business
{
    public class NumberConverterService : INumberConverterService
    {
        string[] NUMBERS = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };

        public NumberConverterService()
        {
        }

        public string ConvertToString(double input)
        {
            //Round number to 2 decimal place
            double roundedNumber = Math.Round(input, 2);
            double numberWithoutDecimial = Math.Floor(roundedNumber);
            double numberOfCents = (roundedNumber - numberWithoutDecimial) * 100;


            return ReadNumber((int)numberOfCents, "Cents");
        }
        /// <summary>
        /// This function to read the number from 0-999.
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="number">Number.</param>
        /// <param name="unit">Unit : cents, thousand, hundress, million, ....</param>
        public string ReadNumber(int number, string unit) {
            string result = "";

            int number1 = number % 10;

            int number2 = (number-number1) / 100;
            int number3 = (number - number1-number2) % 100;

            if(number1 >0) {
                result += NUMBERS[number1];
            }

            return result + " " + unit;

        }
    }
}
