using LiberacaoCredito.Business.Interfaces;
using LiberacaoCredito.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacaoCredito.Business.Services
{
    public class ValidacaoService : IValidacaoService
    {
        public async Task<List<string>> ValidarCredito(CreditoModel credito)
        {
            List<string> erros = new List<string>();

            if (credito.TipoCredito == TipoCreditoModel.Pj && credito.ValorCredito < Convert.ToDecimal(15000))
                erros.Add("O valor minimo para Crédito Pessoa Juridica é de R$15.000,00");

            if (credito.ValorCredito == 0 || credito.ValorCredito > Convert.ToDecimal(1000000))
                erros.Add("Valor do crédito inválido");

            if (credito.QtdParcelas < 5 || credito.QtdParcelas > 75)
                erros.Add("Quantidade de parcelas deve ser entre 5 e 75");

            if (credito.DataVencimento.Date < DateTime.Now.AddDays(15).Date || credito.DataVencimento.Date > DateTime.Now.AddDays(40).Date)
                erros.Add("O data de vencimento minima é: " + DateTime.Now.AddDays(15).ToString("dd/MM/yyyy") + " e a máxima: " + DateTime.Now.AddDays(40).ToString("dd/MM/yyyy"));

            return erros;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
