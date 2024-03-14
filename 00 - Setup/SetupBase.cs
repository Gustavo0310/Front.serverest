using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesAlternativos._00___Setup
{
    [SetUpFixture]
    public class SetupBase : SetupConfig
    {
        public static ChromeDriver driver;
        public static WebDriverWait wait;
        public static string localRunId = string.Empty;

        [SetUp]
        public void ConfiguracoesGeraisPreTeste()
        {
            ConfigurarWebDriverChrome();
            LimparVariaveis();
        }

        [TearDown]
        public void ConsolidarTesteExecutado()
        {
            ConfigurarGeracaoDeRelatorio();
            EncerrarWebDriverChrome();

        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            if (localRunId == string.Empty)
                localRunId = $"Local_Run_{DateTime.Now:HHmmss}";
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            driver?.Dispose();
            //ForcarEncerramentoDotNetExe();
        }
    }
}
