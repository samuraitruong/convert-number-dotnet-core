using System;
namespace AKAQ.CodeChallenge.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class NumberConverterService : INumberConverterService
    {
        string[] NUMBERS = new string[] { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN" };
        string[] TYS = new string[] { "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
        string[] TEENS = new string[] { "", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        string[] GROUPS = new string[] {" DOLLARS", " THOUSAND", " MILLION", " BILLION", " TRILLION"};
        // I stop at 999 Trillion, because those number is huge already. Technically, we can add support for bigger number by extend this GROUPS LIST
        // Full list of number can be found here - https://www.thoughtco.com/bigger-than-a-trillion-1857463
        public NumberConverterService()
        {
        }

        public string ConvertToString(double input)
        {
            // Round number to 2 decimal place
            double roundedNumber = Math.Round(input, 2);
            double numberWithoutDecimial = Math.Floor(roundedNumber);
            // Sometime computer calculate and return very weir number ex: (0.29-0) *100. it give me 28.999999999
            double numberOfCents = Math.Round((roundedNumber - numberWithoutDecimial) * 100,0);

            string readCents = ReadNumber((int)numberOfCents, " CENTS");
            // Convert fullnumber to string, then partition it to a chunk of 3 digit and call ReadNumber 
            // Great, all the logic will be inside ReadNumber function . This approach is greate for unit test because the code only reponsible for read 3 digit number

            var fullNumberStr = numberWithoutDecimial.ToString();
            int pos = fullNumberStr.Length;
            int chunks = 0;
            string results = "";
            int chunkValue = 0;
            while(pos > 0) {
                if(chunkValue>0) {
                    results = " AND " + results;
                }

                //In the case there only 1 digit, we need to read 1 digit to chunk. 
                string chunk = fullNumberStr.Substring(Math.Max(pos - 3, 0), Math.Min(pos, 3));
                //this to append group Thousand, Milion, Bilion......
                chunkValue = int.Parse(chunk);
                string readChunk = ReadNumber(chunkValue, GROUPS[chunks]);
                pos -= 3;
                //If value is 0 for it not a end chunk, append unit it to result . eg. it should read five thousand dollars instead of 5 thousand
                // Only append DOLLARS, if chunk >1 we will ignore it. eg. 8000000 should read Eight million dollars, if not check it will read eight milions thousand dollars.

                if (chunkValue ==0 && pos>0 && chunks ==0) {
                    readChunk = GROUPS[chunks];
                }
                
                chunks++;
                
                results = readChunk + results;

            }
            //Only cent, return it without "And"
            if (string.IsNullOrEmpty(results)) return readCents;

            //if there no cents in the number, dont need to read it
            return results + (string.IsNullOrEmpty(readCents)?"" : (" AND " + readCents));
        }
        /// <summary>
        /// This function to read the number from 0-999.
        /// </summary>
        /// <returns>The number.</returns>
        /// <param name="number">Number.</param>
        /// <param name="unit">Unit : cents, thousand, hundress, million, ....</param>
        public string ReadNumber(int number, string unit="") {
            if (number == 0) return string.Empty;
            //If number from 0 to 10, just return single number read 
            if (number <= 10) return NUMBERS[number]  + unit;
            
            if(number< 20) return TEENS[number-10]  + unit;
            //the number from 20 to 99 , return read from TYS
            int ten = number % 10;
            int ty = number / 10;

            if (number < 100) {
                //if the last digit is 0, dont add a space after first number.
                return string.Format(ten == 0?"{0}":"{0}-{1}", TYS[ty-1], NUMBERS[ten]) + unit;
            }
            int hundres = number / 100;
            //reuse the logic for reading number <100 that we already test and work before, in this case, Unit is empty 
            string secondPart = ReadNumber(number % 100);

            return NUMBERS[hundres] + " HUNDRED" + (string.IsNullOrEmpty(secondPart)?"":" AND " + secondPart) + unit;

        }
    }
}
