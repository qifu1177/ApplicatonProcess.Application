using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Attributes
{
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString().ToLower(), @".+@{1}\w+\.{1}\w{2,}");
        }
    }
}
