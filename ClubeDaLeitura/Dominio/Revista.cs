using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Id;

namespace ClubeDaLeitura.Dominio
    
{
    class Revista : DominioValidavel
    {
        private int id;
        private string tipoColecao;
        private int ano;
        private Caixa caixa;

        public Revista(string tipoColecao, int ano, Caixa caixa)
        {
            this.id = GeradorId.PegarProximoId(this);
            this.tipoColecao = tipoColecao;
            this.ano = ano;
            this.caixa = caixa;
        }

        public Revista(int id, string tipoColecao, int ano, Caixa caixa)
        {
            this.id = id;
            this.tipoColecao = tipoColecao;
            this.ano = ano;
            this.caixa = caixa;
        }

        public Revista(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            Revista r = (Revista)obj;
            return (r != null && r.id == this.id);
        }

        public int Id { get => id; }
        public string TipoColecao { get => tipoColecao; }
        public int Ano { get => ano; }
        public Caixa Caixa { get => caixa; }
    }
}
