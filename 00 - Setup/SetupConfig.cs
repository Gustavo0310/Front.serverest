using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontServerest._06___ReportManagement;

namespace FrontServerest._00___Setup
{
    public class SetupConfig
    {
        Report report = new Report();
        public static string tempoDecorridoTotal = string.Empty;
        static Stopwatch stopwatch = new Stopwatch();

        public void ConfigurarGeracaoDeRelatorio()
        {
            report.GenerateReport();

            var localReport = Report.report;

            if (TestContext.CurrentContext.Result.FailCount > 0)
                localReport.StackTrace += TestContext.CurrentContext.Result.Message ?? "";

            localReport.Outcome = TestContext.CurrentContext.Result.Outcome.ToString();

            stopwatch.Stop();
            tempoDecorridoTotal = stopwatch.Elapsed.TotalSeconds.ToString("N2");
            localReport.ElapsedTime = tempoDecorridoTotal;
            report.SaveReport(report.GetReport(localReport), localReport.Outcome);
        }

        public static void LimparVariaveis()
        {
            tempoDecorridoTotal = string.Empty;
            stopwatch = Stopwatch.StartNew();
            Report.report = new Report();
            ReportService.timestamp = string.Empty;
        }

        public void ConfigurarWebDriverChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("--start-maximized");
            chromeOptions.AddArguments("ignore-certificate-errors");
            //chromeOptions.AddArgument("--headless=new");//1 para visualizar a execução dos teste, comente essa linha. 2 Antes de subir para Azure, descomente essa linha. 
            chromeOptions.AddArguments("--whitelisted-ips=''");
            //chromeOptions.AddArguments("--window-size=1280,720");


            SetupBase.driver = new ChromeDriver(chromeOptions);
            SetupBase.wait = new WebDriverWait(SetupBase.driver, TimeSpan.FromSeconds(10));
        }

        public static void ForcarEncerramentoChromeDriver()
        {
            Process proc = new Process();
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "CMD.exe",
                Arguments = "/C taskkill /im chromedriver.exe /f",
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            proc.StartInfo = info;
            proc.Start();
        }

        public static void ForcarEncerramentoDotNetExe()
        {
            Process proc = new Process();
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "CMD.exe",
                Arguments = "/C taskkill /im dotnet.exe /f",
                CreateNoWindow = true,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            proc.StartInfo = info;
            proc.Start();
        }

        public void EncerrarWebDriverChrome()
        {
            SetupBase.driver.Close();
        }
    }
}
