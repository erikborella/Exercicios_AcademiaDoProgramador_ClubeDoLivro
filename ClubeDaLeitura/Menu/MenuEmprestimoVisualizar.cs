using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Menu
{
    class MenuEmprestimoVisualizar : GerenciadorMenu
    {
        private ControladorEmprestimo controladorEmprestimo;
        private MenuEmprestimo menuEmprestimo;

        public MenuEmprestimoVisualizar(ControladorEmprestimo controladorEmprestimo, 
            MenuEmprestimo menuEmprestimo) : base("Vizualizar Emprestimos")
        {
            this.controladorEmprestimo = controladorEmprestimo;
            this.menuEmprestimo = menuEmprestimo;

            AdicionarOpcao(new MenuEmprestimoVisualizarTodos(this));
            AdicionarOpcao(new MenuEmprestimoVisualizarAbertos(this));
            AdicionarOpcao(new MenuEmprestimoVisualizarAbertosDia(this));
            AdicionarOpcao(new MenuEmprestimoVisualizarMes(this));
        }

        #region Opções
        private class MenuEmprestimoVisualizarTodos : GerenciadorMenu
        {
            private MenuEmprestimoVisualizar menuVizualizar;

            public MenuEmprestimoVisualizarTodos(MenuEmprestimoVisualizar menuVizualizar) : base("Visualizar todos os emprestimos")
            {
                this.menuVizualizar = menuVizualizar;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Emprestimo[] naoDevolvidos = menuVizualizar.controladorEmprestimo.SelecionarEmprestimos();
                menuVizualizar.menuEmprestimo.VisualizarEmprestimos(naoDevolvidos);

                Console.WriteLine();
                Pausar();

                return null;
            }
        }

        private class MenuEmprestimoVisualizarAbertos : GerenciadorMenu
        {
            private MenuEmprestimoVisualizar menuVisualizar;

            public MenuEmprestimoVisualizarAbertos(MenuEmprestimoVisualizar menuVisualizar) : base("Ver Emprestimos abertos")
            {
                this.menuVisualizar = menuVisualizar;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Emprestimo[] naoDevolvidos = menuVisualizar.controladorEmprestimo.SelecionarEmprestimosNaoDevolvidos();
                menuVisualizar.menuEmprestimo.VisualizarEmprestimos(naoDevolvidos);

                Console.WriteLine();
                Pausar();

                return null;
            }
        }

        private class MenuEmprestimoVisualizarAbertosDia : GerenciadorMenu
        {
            private MenuEmprestimoVisualizar menuVisualizar;

            public MenuEmprestimoVisualizarAbertosDia(MenuEmprestimoVisualizar menuVisualizar) : base("Visualizar emprestimo abertos de hoje")
            {
                this.menuVisualizar = menuVisualizar;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Emprestimo[] emprestimosHoje = menuVisualizar.controladorEmprestimo.SelecionarEmprestimosAbertoDia();
                menuVisualizar.menuEmprestimo.VisualizarEmprestimos(emprestimosHoje);

                Console.WriteLine();
                Pausar();

                return null;
            }
        }

        private class MenuEmprestimoVisualizarMes : GerenciadorMenu
        {
            private MenuEmprestimoVisualizar menuVizualizar;

            public MenuEmprestimoVisualizarMes(MenuEmprestimoVisualizar menuVizualizar) : base("Vizualizar Emprestimos do Mes")
            {
                this.menuVizualizar = menuVizualizar;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Emprestimo[] emprestimosHoje = menuVizualizar.controladorEmprestimo.SelecionarEmprestimosAbertoMes();
                menuVizualizar.menuEmprestimo.VisualizarEmprestimos(emprestimosHoje);

                Console.WriteLine();
                Pausar();

                return null;
            }
        }
        #endregion
    }

}
