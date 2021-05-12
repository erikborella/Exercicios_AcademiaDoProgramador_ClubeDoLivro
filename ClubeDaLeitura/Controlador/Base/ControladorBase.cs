using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Controlador.Base
{

    abstract class ControladorBase<T> : ControladorLista<T> where T : DominioValidavel
    {
        private string nomeRegistro;

        protected ControladorBase(int tamanhoMaximo) : base(tamanhoMaximo)
        {
            nomeRegistro = typeof(T).Name;
        }

        protected Mensagem Inserir(T registro)
        {
            string msgValidacao = registro.Validar();

            if (msgValidacao != registro.VERIFICACAO_SUCESSO)
            {
                return new MensagemValidacaoClasse(msgValidacao);
            }

            bool conseguiuInserir = InserirRegistro(registro);

            return new MensagemInserir(conseguiuInserir, nomeRegistro);
        }

        protected Mensagem Editar(T registro)
        {
            string msgValidacao = registro.Validar();

            if (msgValidacao != registro.VERIFICACAO_SUCESSO)
            {
                return new MensagemValidacaoClasse(msgValidacao);
            }

            bool conseguiuEditar = SubstituirInformacoesRegistro(registro);

            return new MensagemEditar(conseguiuEditar, nomeRegistro);
        }

        protected Mensagem Excluir(T registroId)
        {
            bool conseguiuExcluir = ExcluirRegistro(registroId);

            return new MensagemExcluir(conseguiuExcluir, nomeRegistro);
        }
    }
}
