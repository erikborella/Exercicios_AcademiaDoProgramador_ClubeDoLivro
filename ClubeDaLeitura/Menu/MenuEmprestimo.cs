using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Menu
{
    class MenuEmprestimo : GerenciadorMenu
    {
        private ControladorEmprestimo controladorEmprestimo;
        private ControladorAmiguinho controladorAmiguinho;
        private ControladorRevista controladorRevista;

        private MenuAmiguinho menuAmiguinho;
        private MenuRevista menuRevista;

        public string template = "{0, -3} | {1, -20} | {2, -20} | {3, -20} | {4, -20}";

        public MenuEmprestimo(ControladorEmprestimo controladorEmprestimo, ControladorAmiguinho controladorAmiguinho,
            ControladorRevista controladorRevista, MenuAmiguinho menuAmiguinho, MenuRevista menuRevista) : base("Emprestimos")
        {
            this.controladorEmprestimo = controladorEmprestimo;
            this.controladorAmiguinho = controladorAmiguinho;
            this.controladorRevista = controladorRevista;

            this.menuAmiguinho = menuAmiguinho;
            this.menuRevista = menuRevista;

            AdicionarOpcao(new MenuEmprestimoRealizar(this));
            AdicionarOpcao(new MenuEmprestimosDevolver(this));
            AdicionarOpcao(new MenuEmprestimoVisualizar(controladorEmprestimo, this));
        }

        private bool VerificarDependenciaAmiguinhos()
        {
            if (controladorAmiguinho.SelecionarAmiguinhos().Length == 0)
            {
                Console.Clear();
                Console.WriteLine();
                ImprimirMensagem("Nenhum Amiguinho cadastrado, por favor cadastre alguma", TipoMensagem.ERRO);
                Pausar();

                return false;
            }

            return true;
        }

        private bool VerificarDependenciaRevistas()
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

        private bool VerificarDependenciaEmprestimos()
        {
            if (controladorEmprestimo.SelecionarEmprestimos().Length == 0)
            {
                Console.Clear();
                Console.WriteLine();
                ImprimirMensagem("Nenhum Emprestimo cadastrado, por favor cadastre alguma", TipoMensagem.ERRO);
                Pausar();

                return false;
            }

            return true;
        }

        public void VisualizarEmprestimos(Emprestimo[] emprestimos)
        {
            Console.WriteLine(template, "Id", "Amiguinho", "Revista", "Data Emprestimo", "Data devolução");
            Console.WriteLine();

            if (emprestimos.Length == 0)
            {
                ImprimirMensagem("Nenhum emprestimo encontrado", TipoMensagem.ERRO);
                return;
            }


            foreach(Emprestimo emprestimo in emprestimos)
            {
                string dataDevolucao = (emprestimo.DataDevolucao == DateTime.MinValue) ? 
                    "Não devolvido" : emprestimo.DataDevolucao.ToString();

                Console.WriteLine(template, emprestimo.Id, emprestimo.Amiguinho.Nome, emprestimo.Revista.TipoColecao, 
                    emprestimo.DataEmprestimo, dataDevolucao);
            }
        }

        #region Opções CRUD
        private class MenuEmprestimoRealizar : GerenciadorMenu
        {
            private MenuEmprestimo menuEmprestimo;

            public MenuEmprestimoRealizar(MenuEmprestimo menuEmprestimo) : base("Realizar novo emprestimo")
            {
                this.menuEmprestimo = menuEmprestimo;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuEmprestimo.VerificarDependenciaAmiguinhos())
                    return null;

                if (!menuEmprestimo.VerificarDependenciaRevistas())
                    return null;

                Console.Clear();
                menuEmprestimo.menuAmiguinho.VisualizarAmiguinhos();
                Console.Write("\nDigite o id do amiguinho que você quer emprestar: ");
                int amiguinhoId = LerInt();

                Console.WriteLine();

                menuEmprestimo.menuRevista.VisualizarRevistas();
                Console.Write("\nDigite o id da revista que você deseja emprestar: ");
                int revistaId = LerInt();

                Mensagem msg = menuEmprestimo.controladorEmprestimo.RealizarEmprestimo(amiguinhoId, revistaId);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuEmprestimosDevolver : GerenciadorMenu
        {
            private MenuEmprestimo menuEmprestimo;

            public MenuEmprestimosDevolver(MenuEmprestimo menuEmprestimo) : base("Devolver Emprestimo")
            {
                this.menuEmprestimo = menuEmprestimo;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuEmprestimo.VerificarDependenciaEmprestimos())
                    return null;

                Console.Clear();

                Emprestimo[] naoDevolvidos = menuEmprestimo.controladorEmprestimo.SelecionarEmprestimosNaoDevolvidos();
                menuEmprestimo.VisualizarEmprestimos(naoDevolvidos);

                Console.Write("\nDigite o id do emprestimo que você deseja devolver: ");
                int id = LerInt();

                Mensagem msg = menuEmprestimo.controladorEmprestimo.DevolverEmprestimo(id);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        #endregion
    }
}
