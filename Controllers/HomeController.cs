using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Thesis.Controllers
{
    public class HomeController : Controller
    {
        //DBConnection conn = null;
        //public HomeController()
        //{
        //    conn = new DBConnection();
        //}
        private DataAccess conn = new DataAccess();
        public ActionResult Index()
        {
            //List<double> list = Conversion(12, .5, 8.5, 1);
            List<FinalView> obj = GetConvertedReportDetails();
            return View(obj);
        }
        public ActionResult About()
        {
            List<FinalView> obj = GetConvertedReportDetails();
            return View(obj);
        }
        public ActionResult Details(string Id, double InnerRadius, double OuterRadius, string InnerColor, string OuterColor)
        {
            FinalView finalView = new FinalView();
            string[] temp = Id.Split('-');
            int ReportId = Convert.ToInt32(temp[1]);
            ReportDetails reportDetails = conn.SelectReportDetails("Select *from ReportDetails where Id = '" + ReportId + "'").FirstOrDefault();
            finalView.testImageList = conn.GetAllImage("Select *from TestImage where ReportId = '" + ReportId + "'");
            finalView.TestName = reportDetails.TestName;
            finalView.InnerColor = InnerColor;
            finalView.InnerRadius = InnerRadius;
            finalView.OuterColor = OuterColor;
            finalView.OuterRadius = OuterRadius;
            return View(finalView);
        }
        private ReportDetails Conversion(double NormalValueMax, double NormalValueMin, double ResultedValue, int ReportId)
        {
            ReportDetails reportDetails = new ReportDetails();
            reportDetails.OrgNormalMaxValue = NormalValueMax;
            reportDetails.OrgNormalMinValue = NormalValueMin;
            reportDetails.OrgResultedValue = ResultedValue;
            double MaxValue = 0;
            double ResultedDiff;
            int countrow;
            if (NormalValueMax > ResultedValue && ResultedValue > NormalValueMin)
            {
                MaxValue = NormalValueMax;
                ResultedValue = (100 * ResultedValue) / NormalValueMax;
                NormalValueMin = (100 * NormalValueMin) / NormalValueMax;
                NormalValueMax = 100;
                ResultedDiff = ((NormalValueMax - NormalValueMin) / 2) - ResultedValue;
                countrow = conn.SelectSortedData("SELECT *FROM SortedData where ReportId='" + ReportId + "'").Count;
                if (countrow == 1)
                    conn.Update("UPDATE sortedData SET ResultedDiff='" + ResultedDiff + "' WHERE ReportId='" + ReportId + "'");
                else
                    conn.Insert("INSERT INTO sorteddata (ResultedDiff, ReportId) VALUES ('" + ResultedDiff + "', '" + ReportId + "')");
            }
            else if (ResultedValue < NormalValueMin)
            {
                MaxValue = NormalValueMin;
                ResultedValue = (100 * ResultedValue) / NormalValueMax;
                NormalValueMin = (100 * NormalValueMin) / NormalValueMax;
                NormalValueMax = 100;
                ResultedDiff = NormalValueMin - ResultedValue;
                countrow = conn.SelectSortedData("SELECT *FROM SortedData where ReportId='" + ReportId + "'").Count;
                if (countrow == 1)
                    conn.Update("UPDATE sortedData SET ResultedDiff='" + ResultedDiff + "' WHERE ReportId='" + ReportId + "'");
                else
                    conn.Insert("INSERT INTO sorteddata (ResultedDiff, ReportId) VALUES ('" + ResultedDiff + "', '" + ReportId + "')");

                MaxValue = NormalValueMin;
                ResultedValue = (100 * ResultedValue) / NormalValueMin;
                NormalValueMax = (100 * NormalValueMax) / NormalValueMin;
                NormalValueMin = 100;
                if (ResultedValue >= 0)
                    ResultedValue = 100 - ResultedValue;
                ResultedValue = Math.Abs(ResultedValue);
                if (ResultedValue >= 100)
                    ResultedValue = 100 - 20;
            }
            else
            {
                MaxValue = ResultedValue;
                NormalValueMax = (100 * NormalValueMax) / ResultedValue;
                NormalValueMin = (100 * NormalValueMin) / ResultedValue;
                ResultedValue = 100;
                ResultedDiff = ResultedValue - NormalValueMax;
                countrow = conn.SelectSortedData("SELECT *FROM SortedData where ReportId='" + ReportId + "'").Count;
                if (countrow == 1)
                    conn.Update("UPDATE sortedData SET ResultedDiff='" + ResultedDiff + "' WHERE ReportId='" + ReportId + "'");
                else
                    conn.Insert("INSERT INTO sorteddata (ResultedDiff, ReportId) VALUES ('" + ResultedDiff + "', '" + ReportId + "')");

            }
            NormalValueMax = Math.Abs(NormalValueMax);
            NormalValueMin = Math.Abs(NormalValueMin);
            ResultedValue = Math.Abs(ResultedValue);

            //##Deduct Extra Value

            //##Add Some Extra Value
            if (ResultedValue < 20)
                ResultedValue += 20;
            if (NormalValueMin < 20)
                NormalValueMin += 20;
            if (NormalValueMax < 20)
                NormalValueMax += 20;
            reportDetails.NormalMaxValue = (NormalValueMax);
            reportDetails.NormalMinValue = (NormalValueMin);
            reportDetails.ResultedValue = (ResultedValue);

            return reportDetails;
        }

        private List<FinalView> GetConvertedReportDetails()
        {
            List<ReportDetails> list = conn.SelectReportDetails("SELECT * FROM ReportDetails as RD Left JOIN SortedData as sd ON sd.ReportId = RD.Id order by sd.ResultedDiff DESC");
            List<FinalView> convertedList = new List<FinalView>();
            foreach (var item in list)
            {

                ReportDetails report = Conversion(item.NormalMaxValue, item.NormalMinValue, item.ResultedValue, item.Id);
                report.TestName = item.TestName;
                if (item.NormalMaxValue > item.ResultedValue && item.NormalMinValue < item.ResultedValue)
                {
                    FinalView finalView = new FinalView()
                    {
                        Id = item.Id,
                        TestName = item.TestName,
                        OuterRadius = report.NormalMaxValue,
                        InnerRadius = report.ResultedValue,
                        //InnerColor = (report.ResultedValue * 255).ToString(),
                        //OuterColor = (report.NormalMaxValue * 255).ToString()
                        InnerColor = "#00" + ((Convert.ToInt32(Math.Abs(report.ResultedValue-report.NormalMaxValue)) * 255) / 100).ToString("X") + "00",
                        //OuterColor = "#00" + ((Convert.ToInt32(report.NormalMaxValue) * 255)/100).ToString("X") + "00",
                        OuterColor = "#0000FF",
                        OrgResultedValue = report.OrgResultedValue,
                        OrgNormalMinValue = report.OrgNormalMinValue,
                        OrgNormalMaxValue = report.OrgNormalMaxValue
                    };
                    convertedList.Add(finalView);
                    //myCircle(X, Y, normalValueMax, c3, c2);
                    // myCircle(X, Y, rV, c2,c3 )
                }
                else if (item.ResultedValue < item.NormalMinValue)
                {
                    FinalView finalView = new FinalView()
                    {
                        Id = item.Id,
                        TestName = item.TestName,
                        OuterRadius = report.NormalMinValue,
                        InnerRadius = report.ResultedValue,
                        //InnerColor = (report.ResultedValue * 255).ToString(),
                        //OuterColor = (report.NormalMinValue * 255).ToString()
                        InnerColor = "#" + ((Convert.ToInt32(Math.Abs(report.ResultedValue-report.NormalMinValue)) * 255) / 100).ToString("X") + "0000",
                        //OuterColor = "#00" + ((Convert.ToInt32(report.NormalMinValue) * 255) / 100).ToString("X") + "00",
                        OuterColor = "#0000FF",
                        OrgResultedValue = report.OrgResultedValue,
                        OrgNormalMinValue = report.OrgNormalMinValue,
                        OrgNormalMaxValue = report.OrgNormalMaxValue
                    };
                    convertedList.Add(finalView);
                    //myCircle(X, Y, normalValueMin, c3, c2)
                    //myCircle(X, Y, rV, c2, c1)
                }

                else
                {
                    FinalView finalView = new FinalView()
                    {
                        Id = item.Id,
                        TestName = item.TestName,
                        OuterRadius = report.ResultedValue,
                        InnerRadius = report.NormalMaxValue,
                        //InnerColor = (report.NormalMaxValue * 255).ToString(),
                        //OuterColor = (report.ResultedValue * 255).ToString()
                        //InnerColor = "#00" + ((Convert.ToInt32(report.NormalMaxValue) * 255) / 100).ToString("X") + "00",
                        InnerColor = "#0000FF",
                        //OuterColor = "red",
                        OuterColor = "#" + ((Convert.ToInt32(Math.Abs(report.ResultedValue-report.NormalMaxValue)) * 255) / 100).ToString("X") + "0000",
                        OrgResultedValue = report.OrgResultedValue,
                        OrgNormalMinValue = report.OrgNormalMinValue,
                        OrgNormalMaxValue = report.OrgNormalMaxValue
                    };
                    convertedList.Add(finalView);
                    // myCircle(X, Y, rV, c2, c1)
                    //myCircle(X, Y, normalValueMax, c1, c2)
                }

            }
            return convertedList;
        }
    }
}