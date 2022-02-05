using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thesis
{
    public class FinalView
    {
        public int Id { get; set; }
        public double OuterRadius { get; set; }
        public double InnerRadius { get; set; }
        public string TestName { get; set; }
        public string OuterColor { get; set; }
        public string InnerColor { get; set; }
        public double OrgNormalMinValue { get; set; }
        public double OrgNormalMaxValue { get; set; }
        public double OrgResultedValue { get; set; }
        public List<TestImage> testImageList { get; set; }
    }
}