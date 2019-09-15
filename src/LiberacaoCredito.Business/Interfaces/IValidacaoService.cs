using LiberacaoCredito.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacaoCredito.Business.Interfaces
{
    public interface IValidacaoService : IDisposable
    {
        Task<List<string>> ValidarCredito(CreditoModel credito);
    }
}
