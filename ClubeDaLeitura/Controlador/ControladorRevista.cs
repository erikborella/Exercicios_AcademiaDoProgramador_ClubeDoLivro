using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Controlador
{
    class ControladorRevista : ControladorBase<Revista>
    {
        private ControladorCaixa controladorCaixa;

        public ControladorRevista(ControladorCaixa controladorCaixa, int tamanhoMaximo) : base(tamanhoMaximo)
        {
            this.controladorCaixa = controladorCaixa;
        }

        public Mensagem InserirRevista(string tipoColecao, int ano, int caixaId)
        {
            Caixa caixa = controladorCaixa.SelecionarCaixa(caixaId);
            Revista revista = new Revista(tipoColecao, ano, caixa);
            return Inserir(revista);
        }

        public Mensagem EditarRevista(int id, string tipoColecao, int ano, int caixaId)
        {
            Caixa caixa = controladorCaixa.SelecionarCaixa(caixaId);
            Revista revista = new Revista(id, tipoColecao, ano, caixa);
            return Editar(revista);
        }

        public Mensagem ExcluirRevista(int id)
        {
            Revista revista = new Revista(id);
            return Excluir(revista);
        }

        public Revista[] SelecionarRevistas()
        {
            return SelecionarRegistros();
        }

        public Revista SelecionarRevista(int id)
        {
            Revista revista = new Revista(id);
            return SelecionarRegistro(revista);
        }

    }
}
