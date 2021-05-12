using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador;
using ClubeDaLeitura.Menu.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Menu
{
    class MenuAmiguinho : GerenciadorMenu
    {
        private ControladorAmiguinho controladorAmiguinho;

        public MenuAmiguinho(ControladorAmiguinho controladorAmiguinho) : base("Amiguinho")
        {
            this.controladorAmiguinho = controladorAmiguinho;

            AdicionarOpcao(new MenuAmiguinhoInserir(this));
            AdicionarOpcao(new MenuAmiguinhoEditar(this));
            AdicionarOpcao(new MenuAmiguinhoExcluir(this));
            AdicionarOpcao(new MenuAmiguinhoVisualizar(this));
        }

        private bool VerificarDependenciasAmiguinho()
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

        public void VisualizarAmiguinhos()
        {
            string template = "{0, -3} | {1, -20} | {2, -25} | {3, -12} | {4, -20}";

            Console.WriteLine(template, "Id", "Nome", "Nome do responsavel", "Telefone", "Localização");
            Console.WriteLine();

            Amiguinho[] amiguinhos = controladorAmiguinho.SelecionarAmiguinhos();

            if (amiguinhos.Length == 0)
            {
                Console.WriteLine("Nenhum Amiguinho cadastrado");
            }

            foreach(Amiguinho amiguinho in amiguinhos)
            {
                Console.WriteLine(template, amiguinho.Id, amiguinho.Nome, 
                    amiguinho.NomeResponsavel, amiguinho.Telefone, amiguinho.Localizacao);
            }
        }

        #region Opções CRUD
        private class MenuAmiguinhoInserir : GerenciadorMenu
        {
            private MenuAmiguinho menuAmiguinho;

            public MenuAmiguinhoInserir(MenuAmiguinho menuAmiguinho) : base("Inserir")
            {
                this.menuAmiguinho = menuAmiguinho;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();

                Console.Write("Digite o nome do amiguinho: ");
                string nome = Console.ReadLine();

                Console.Write("Digite o nome do responsavel do amiguinho: ");
                string nomeResponsavel = Console.ReadLine();

                Console.Write("Digite o telefone do amiguinho: ");
                string telefone = Console.ReadLine();

                Console.Write("Digite a localização do amiguinho: ");
                string localizacao = Console.ReadLine();

                Console.WriteLine();
                Mensagem msg = menuAmiguinho.controladorAmiguinho.InserirAmiguinho(nome, nomeResponsavel, telefone, localizacao);

                ImprimirMensagem(msg);

                Pausar();
                return null;
            }
        }

        private class MenuAmiguinhoEditar : GerenciadorMenu
        {
            private MenuAmiguinho menuAmiguinho;

            public MenuAmiguinhoEditar(MenuAmiguinho menuAmiguinho) : base("Editar")
            {
                this.menuAmiguinho = menuAmiguinho;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuAmiguinho.VerificarDependenciasAmiguinho())
                    return null;

                Console.Clear();

                menuAmiguinho.VisualizarAmiguinhos();
                Console.Write("\nDigite o id do amiguinho que você deseja editar: ");
                int id = LerInt();

                Console.Write("Digite o nome do amiguinho: ");
                string nome = Console.ReadLine();

                Console.Write("Digite o nome do responsavel do amiguinho: ");
                string nomeResponsavel = Console.ReadLine();

                Console.Write("Digite o telefone do amiguinho: ");
                string telefone = Console.ReadLine();

                Console.Write("Digite a localização do amiguinho: ");
                string localizacao = Console.ReadLine();

                Mensagem msg = menuAmiguinho.controladorAmiguinho.EditarAmiguinho(id, nome, nomeResponsavel, 
                    telefone, localizacao);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuAmiguinhoExcluir : GerenciadorMenu
        {
            private MenuAmiguinho menuAmiguinho;

            public MenuAmiguinhoExcluir(MenuAmiguinho menuAmiguinho) : base("Excluir")
            {
                this.menuAmiguinho = menuAmiguinho;
            }

            public override GerenciadorMenu Executar()
            {
                if (!menuAmiguinho.VerificarDependenciasAmiguinho())
                    return null;

                Console.Clear();
                menuAmiguinho.VisualizarAmiguinhos();
                Console.Write("\nDigite o id do amiguinho que deseja excluir: ");
                int id = LerInt();

                Mensagem msg = menuAmiguinho.controladorAmiguinho.ExcluirAmiguinho(id);

                Console.WriteLine();
                ImprimirMensagem(msg);

                Pausar();

                return null;
            }
        }

        private class MenuAmiguinhoVisualizar : GerenciadorMenu
        {
            private MenuAmiguinho menuAmiguinho;

            public MenuAmiguinhoVisualizar(MenuAmiguinho menuAmiguinho) : base("Visualizar")
            {
                this.menuAmiguinho = menuAmiguinho;
            }

            public override GerenciadorMenu Executar()
            {
                Console.Clear();
                menuAmiguinho.VisualizarAmiguinhos();
                Pausar();

                return null;
            }
        }
        #endregion
    }
}
