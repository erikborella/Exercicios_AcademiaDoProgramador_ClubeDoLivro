using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClubeDaLeitura.Dominio
{
    abstract class DominioValidavel
    {
        public string VERIFICACAO_SUCESSO = "VERIFICACAO_SUCESSO";

        public string Validar()
        {
            StringBuilder mensagemVerificacao = new StringBuilder();
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            foreach (PropertyInfo property in propertyInfos)
            {

                if (property.PropertyType == typeof(string))
                {
                    VerificarString(property, mensagemVerificacao);
                    continue;
                } 

                if (property.PropertyType.IsClass)
                {
                    VerificarClasse(property, mensagemVerificacao);
                    continue;
                }

            }

            if (string.IsNullOrEmpty(mensagemVerificacao.ToString()))
                return VERIFICACAO_SUCESSO;
            else
                return mensagemVerificacao.ToString();
        }

        private void VerificarString(PropertyInfo propertyString, StringBuilder msgVerificacao)
        {
            string valor = propertyString.GetValue(this).ToString();

            if (string.IsNullOrEmpty(valor))
            {
                msgVerificacao.Append($"O campo {propertyString.Name} é obrigatorio");
                msgVerificacao.AppendLine();
            }
        }

        private void VerificarClasse(PropertyInfo propertyClasse, StringBuilder msgVerificacao)
        {
            if (propertyClasse.GetValue(this) is null)
            {
                msgVerificacao.Append($"O campo {propertyClasse.Name} é obrigatorio");
                msgVerificacao.AppendLine();
            }
        }
    }
}
