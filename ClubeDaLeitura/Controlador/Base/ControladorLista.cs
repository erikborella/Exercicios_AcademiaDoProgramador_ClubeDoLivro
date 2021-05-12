using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;

namespace ClubeDaLeitura.Controlador.Base
{
    abstract class ControladorLista<T> where T : class
    {
        protected const int NOVO_REGISTRO = -1;

        private const int POSICAO_NAO_ENCONTRADA = -2;

        private T[] registros;

        protected ControladorLista(int tamanhoMaximo)
        {
            registros = new T[tamanhoMaximo];
        }

        protected bool InserirRegistro(T registro)
        {
            return InserirRegistro(registro, NOVO_REGISTRO);
        }

        protected bool SubstituirInformacoesRegistro(T registro)
        {
            int posicaoRegistro = EncontrarPosicaoRegistro(registro);

            return InserirRegistro(registro, posicaoRegistro);
        }

        protected bool ExcluirRegistro(T registro)
        {
            int posicao = EncontrarPosicaoRegistro(registro);

            if (posicao == POSICAO_NAO_ENCONTRADA)
                return false;

            return ExcluirRegistro(posicao);
        }

        protected T[] SelecionarRegistros()
        {
            T[] registrosSelecionados = new T[QuantidadeDeRegistros()];

            int i = 0;

            foreach(T registro in registros)
            {
                if (registro != null)
                {
                    registrosSelecionados[i] = registro;
                    i++;
                }
            }

            return registrosSelecionados;
        }

        protected T SelecionarRegistro(T registroParaBuscar)
        {
            int posicaoRegistro = EncontrarPosicaoRegistro(registroParaBuscar);

            if (posicaoRegistro == POSICAO_NAO_ENCONTRADA)
                return null;

            return registros[posicaoRegistro];
        }

        private bool InserirRegistro(T registro, int posicao)
        {
            if (posicao == NOVO_REGISTRO)
            {
                posicao = EncontrarPosicaoVazia();
            }

            if (posicao == POSICAO_NAO_ENCONTRADA)
                return false;

            registros[posicao] = registro;

            return true;

        }

        private bool ExcluirRegistro(int posicao)
        {
            registros[posicao] = null;
            return true;
        }

        private int EncontrarPosicaoVazia()
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    return i;
            }

            return POSICAO_NAO_ENCONTRADA;
        }

        private int EncontrarPosicaoRegistro(T registroParaBuscar)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                T registro = registros[i];
                if (registroParaBuscar.Equals(registro))
                    return i;
            }

            return POSICAO_NAO_ENCONTRADA;
        }

        private int QuantidadeDeRegistros()
        {
            int cont = 0;
            foreach(T registro in registros)
            {
                if (registro != null)
                    cont++;
            }
            return cont;
        }
    }
}
