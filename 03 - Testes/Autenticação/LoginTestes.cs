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
            loginPage.InserirCredenciais("testelogin@gmail.com", "12345");
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
            loginPage.InserirCredenciais("usuario@Errado.com", "1234567");
            loginPage.PressionarBotaoEntrar();

            //Assert
            string mensagem = loginPage.RetornarMensagemDeUsuarioOuSenhaIncorreta();
            Assert.That(mensagem, Is.EqualTo("Email e/ou senha inválidos"));
            Thread.Sleep(1000);
        }



        [TestCase(TestName = "Cadastrar Usuário para acesso ao sistema")]
        public void CadastrarUsuarioParaAcessoAoSistema()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.PressionarBotaoCadastreSe();
            loginPage.InserirCredenciaisDeCadastroDoUsuario("Teste", "testelogin@gmail.com", "12345");
            loginPage.PressionarBotaoCadastrar();
            Thread.Sleep(5000);
        }



        [TestCase(TestName = "Mensagem que ja existe um cadastro")]
        public void MensagemQueJaExiteUmCadastro()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.PressionarBotaoCadastreSe();
            loginPage.InserirCredenciaisDeCadastroDoUsuario("Teste", "testelogin@gmail.com", "12345");
            loginPage.PressionarBotaoCadastrar();

            //Assert
            string mensagem = loginPage.RetornarMensagemQueJaExisteUmCadastro();
            Assert.That(mensagem, Is.EqualTo("Este email já está sendo usado"));
            Thread.Sleep(3000);
        }
    }
}
