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

        [Theory]
        [InlineData(0.01, "One Cents")]
        public void ShouldRead_Cents(double input, string expect)
        {
            var read = this.service.ConvertToString(input);
            Assert.Equal(expect, read);
        }
    }
}
