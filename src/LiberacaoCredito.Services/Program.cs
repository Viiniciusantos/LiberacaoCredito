using LiberacaoCredito.Business.Interfaces;
using LiberacaoCredito.Business.Services;
using LiberacaoCredito.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading;

namespace LiberacaoCredito.App
{
    class Program
    {
        private static readonly Container container;

        static Program()
        {
            //Registrando as dependencias
            container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<ICreditoService, CreditoService>(Lifestyle.Scoped);
            container.Register<IValidacaoService, ValidacaoService>(Lifestyle.Scoped);
            container.Register<Gerenciador>();

            container.Verify();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    var service = container.GetInstance<Gerenciador>();
                    service.Run();
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

        }
    }
}
