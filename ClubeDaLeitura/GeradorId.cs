using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace ClubeDaLeitura.Id
{
    class GeradorId
    {
        private static Hashtable contadoresDeIdPorTipoDeClasse = new Hashtable();

        public static int PegarProximoId(object obj)
        {
            string nomeDaClasse = obj.GetType().Name;

            CriarChaveCasoNaoExista(nomeDaClasse);

            IncrementarId(nomeDaClasse);
            return PegarId(nomeDaClasse);
        }

        #region metodo privados
        private static void CriarChaveCasoNaoExista(string nome)
        {
            if (!contadoresDeIdPorTipoDeClasse.ContainsKey(nome))
            {
                contadoresDeIdPorTipoDeClasse.Add(nome, 0);
            }
        }

        private static void IncrementarId(string nome)
        {
            int idAtual = PegarId(nome);
            contadoresDeIdPorTipoDeClasse[nome] = idAtual + 1;
        }

        private static int PegarId(string nome)
        {
            return Convert.ToInt32(contadoresDeIdPorTipoDeClasse[nome]);
        }
        #endregion
    }
}