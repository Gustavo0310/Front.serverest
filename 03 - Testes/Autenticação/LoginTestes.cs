using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestesAlternativos._00___Setup;
using TestesAlternativos._01___Pages.Autenticação;

namespace TestesAlternativos._03___Testes.Autenticação
{
    [TestFixture(Category = "Autenticação - Login")]
    public class LoginTestes : SetupBase
    {
        LoginPage loginPage = new LoginPage();

        [TestCase(TestName = "Realizar login com dados válidos DEVE retornar sucesso")]
        public void RealizarLoginComDadosValidosDeveRetornarSucesso()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.InserirCredenciais("gustavolmg2@gmail.com", "88687291");
            loginPage.PressionarBotaoEntrar();

            //Assert - Validação
            string nomeDoUsuario = loginPage.RetornarMensagemLoginComSucesso();
            Assert.That(nomeDoUsuario, Is.EqualTo("Login\r\nEntrar\r\nNão é cadastrado?Cadastre-se"));
            Thread.Sleep(800);
        }



        [TestCase(TestName = "Realizar login com dados inválidos DEVE retornar mensagem de erro")]
        public void RealizarLoginComDadosInvalidosDeveRetornarMensagemDeErro()
        {
            //Arrange
            loginPage.AcessarPaginaDeAutenticacao();

            //Act
            loginPage.InserirCredenciais("usuario@Errado.com", "123456");
            loginPage.PressionarBotaoEntrar();

            //Assert
            string mensagem = loginPage.RetornarMensagemDeUsuarioOuSenhaIncorreta();
            Assert.That(mensagem, Is.EqualTo("Email e/ou senha inválidos"));
            Thread.Sleep(1000);
        }
    }
}
