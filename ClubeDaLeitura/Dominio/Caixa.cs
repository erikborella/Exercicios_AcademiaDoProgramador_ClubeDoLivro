using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Id;

namespace ClubeDaLeitura.Dominio
{
    class Caixa : DominioValidavel
    {
        private string cor;
        private string etiqueta;
        private int id;

        public Caixa(string cor, string etiqueta)
        {
            this.id = GeradorId.PegarProximoId(this);
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public Caixa(int id, string cor, string etiqueta)
        {
            this.id = id;
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public Caixa(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            Caixa c = (Caixa)obj;
            return (c != null && this.id == c.id);
        }

        public string Cor { get => cor; }
        public string Etiqueta { get => etiqueta; }
        public int Id { get => id;  }
    }
}
