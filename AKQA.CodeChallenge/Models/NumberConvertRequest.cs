using System;
using System.ComponentModel.DataAnnotations;

namespace AKQA.CodeChallenge.Models
{
    public class NumberConvertRequest
    {
        public NumberConvertRequest()
        {
        }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double? InputNumber
        {
            get;
            set;
        }
    }
}
