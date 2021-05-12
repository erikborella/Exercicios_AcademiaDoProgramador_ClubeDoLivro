using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Controlador;

using ClubeDaLeitura.Menu.Base;

namespace ClubeDaLeitura.Menu
{
    class MenuPrincipal : GerenciadorMenu
    {
        public MenuPrincipal(ControladorCaixa controladorCaixa, ControladorAmiguinho controladorAmiguinho, 
            ControladorRevista controladorRevista, ControladorEmprestimo controladorEmprestimo) : base("Clube do Livro")
        {
            MenuCaixa menuCaixa = new MenuCaixa(controladorCaixa);
            MenuAmiguinho menuAmiguinho = new MenuAmiguinho(controladorAmiguinho);
            MenuRevista menuRevista = new MenuRevista(controladorRevista, controladorCaixa, menuCaixa);
            MenuEmprestimo menuEmprestimo = new MenuEmprestimo(controladorEmprestimo, controladorAmiguinho, controladorRevista,
                menuAmiguinho, menuRevista);

            AdicionarOpcao(menuCaixa);
            AdicionarOpcao(menuRevista);
            AdicionarOpcao(menuAmiguinho);
            AdicionarOpcao(menuEmprestimo);
        }
    }
}
