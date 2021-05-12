using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Controlador.Base;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Controlador
{
    class ControladorAmiguinho : ControladorBase<Amiguinho>
    {
        public ControladorAmiguinho(int tamanhoMaximo) : base(tamanhoMaximo)
        {
        }

        public Mensagem InserirAmiguinho(string nome, string nomeResponsavel, string telefone, string localizacao)
        {
            Amiguinho amiguinho = new Amiguinho(nome, nomeResponsavel, telefone, localizacao);
            return Inserir(amiguinho);
        }

        public Mensagem EditarAmiguinho(int id, string nome, string nomeResponsavel, string telefone, string localizacao)
        {
            Amiguinho amiguinho = new Amiguinho(id, nome, nomeResponsavel, telefone, localizacao);
            return Editar(amiguinho);
        }

        public Mensagem ExcluirAmiguinho(int id)
        {
            Amiguinho amiguinho = new Amiguinho(id);
            return Excluir(amiguinho);
        }

        public Amiguinho[] SelecionarAmiguinhos()
        {
            return SelecionarRegistros();
        }

        public Amiguinho SelecionarAmiguinho(int id)
        {
            Amiguinho amiguinho = new Amiguinho(id);
            return SelecionarRegistro(amiguinho);
        }
    }
}
