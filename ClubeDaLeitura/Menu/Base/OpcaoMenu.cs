using System;
using System.Collections.Generic;
using System.Text;

namespace ClubeDaLeitura.Menu.Base
{
    class OpcaoMenu
    {
        private int n;
        private GerenciadorMenu menu;

        public OpcaoMenu(int n, GerenciadorMenu menu)
        {
            this.n = n;
            this.menu = menu;
        }

        public int N { get => n; set => n = value; }
        public GerenciadorMenu Menu { get => menu; set => menu = value; }
    }
}
