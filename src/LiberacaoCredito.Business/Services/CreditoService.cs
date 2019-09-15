using LiberacaoCredito.Business.Interfaces;
using LiberacaoCredito.Business.Models;
using System;
using System.Threading.Tasks;

namespace LiberacaoCredito.Business.Services
{
    public class CreditoService : ICreditoService
    {
        #region [Injeção de dependencia]
        private readonly IValidacaoService _validacaoService;

        public CreditoService(IValidacaoService validacaoService)
        {
            _validacaoService = validacaoService;
        }
        #endregion [Injeção de dependencia]

        public async Task<ReturnoModel> AnaliseCredito(CreditoModel credito)
        {
            ReturnoModel retorno = new ReturnoModel();
            var erros = await _validacaoService.ValidarCredito(credito);
            if (erros.Count > 0)
            {
                retorno.Status = "Reprovado";
                retorno.MensagemErro = erros[0].ToString();
            }
            else
            {
                retorno = await CalcularCredito(credito);
                retorno.Status = "Aprovado";
            }
            return retorno;
        }

        public async Task<ReturnoModel> CalcularCredito(CreditoModel credito)
        {
            ReturnoModel retorno = new ReturnoModel();

            switch (credito.TipoCredito)
            {
                case TipoCreditoModel.Direto:
                    //Juros de 2% ao mês
                    decimal jurosDireto = Convert.ToDecimal(0.02);
                    retorno.ValorJuros = credito.ValorCredito * jurosDireto * credito.QtdParcelas;
                    retorno.ValorTotal = credito.ValorCredito + retorno.ValorJuros;
                    break;
                case TipoCreditoModel.Consignado:
                    //Juros de 1% ao mês
                    decimal jurosConsignado = Convert.ToDecimal(0.01);
                    retorno.ValorJuros = credito.ValorCredito * jurosConsignado * credito.QtdParcelas;
                    retorno.ValorTotal = credito.ValorCredito + retorno.ValorJuros;
                    break;
                case TipoCreditoModel.Pj:
                    //Juros de 5% ao mês
                    decimal jurosPj = Convert.ToDecimal(0.05);
                    retorno.ValorJuros = credito.ValorCredito * jurosPj * credito.QtdParcelas;
                    retorno.ValorTotal = credito.ValorCredito + retorno.ValorJuros;
                    break;
                case TipoCreditoModel.Pf:
                    //Juros de 3% ao mês
                    decimal jurosPf = Convert.ToDecimal(0.03);
                    retorno.ValorJuros = credito.ValorCredito * jurosPf * credito.QtdParcelas;
                    retorno.ValorTotal = credito.ValorCredito + retorno.ValorJuros;
                    break;
                case TipoCreditoModel.Imobiliario:
                    //Juros de 9% ao mês
                    decimal jurosImobiliario = Convert.ToDecimal(0.09);
                    retorno.ValorJuros = credito.ValorCredito * jurosImobiliario * credito.QtdParcelas;
                    retorno.ValorTotal = credito.ValorCredito + retorno.ValorJuros;
                    break;
            }

            return retorno;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
