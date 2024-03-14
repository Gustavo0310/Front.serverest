using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesAlternativos._06___ReportManagement
{
    public class Report : ReportService
    {
        public string ElapsedTime { get; set; }
        public string TestMethodName { get; set; }
        public string AzureTestCaseName { get; set; }
        public string Datetime { get; set; }
        public string Outcome { get; set; }
        public string TestCategory { get; set; }
        public string StackTrace { get; set; }


        public static Report report = null;


        public void GenerateReport()
        {
            Report reportBody = new Report
            {
                Datetime = DateTime.Now.ToString(),
                Outcome = string.Empty,
                TestMethodName = TestContext.CurrentContext.Test.MethodName,
                AzureTestCaseName = TestContext.CurrentContext.Test.Name,
            };

            report = reportBody;
        }

        public string GetReport(Report report)
        {
            return $"================================================================================================== \n" +
                    $"DATA: {report.Datetime} \n" +
                    $"NOME DO MÉTODO: {report.TestMethodName} \n" +
                    $"AZURE TEST CASE: {report.AzureTestCaseName} \n" +
                    $"RESULTADO: {report.Outcome} \n" +
                    $"TEMPO TOTAL DE EXECUÇÃO: {report.ElapsedTime} segundos\n" +
                    $"================================================================================================== \n" +
                    $"                          STACK TRACE \n" +
                    $"================================================================================================== \n" +
                    $"{report.StackTrace} \n" +
                    $"================================================================================================== \n";
        }
    }
}
