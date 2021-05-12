using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Menu
{
    class MenuCaixa : GerenciadorMenu
    {

        private ControladorCaixa controladorCaixa;
        
        public MenuCaixa(ControladorCaixa controladorCaixa) : base("Caixa")
        {
            this.controladorCaixa = controladorCaixa;

            AdicionarOpcao(new MenuCaixaInserir(this));
            AdicionarOpcao(new MenuCaixaEditar(this));
            AdicionarOpcao(new MenuCaixaExcluir(this));
            AdicionarOpcao(new MenuCaixaVisualizar(this));
        }

        private bool VerificarDependenciasCaixas()
        {
            if (controladorCaixa.SelecionarCaixas().Length == 0)
            {
                Console.Clear();
                Console.WriteLine();
                ImprimirMensagem("Nenhuma Caixa cadastrada, por favor cadastre alguma", TipoMensagem.ERRO);
                Pausar();

                return false;
            }
            return true;
        }

        public void VisualizarCaixas()
        {
            string template = "{0, -3} | {1, -20} | {2, -20}";

            Console.WriteLine(template, "Id", "Cor", "Etiqueta");
            Console.WriteLine();

            Caixa[] caixas = controladorCaixa.SelecionarCaixas();

            if (caixas.Length == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada");
                return;
            }

            foreach(Caixa caixa in caixas)
            {
                Console.WriteLine(template, caixa.Id, caixa.Cor, caixa.Etiqueta);
            }
        }

        #region Opções CRUD
        private class MenuCaixaInserir : GerenciadorMenu
        {
            private MenuCaixa menuCaixa;

            public MenuCaixaInserir(MenuCaixa menuCaixa) : base("Inserir")
            {
                this.menuCaixa = menuCaixa;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Console.Write("Digite a cor da caixa: ");
                string cor = Console.ReadLine();

                Console.Write("Digite a etiqueta da caixa: ");
                string etiqueta = Console.ReadLine();

                Mensagem msg = menuCaixa.controladorCaixa.InserirCaixa(cor, etiqueta);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuCaixaEditar : GerenciadorMenu
        {
            private MenuCaixa menuCaixa;

            public MenuCaixaEditar(MenuCaixa menuCaixa) : base("Editar")
            {
                this.menuCaixa = menuCaixa;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuCaixa.VerificarDependenciasCaixas())
                    return null;

                Console.Clear();

                menuCaixa.VisualizarCaixas();
                Console.Write("\nDigite o id de qual caixa você deseja editar: ");
                int id = LerInt();

                Console.Write("Digite a cor da caixa: ");
                string cor = Console.ReadLine();

                Console.Write("Digite a etiqueta da caixa: ");
                string etiqueta = Console.ReadLine();

                Mensagem msg = menuCaixa.controladorCaixa.EditarCaixa(id, cor, etiqueta);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuCaixaExcluir : GerenciadorMenu
        {
            private MenuCaixa menuCaixa;

            public MenuCaixaExcluir(MenuCaixa menuCaixa) : base("Excluir")
            {
                this.menuCaixa = menuCaixa;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuCaixa.VerificarDependenciasCaixas())
                    return null;

                Console.Clear();

                menuCaixa.VisualizarCaixas();
                Console.Write("\nDigite o id da caixa que você deseja excluir: ");
                int id = LerInt();

                Mensagem msg = menuCaixa.controladorCaixa.ExcluirCaixa(id);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuCaixaVisualizar : GerenciadorMenu
        {
            private MenuCaixa menuCaixa;
            public MenuCaixaVisualizar(MenuCaixa menuCaixa) : base("Visualizar")
            {
                this.menuCaixa = menuCaixa;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                menuCaixa.VisualizarCaixas();
                Pausar();
                return null;
            }
        }
        #endregion
    }
}
