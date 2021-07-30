using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Models
{
    public class ErrorMessagesResponse
    {
        public int ErrorCode { get; set; }

        public List<string> Messages { get; set; } = new List<string>();
    }
}
