using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingFunction.Models
{
    public class ErrorAnalysisResponse
    {
        public ErrorResponse ErrorResponse { get; set; }
        public List<string> Emails { get; set; }
        public bool IsCritical { get; set; }
    }
}
