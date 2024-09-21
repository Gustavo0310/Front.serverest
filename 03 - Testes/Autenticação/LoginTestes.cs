using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontServerest._00___Setup;
using FrontServerest._01___Pages.Autenticação;

namespace FrontServerest._03___Testes.Autenticação
{
    [TestFixture(Category = "Autenticação - Login")]
    public class LoginTestes : SetupBase
    {
        LoginPage loginPage = new LoginPage();

        [TestCase(TestName = "Realizar login com dados válidos DEVE ser possível")]
        public void RealizarLoginComDadosValidosDeveSerPossivel()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.InserirCredenciais("teste@gmail.com", "12345");
            loginPage.PressionarBotaoEntrar();

            //Assert - Validação
            string nomeDoUsuario = loginPage.RetornarMensagemLoginComSucesso();
            Assert.That(nomeDoUsuario, Is.EqualTo("Login\r\nEntrar\r\nNão é cadastrado?Cadastre-se"));
            Thread.Sleep(3000);
        }



        [TestCase(TestName = "Realizar login com dados inválidos não DEVE ser possível")]
        public void RealizarLoginComDadosInvalidosNaoDeveSerPoddivel()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.InserirCredenciais("usuario@Errado.com", "1234567");
            loginPage.PressionarBotaoEntrar();

            //Assert - Validação
            string mensagem = loginPage.RetornarMensagemDeUsuarioOuSenhaIncorreta();
            Assert.That(mensagem, Is.EqualTo("Email e/ou senha inválidos"));
            Thread.Sleep(2000);
        }



        [TestCase(TestName = "Cadastrar Usuário para acesso ao sistema")]
        public void CadastrarUsuarioParaAcessoAoSistema()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.PressionarBotaoCadastreSe();
            loginPage.InserirCredenciaisDeCadastroDoUsuario("Teste", "teste@gmail.com", "12345");

            //Assert - Validação
            loginPage.PressionarBotaoCadastrar();
            Thread.Sleep(5000);
        }



        [TestCase(TestName = "Validação de repetição de cadastro com Nome, E-mail e Senha já existente")]
        public void ValidacaoDeRepeticaoDeCadastroComNomeEmailEsenhaJaExistente()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.PressionarBotaoCadastreSe();
            loginPage.InserirCredenciaisDeCadastroDoUsuario("Teste", "teste@gmail.com", "12345");
            loginPage.PressionarBotaoCadastrar();

            //Assert - Validação
            string mensagem = loginPage.RetornarMensagemQueJaExisteUmCadastro();
            Assert.That(mensagem, Is.EqualTo("Este email já está sendo usado"));
            Thread.Sleep(1000);
        }



        [TestCase(TestName = "Login sem valor no campo Email não DEVE ser possível.")]
        public void LoginSemValorNoCampoEmailNaoDeveSerPossivel()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.InserirCredenciais("", "12345");
            loginPage.PressionarBotaoEntrar();

            //Assert - Validação
            string mensagem = loginPage.RetornarMensagemDeEmailObrigatorio();
            Assert.That(mensagem, Is.EqualTo("Email é obrigatório"));
            Thread.Sleep(3000);
        }



        [TestCase(TestName = "Login sem valor no campo Senha não DEVE ser possível.")]
        public void LoginSemValorNoCampoSenhaNaoDeveSerPossivel()
        {
            //Arrange - Pré requisito
            loginPage.AcessarPaginaDeAutenticacao();

            //Act - Ação do teste
            loginPage.InserirCredenciais("teste@gmail.com", "");
            loginPage.PressionarBotaoEntrar();

            //Assert - Validação
            string mensagem = loginPage.RetornarMensagemSenhaObrigatoria();
            Assert.That(mensagem, Is.EqualTo("Password é obrigatório"));
            Thread.Sleep(3000);
        }
    }
}
