using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursementsMd3.Entities
{
    public class PensionerDetails
    {
        public string Name { get; set; }
        public DateTime dateOfBirth { get; set; }
        public long AadharNumber { get; set; }
        public string PAN { get; set; }
        public double salaryEarned { get; set; }
        public double allowances { get; set; }
        public string pensionType { get; set; }
        public string bankName { get; set; }
        public string accountNumber { get; set; }
        public string bankType { get; set; }
    }
}
