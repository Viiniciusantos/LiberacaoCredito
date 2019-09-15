using System;

namespace LiberacaoCredito.Business.Models
{
    public class CreditoModel
    {
        public decimal ValorCredito { get; set; }
        public TipoCreditoModel TipoCredito { get; set; }
        public int QtdParcelas { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
