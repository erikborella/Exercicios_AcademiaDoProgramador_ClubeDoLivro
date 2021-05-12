using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Id;

namespace ClubeDaLeitura.Dominio
{
    class Emprestimo : DominioValidavel
    {
        private int id;
        private Amiguinho amiguinho;
        private Revista revista;
        private DateTime dataEmprestimo;
        private DateTime dataDevolucao;
        private bool devolvido = false;

        public Emprestimo(int id)
        {
            this.id = id;
        }

        public Emprestimo(Amiguinho amiguinho, Revista revista, DateTime dataEmprestimo, DateTime dataDevolucao)
        {
            this.id = GeradorId.PegarProximoId(this);
            this.amiguinho = amiguinho;
            this.revista = revista;
            this.dataEmprestimo = dataEmprestimo;
            this.dataDevolucao = dataDevolucao;
        }

        public override bool Equals(object obj)
        {
            Emprestimo e = (Emprestimo)obj;
            return (e != null && e.id == this.id);
        }

        public int Id { get => id; }
        public DateTime DataEmprestimo { get => dataEmprestimo; }
        public DateTime DataDevolucao { get => dataDevolucao; set => dataDevolucao = value; }
        public Amiguinho Amiguinho { get => amiguinho;  }
        public Revista Revista { get => revista; }
        public bool Devolvido { get => devolvido; set => devolvido = value; }
    }
}
