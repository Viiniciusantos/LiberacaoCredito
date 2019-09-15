using LiberacaoCredito.Business.Models;
using System;
using System.Threading.Tasks;

namespace LiberacaoCredito.Business.Interfaces
{
    public interface ICreditoService : IDisposable
    {
        Task<ReturnoModel> AnaliseCredito(CreditoModel credito);
        Task<ReturnoModel> CalcularCredito(CreditoModel credito);
    }
}
