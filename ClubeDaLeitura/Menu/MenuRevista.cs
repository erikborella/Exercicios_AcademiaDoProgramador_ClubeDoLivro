using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Menu
{
    class MenuRevista : GerenciadorMenu
    {
        private ControladorRevista controladorRevista;
        private ControladorCaixa controladorCaixa;

        private MenuCaixa menuCaixa;

        public MenuRevista(ControladorRevista controladorRevista, ControladorCaixa controladorCaixa,
            MenuCaixa menuCaixa) : base("Revistas")
        {
            this.controladorRevista = controladorRevista;
            this.controladorCaixa = controladorCaixa;
            this.menuCaixa = menuCaixa;

            AdicionarOpcao(new MenuRevistaInserir(this));
            AdicionarOpcao(new MenuRevistaEditar(this));
            AdicionarOpcao(new MenuRevistaExcluir(this));
            AdicionarOpcao(new MenuRevistaVisualizar(this));
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

        private bool VerificarDependenciasRevistas()
        {
            if (controladorRevista.SelecionarRevistas().Length == 0)
            {
                Console.Clear();
                Console.WriteLine();
                ImprimirMensagem("Nenhuma Revista cadastrada, por favor cadastre alguma", TipoMensagem.ERRO);
                Pausar();
                return false;
            }

            return true;
        }

        public void VisualizarRevistas()
        {
            string template = "{0, -3} | {1, -20} | {2, -20} | {3, -20}";

            Console.WriteLine(template, "Id", "Tipo da Coleção", "Ano", "Etiqueta Caixa");
            Console.WriteLine();

            Revista[] revistas = controladorRevista.SelecionarRevistas();

            if (revistas.Length == 0)
            {
                Console.WriteLine("Nenhuma caixa cadastrada");
                return;
            }

            foreach(Revista revista in revistas)
            {
                Console.WriteLine(template, revista.Id, revista.TipoColecao, revista.Ano, revista.Caixa.Etiqueta);
            }
        }

        #region Opções CRUD
        private class MenuRevistaInserir: GerenciadorMenu
        {
            private MenuRevista menuRevista;

            public MenuRevistaInserir(MenuRevista menuRevista) : base("Inserir")
            {
                this.menuRevista = menuRevista;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuRevista.VerificarDependenciasCaixas())
                    return null;

                Console.Clear();

                Console.Write("Digite o tipo da coleção da revista: ");
                string tipoColecao = Console.ReadLine();

                Console.Write("Digite o ano da revista: ");
                int ano = LerInt();

                Console.WriteLine();
                menuRevista.menuCaixa.VisualizarCaixas();
                Console.Write("\nDigite o id da caixa da qual a revista pertence: ");
                int idCaixa = LerInt();

                Mensagem msg = menuRevista.controladorRevista.InserirRevista(tipoColecao, ano, idCaixa);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();
                return null;
            }
        }

        private class MenuRevistaEditar : GerenciadorMenu
        {
            private MenuRevista menuRevista;

            public MenuRevistaEditar(MenuRevista menuRevista) : base("Editar")
            {
                this.menuRevista = menuRevista;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuRevista.VerificarDependenciasRevistas())
                    return null;

                Console.Clear();
                menuRevista.VisualizarRevistas();
                Console.Write("\nDigite o id da revista que você quer editar: ");
                int id = LerInt();

                Console.Write("Digite o tipo da coleção da revista: ");
                string tipoColecao = Console.ReadLine();

                Console.Write("Digite o ano da revista: ");
                int ano = LerInt();

                Console.WriteLine();
                menuRevista.menuCaixa.VisualizarCaixas();
                Console.Write("\nDigite o id da caixa da qual a revista pertence: ");
                int idCaixa = LerInt();

                Mensagem msg = menuRevista.controladorRevista.EditarRevista(id, tipoColecao, ano, idCaixa);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuRevistaExcluir : GerenciadorMenu
        {
            MenuRevista menuRevista;

            public MenuRevistaExcluir(MenuRevista menuRevista) : base("Excluir")
            {
                this.menuRevista = menuRevista;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuRevista.VerificarDependenciasRevistas())
                    return null;

                Console.Clear();

                menuRevista.VisualizarRevistas();
                Console.Write("\nDigite o id da revista que você deseja excluir: ");
                int id = LerInt();

                Mensagem msg = menuRevista.controladorRevista.ExcluirRevista(id);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuRevistaVisualizar : GerenciadorMenu
        {
            private MenuRevista menuRevista;

            public MenuRevistaVisualizar(MenuRevista menuRevista) : base("Visualizar")
            {
                this.menuRevista = menuRevista;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                menuRevista.VisualizarRevistas();
                Pausar();
                return null;
            }
        }
        #endregion
    }
}
