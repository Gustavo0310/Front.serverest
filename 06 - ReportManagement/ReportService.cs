using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestesAlternativos._00___Setup;

namespace TestesAlternativos._06___ReportManagement
{
    public class ReportService
    {
        private DirectoryInfo TestResultFolder;
        private DirectoryInfo DatetimeFolder;
        public static string timestamp = DateTime.Now.Ticks.ToString();
        private readonly string CurrentTestFolder = TestContext.CurrentContext.TestDirectory;
        public static List<string> runSummary = new List<string>();

        public void SaveReport(string report, string outcome)
        {
            var path = GetPath();
            var name = TestContext.CurrentContext.Test.Name.Replace("á", "a").Replace("ã", "a").Replace("ó", "o").Replace("ç", "c");

            name = Regex.Replace(name, "[^0-9A-Za-z]", "");
            var filename = $"{outcome.Replace("Failed:Error", "Failed")}_{name}";

            SaveFile(report, path, $"{filename}.txt");
            SaveScreenshot(path, $"{filename}.png");

            GenerateRunSummary(outcome);
        }

        public void GenerateRunSummary(string outcome)
        {
            string line = $"{outcome} | {TestContext.CurrentContext.Test.Name}";
            SaveFile(line, GetPath().Parent, $"RunSummary_{outcome}-{SetupBase.localRunId}.txt", FileMode.Append);
        }

        private DirectoryInfo GetPath()
        {
            TestResultFolder = Directory.CreateDirectory("../../../07 - Evidencias");
            var currentClassName = TestContext.CurrentContext.Test.ClassName;

            DatetimeFolder = Directory.CreateDirectory
                (Path.Combine(TestResultFolder.FullName, DateTime.Now.Date.ToString("dd-MM-yyyy")));

            return Directory.CreateDirectory(Path.Combine(DatetimeFolder.FullName, SetupBase.localRunId, currentClassName.Split(new[] { '.' }).Last()));
        }

        public void SaveFile(string text, DirectoryInfo path, string filename, FileMode fileMode = FileMode.Create)
        {
            FileStream fs = new FileStream(Path.Combine(path.FullName, filename), fileMode, FileAccess.Write);
            using var sw = new StreamWriter(fs);
            sw.WriteLine(text);

            TestContext.AddTestAttachment(Path.Combine(path.FullName, filename));
        }

        public void SaveScreenshot(DirectoryInfo path, string filename)
        {
            Screenshot ss = ((ITakesScreenshot)SetupBase.driver).GetScreenshot();
            ss.SaveAsFile(Path.Combine(path.FullName, filename), ScreenshotImageFormat.Png);

            TestContext.AddTestAttachment(Path.Combine(path.FullName, filename));
        }
    }
}
