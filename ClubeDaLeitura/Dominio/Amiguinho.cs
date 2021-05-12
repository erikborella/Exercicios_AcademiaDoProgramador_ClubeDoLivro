using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Id;

namespace ClubeDaLeitura.Dominio
{
    class Amiguinho : DominioValidavel
    {
        private int id;
        private string nome;
        private string nomeResponsavel;
        private string telefone;
        private string localizacao;

        public Amiguinho(string nome, string nomeResponsavel, string telefone, string localizacao)
        {
            this.id = GeradorId.PegarProximoId(this);
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.localizacao = localizacao;
        }

        public Amiguinho(int id, string nome, string nomeResponsavel, string telefone, string localizacao)
        {
            this.id = id;
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.localizacao = localizacao;
        }

        public Amiguinho(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            Amiguinho a = (Amiguinho)obj;
            return (a != null && a.id == this.id);
        }

        public int Id { get => id;  }
        public string Nome { get => nome; }
        public string NomeResponsavel { get => nomeResponsavel; }
        public string Telefone { get => telefone; }
        public string Localizacao { get => localizacao; }
    }
}
