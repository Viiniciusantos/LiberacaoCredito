using System.ComponentModel;

namespace LiberacaoCredito.Business.Models
{
    public class ReturnoModel
    {
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorJuros { get; set; }
        public string MensagemErro { get; set; }
    }
}
