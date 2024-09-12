using DocumentFormat.OpenXml.Bibliography;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontServerest._00___Setup;

namespace FrontServerest._04___Utilidades
{
    public class Driver : SetupBase
    {
        public static IWebElement BuscarElemento(By buscarPor)
        {
            IWebElement elemento = wait.Until(ExpectedConditions.ElementToBeClickable(buscarPor));

            if (elemento == null)
                Assert.Fail($"O elemento buscado não foi encontrado: {buscarPor}");

            if (!elemento.Enabled)
                Assert.Fail($"O elemento buscado está desabilitado: {buscarPor}");

            return elemento;
        }

        public static bool VerificarVisibilidadeDoElemento(By buscarPor)
        {
            try
            {
                return wait.Until(ExpectedConditions.ElementIsVisible(buscarPor)).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IWebElement BuscarElemento_PorXpath(string xpath)
        {
            return BuscarElemento(By.XPath(xpath));
        }

        public static IWebElement BuscarElemento_PorID(string id)
        {
            return BuscarElemento(By.Id(id));
        }

        public static string BuscarTextoDoElementoPorID(string id)
        {
            return BuscarElemento_PorID(id).Text;
        }

        public static string BuscarTextoDoElemento_PorXpath(string xpath)
        {
            return BuscarElemento_PorXpath(xpath).Text;
        }

        public static void RealizarClique_PorXpath(string xpath)
        {
            BuscarElemento_PorXpath(xpath).Click();
        }

        public static void RealizarClique_PorID(string id)
        {
            BuscarElemento_PorID(id).Click();
        }

        public static void RealizarCliqueSimulandoUsuario(By buscarPor)
        {
            var elemento = BuscarElemento(buscarPor);

            Actions builder = new Actions(driver);
            builder
                .MoveToElement(elemento)
                .Click()
                .Build()
                .Perform();
        }
    }
}
