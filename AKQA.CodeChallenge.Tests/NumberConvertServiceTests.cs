using System;
using AKAQ.CodeChallenge.Business;
using Xunit;

namespace AKQA.CodeChallenge.Tests
{
    public class NumberConvertServiceTests
    {
        readonly INumberConverterService service;

        public NumberConvertServiceTests() {
            service = new NumberConverterService();
        }
        // with xUnit we can using CSV or JSON Data, in this example I just use inline for easy read and change it.
        [Theory]
        [InlineData(0.00, "")]
        [InlineData(0.01, "ONE CENTS")]
        [InlineData(0.1, "TEN CENTS")]
        [InlineData(0.8, "EIGHTY CENTS")]
        [InlineData(0.09, "NINE CENTS")]
        [InlineData(0.29, "TWENTY-NINE CENTS")]
        [InlineData(0.101, "TEN CENTS")]
        [InlineData(0.11, "ELEVEN CENTS")]
        [InlineData(0.15, "FIFTEEN CENTS")]
        [InlineData(0.20, "TWENTY CENTS")]
        [InlineData(0.25, "TWENTY-FIVE CENTS")]
        [InlineData(0.99, "NINETY-NINE CENTS")]
        [InlineData(1.00, "ONE DOLLARS")]
        [InlineData(1.50, "ONE DOLLARS AND FIFTY CENTS")]
        [InlineData(12.65, "TWELVE DOLLARS AND SIXTY-FIVE CENTS")]
        [InlineData(20.39, "TWENTY DOLLARS AND THIRTY-NINE CENTS")]
        [InlineData(100, "ONE HUNDRED DOLLARS")]
        [InlineData(100.68, "ONE HUNDRED DOLLARS AND SIXTY-EIGHT CENTS")]
        [InlineData(557.29, "FIVE HUNDRED AND FIFTY-SEVEN DOLLARS AND TWENTY-NINE CENTS")]
        [InlineData(1000, "ONE THOUSAND DOLLARS")]
        [InlineData(5000.15, "FIVE THOUSAND DOLLARS AND FIFTEEN CENTS")]
        [InlineData(9432.46, "NINE THOUSAND AND FOUR HUNDRED AND THIRTY-TWO DOLLARS AND FOURTY-SIX CENTS")]
        [InlineData(19559.37, "NINETEEN THOUSAND AND FIVE HUNDRED AND FIFTY-NINE DOLLARS AND THIRTY-SEVEN CENTS")]
        [InlineData(43559.00, "FOURTY-THREE THOUSAND AND FIVE HUNDRED AND FIFTY-NINE DOLLARS")]
        [InlineData(50000, "FIFTY THOUSAND DOLLARS")]
        [InlineData(50000.14, "FIFTY THOUSAND DOLLARS AND FOURTEEN CENTS")]
        [InlineData(800000.2, "EIGHT HUNDRED THOUSAND DOLLARS AND TWENTY CENTS")]
        [InlineData(7000000, "SEVEN MILLION DOLLARS")]
        [InlineData(7654321.12, "SEVEN MILLION AND SIX HUNDRED AND FIFTY-FOUR THOUSAND AND THREE HUNDRED AND TWENTY-ONE DOLLARS AND TWELVE CENTS")]
        [InlineData(13654321.12, "THIRTEEN MILLION AND SIX HUNDRED AND FIFTY-FOUR THOUSAND AND THREE HUNDRED AND TWENTY-ONE DOLLARS AND TWELVE CENTS")]
        [InlineData(23734321.18, "TWENTY-THREE MILLION AND SEVEN HUNDRED AND THIRTY-FOUR THOUSAND AND THREE HUNDRED AND TWENTY-ONE DOLLARS AND EIGHTEEN CENTS")]
        [InlineData(10000000, "TEN MILLION DOLLARS")]
        [InlineData(200000000, "TWO HUNDRED MILLION DOLLARS")]
        [InlineData(200000012, "TWO HUNDRED MILLION AND TWELVE DOLLARS")]
        [InlineData(200120012, "TWO HUNDRED MILLION AND ONE HUNDRED AND TWENTY THOUSAND AND TWELVE DOLLARS")]
        [InlineData(123456789.88, "ONE HUNDRED AND TWENTY-THREE MILLION AND FOUR HUNDRED AND FIFTY-SIX THOUSAND AND SEVEN HUNDRED AND EIGHTY-NINE DOLLARS AND EIGHTY-EIGHT CENTS")]
        [InlineData(1000000000, "ONE BILLION DOLLARS")]
        [InlineData(55000000000, "FIFTY-FIVE BILLION DOLLARS")]
        [InlineData(999999999999.99, "NINE HUNDRED AND NINETY-NINE BILLION AND NINE HUNDRED AND NINETY-NINE MILLION AND NINE HUNDRED AND NINETY-NINE THOUSAND AND NINE HUNDRED AND NINETY-NINE DOLLARS AND NINETY-NINE CENTS")]
        [InlineData(7000000000000, "SEVEN TRILLION DOLLARS")]
        public void ShouldRead_Cents(double input, string expect)
        {
            var read = this.service.ConvertToString(input);
            Assert.Equal(expect, read);
        }
        [Fact]
        public void ShouldRead_Number_From_1_To_20()
        {
            string[] data = new string[] {"ONE","TWO","THREE","FOUR","FIVE",
                        "SIX","SEVEN","EIGHT","NINE","TEN","ELEVEN","TWELVE",
                        "THIRTEEN","FOURTEEN","FIFTEEN","SIXTEEN","SEVENTEEN",
                        "EIGHTEEN","NINETEEN","TWENTY" };
            for(var i =1; i<= 20; i++) {
                var read = this.service.ConvertToString((double)i/100);
                Assert.Equal(data[i-1] +" CENTS", read);

                read = this.service.ConvertToString(i);
                Assert.Equal(data[i - 1] + " DOLLARS", read);
            }

        }
    }
}
