using ApplicatonProcess.Domain.Logics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Attributes
{
    public class AssetNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return AssetNameLogic.Instance.IsExist(value.ToString());           
        }
    }
}
