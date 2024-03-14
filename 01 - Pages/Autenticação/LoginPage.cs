using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesAlternativos._04___Utilidades;

namespace TestesAlternativos._01___Pages.Autenticação
{
    public class LoginPage : Driver
    {
        public void RealizarLoginComSucesso()
        {
            AcessarPaginaDeAutenticacao();
            InserirCredenciais("gustavolmg2@gmail.com", "88687291");
            PressionarBotaoEntrar();
            RetornarMensagemLoginComSucesso();
        }

        public float soma()
        {
            return 1 + 1.5f;
        }


        public void AcessarPaginaDeAutenticacao()
        {
            driver.Navigate().GoToUrl("https://front.serverest.dev/login");
        }

        public void InserirCredenciais(string email, string password)
        {
            BuscarElemento_PorID("email").SendKeys(email);
            BuscarElemento_PorID("password").SendKeys(password);
        }

        public void PressionarBotaoEntrar()
        {
            RealizarClique_PorXpath("//*[@id=\"root\"]/div/div/form/button");
        }

        public string RetornarMensagemLoginComSucesso()
        {
            return BuscarTextoDoElemento_PorXpath("//*[@id=\"root\"]");
        }

        public string RetornarMensagemDeUsuarioOuSenhaIncorreta()
        {
            return BuscarTextoDoElemento_PorXpath("//*[@id=\"root\"]/div/div/form/div[1]/span");
        }
    }
}
