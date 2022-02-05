using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thesis
{
    public class ReportDetails
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public double NormalMinValue { get; set; }
        public double NormalMaxValue { get; set; }
        public double ResultedValue { get; set; }
        public double OrgNormalMinValue { get; set; }
        public double OrgNormalMaxValue { get; set; }
        public double OrgResultedValue { get; set; }
        
    }
}