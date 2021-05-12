using System;
using System.Reflection;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu;
using ClubeDaLeitura.Menu.Base;

namespace ClubeDaLeitura
{
    class Program
    {
        static void Main(string[] args)
        {
            int tamanhoMaximo = 1000;

            ControladorCaixa controladorCaixa = new ControladorCaixa(tamanhoMaximo);
            ControladorRevista controladorRevista = new ControladorRevista(controladorCaixa, tamanhoMaximo);
            ControladorAmiguinho controladorAmiguinho = new ControladorAmiguinho(tamanhoMaximo);
            ControladorEmprestimo controladorEmprestimo = new ControladorEmprestimo(controladorAmiguinho, 
                controladorRevista, tamanhoMaximo);

            MenuPrincipal principal = new MenuPrincipal(controladorCaixa, controladorAmiguinho, 
                controladorRevista, controladorEmprestimo);

            ExecutarMenu(principal);
        }

        private static void ExecutarMenu(GerenciadorMenu menu)
        {
            while (true)
            {
                GerenciadorMenu proximoMenu = menu.Executar();

                if (proximoMenu is OpcaoVoltar || proximoMenu == null)
                    break;

                ExecutarMenu(proximoMenu);
            }
        }
    }
}
