using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AKQA.CodeChallenge.Models
{
    public class FormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d*\.?\d*$", ErrorMessage = "The number you enter is invalid format. please try again.")]
        public double Salary { get; set; }
    }
}
