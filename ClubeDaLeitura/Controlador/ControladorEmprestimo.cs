using System;
using System.Collections.Generic;
using System.Text;

using ClubeDaLeitura.Controlador.Base;
using ClubeDaLeitura.Dominio;
using ClubeDaLeitura.Log;

namespace ClubeDaLeitura.Controlador
{
    class ControladorEmprestimo : ControladorBase<Emprestimo>
    {
        private ControladorAmiguinho controladorAmiguinho;
        private ControladorRevista controladorRevista;

        public ControladorEmprestimo(ControladorAmiguinho controladorAmiguinho, 
            ControladorRevista controladorRevista, int tamanhoMaximo) : base(tamanhoMaximo)
        {
            this.controladorAmiguinho = controladorAmiguinho;
            this.controladorRevista = controladorRevista;
        }

        public Mensagem RealizarEmprestimo(int amiguinhoId, int revistaId)
        {
            Amiguinho amiguinho = controladorAmiguinho.SelecionarAmiguinho(amiguinhoId);
            Revista revista = controladorRevista.SelecionarRevista(revistaId);

            if (amiguinho == null)
                return new MensagemPersonalizada(false, "Amiguinho não encontrado");

            if (revista == null)
                return new MensagemPersonalizada(false, "Revista não encontrada");

            if (AmiguinhoJaTemEmprestimo(amiguinho))
                return new MensagemPersonalizada(false, $"O amigo {amiguinho.Nome} já realizou um emprestimo antes");
            
            if (RevistaJaFoiEmprestada(revista))
                return new MensagemPersonalizada(false, $"A revista {revista.TipoColecao}:{revista.Ano} já foi emprestada");


            Emprestimo emprestimo = new Emprestimo(amiguinho, revista, DateTime.Now, DateTime.MinValue);

            return Inserir(emprestimo);
        }

        public Mensagem DevolverEmprestimo(int emprestimoId)
        {
            Emprestimo emprestimo = SelecionarEmprestimo(emprestimoId);

            if (emprestimo == null)
                return new MensagemPersonalizada(false, "Emprestimo não encontrado");

            if (emprestimo.Devolvido)
                return new MensagemPersonalizada(false, "Esse emprestimo já foi devolvido");

            emprestimo.DataDevolucao = DateTime.Now;
            emprestimo.Devolvido = true;

            return new MensagemPersonalizada(true, "Emprestimo devolvido com sucesso");
        }

        public Emprestimo SelecionarEmprestimo(int id)
        {
            Emprestimo emprestimo = new Emprestimo(id);
            return SelecionarRegistro(emprestimo);
        }

        public Emprestimo[] SelecionarEmprestimos()
        {
            return SelecionarRegistros();
        }

        public Emprestimo[] SelecionarEmprestimosNaoDevolvidos()
        {
            Emprestimo[] emprestimos = SelecionarEmprestimos();
            Emprestimo[] naoDevolvidos = new Emprestimo[ContarEmprestimosNaoDevolvidos()];

            int count = 0;

            foreach(Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido)
                {
                    naoDevolvidos[count] = emprestimo;
                    count++;
                }
            }

            return naoDevolvidos;
        }

        public Emprestimo[] SelecionarEmprestimosAbertoDia()
        {
            Emprestimo[] emprestimos = SelecionarEmprestimos();
            Emprestimo[] emprestimosHoje = new Emprestimo[ContarEmprestimosAbertoDia()];

            int diaHoje = DateTime.Now.DayOfYear;
            int count = 0;

            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido && emprestimo.DataEmprestimo.DayOfYear == diaHoje)
                {
                    emprestimosHoje[count] = emprestimo;
                    count++;
                }
            }

            return emprestimosHoje;
        }

        public Emprestimo[] SelecionarEmprestimosAbertoMes()
        {
            Emprestimo[] emprestimos = SelecionarEmprestimos();
            Emprestimo[] emprestimosMes = new Emprestimo[ContarEmprestimosAbertoMes()];

            int mesHoje = DateTime.Now.Month;
            int count = 0;

            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.DataEmprestimo.Month == mesHoje)
                {
                    emprestimosMes[count] = emprestimo;
                    count++;
                }
            }

            return emprestimosMes;
        }

        private bool AmiguinhoJaTemEmprestimo(Amiguinho amiguinho)
        {
            Emprestimo[] emprestimos = SelecionarRegistros();

            foreach(Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido && emprestimo.Amiguinho.Equals(amiguinho))
                    return true;
            }

            return false;
        }

        private bool RevistaJaFoiEmprestada(Revista revista)
        {
            Emprestimo[] emprestimos = SelecionarRegistros();

            foreach(Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido && emprestimo.Revista.Equals(revista))
                    return true;
            }

            return false;
        }

        private int ContarEmprestimosNaoDevolvidos()
        {
            int count = 0;
            Emprestimo[] emprestimos = SelecionarEmprestimos();

            foreach(Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido)
                    count++;
            }

            return count;
        }

        private int ContarEmprestimosAbertoDia()
        {
            int count = 0;
            int diaHoje = DateTime.Now.DayOfYear;
            Emprestimo[] emprestimos = SelecionarEmprestimos();

            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (!emprestimo.Devolvido && emprestimo.DataEmprestimo.DayOfYear == diaHoje)
                    count++;
            }

            return count;
        }

        private int ContarEmprestimosAbertoMes()
        {
            Emprestimo[] emprestimos = SelecionarEmprestimos();

            int mesHoje = DateTime.Now.Month;
            int count = 0;

            foreach(Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.DataEmprestimo.Month == mesHoje)
                    count++;
            }

            return count;
        }
    }
}
