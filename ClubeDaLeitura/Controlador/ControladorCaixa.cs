using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador.Base;
using ClubeDaLeitura.Log;


namespace ClubeDaLeitura.Controlador
{
    class ControladorCaixa : ControladorBase<Caixa>
    {
        public ControladorCaixa(int tamanhoMaximo) : base(tamanhoMaximo)
        {
        }

        public Mensagem InserirCaixa(string cor, string etiqueta)
        {
            Caixa caixa = new Caixa(cor, etiqueta);
            return Inserir(caixa);
        }

        public Mensagem EditarCaixa(int id, string cor, string etiqueta)
        {
            Caixa caixa = new Caixa(id, cor, etiqueta);
            return Editar(caixa);
        }

        public Mensagem ExcluirCaixa(int id)
        {
            Caixa caixa = new Caixa(id);
            return Excluir(caixa);
        }

        public Caixa[] SelecionarCaixas()
        {
            return SelecionarRegistros();
        }

        public Caixa SelecionarCaixa(int id)
        {
            Caixa caixa = new Caixa(id);
            return SelecionarRegistro(caixa);
        }
    }
}
