using LiberacaoCredito.Business.Interfaces;
using LiberacaoCredito.Business.Models;
using System;

namespace LiberacaoCredito.Services
{
    public class Gerenciador
    {
        #region [Injeção de Depencia]
        private readonly ICreditoService _creditoService;
        public Gerenciador(ICreditoService creditoService)
        {
            _creditoService = creditoService;
        }
        #endregion [Injeção de Depencia]

        public async void Run()
        {
            CreditoModel credito = new CreditoModel();
            int menu = 1;

            Console.WriteLine(new string('_', 75));
            Console.WriteLine("Bem vindo ao sistema de Crédito");
            Console.WriteLine(new string('_', 75));

            while (menu == 1)
            {
                #region [Valor total]
                Console.WriteLine("1º) Insira o valor do crédito:");
                string valorCredito = Console.ReadLine();
                decimal ValorCreditoDec;
                while (!Decimal.TryParse(valorCredito, out ValorCreditoDec) || ValorCreditoDec <= 0)
                {
                    Console.WriteLine("[Erro] - Digite um valor de crédito válido");
                    valorCredito = Console.ReadLine();
                }
                credito.ValorCredito = ValorCreditoDec;
                Console.WriteLine(new string('_', 75));
                #endregion [Valor total]

                #region [Tipo de Credito]
                Console.WriteLine("2º) Insira o tipo de crédito que deseja:");
                Console.WriteLine("[1] - Crédito Direto; [2] - Crédito Consignado");
                Console.WriteLine("[3] - Crédito PJ; [4] - Crédito PF; [5] - Crédito Imobiliario");
                string TipoCredito = Console.ReadLine();
                int TipoCreditoInt;
                while (!Int32.TryParse(TipoCredito, out TipoCreditoInt) || TipoCreditoInt < 1 || TipoCreditoInt > 5)
                {
                    Console.WriteLine("[Erro] - Digite um tipo Valido");
                    TipoCredito = Console.ReadLine();
                }
                credito.TipoCredito = ValidaTipoCredito(TipoCreditoInt);
                Console.WriteLine(new string('_', 75));
                #endregion [Tipo de Credito]

                #region [Quantidade de Parcelas]
                Console.WriteLine("3º) Insira a quantidade de parcelas:");
                string qntParcelas = Console.ReadLine();
                int qntParcelasInt;
                while (!Int32.TryParse(qntParcelas, out qntParcelasInt) || qntParcelasInt == 0)
                {
                    Console.WriteLine("[Erro] - Digite uma quantidade de parcelas Valida");
                    qntParcelas = Console.ReadLine();
                }
                credito.QtdParcelas = qntParcelasInt;
                Console.WriteLine(new string('_', 75));
                #endregion [Quantidade de Parcelas]

                #region [Data de Vencimento]
                Console.WriteLine("4º) Insira a data do primeiro Parcelamento: (DD/MM/YYYY)");
                string dataVencimento = Console.ReadLine();
                DateTime dataVencimentoDate;
                while (!DateTime.TryParse(dataVencimento, out dataVencimentoDate) || dataVencimentoDate.Date <= DateTime.Now.Date)
                {
                    Console.WriteLine("[Erro] - Digite uma data de vencimento Valida");
                    dataVencimento = Console.ReadLine();
                }
                credito.DataVencimento = dataVencimentoDate;
                Console.WriteLine(new string('_', 75));
                #endregion [Data de Vencimento]

                var retorno = await _creditoService.AnaliseCredito(credito);
                if (retorno.Status.ToUpper() == "APROVADO")
                {
                    Console.WriteLine(new string('_', 75));
                    Console.WriteLine("Sua solicitação foi aceita!");
                    Console.WriteLine("Status: " + retorno.Status);
                    Console.WriteLine("Valor Total com Juros: R$" + Math.Round(retorno.ValorTotal, 2));
                    Console.WriteLine("Valor do Juros: R$" + Math.Round(retorno.ValorJuros, 2));
                }
                else
                {
                    Console.WriteLine(new string('_', 75));
                    Console.WriteLine("Sua solicitação foi negada!");
                    Console.WriteLine("Status: " + retorno.Status);
                    Console.WriteLine("Mensagem: " + retorno.MensagemErro);
                }

                #region [Menu]
                Console.WriteLine(new string('-', 75));
                Console.WriteLine(new string('_', 75));
                Console.WriteLine("Você deseja realizar uma nova analise?");
                Console.WriteLine("[1] - Sim ; [0] - Não");
                string menuString = Console.ReadLine();
                while (!Int32.TryParse(menuString, out menu) || menu > 1)
                {
                    Console.WriteLine("[Erro] - Digite uma opção válida");
                    menuString = Console.ReadLine();
                }
                if(menu == 0)
                    Environment.Exit(1);
                #endregion [Menu]
            }
        }

        public TipoCreditoModel ValidaTipoCredito(int Tipo)
        {
            TipoCreditoModel tipoCredito = new TipoCreditoModel();
            switch (Tipo)
            {
                case 1:
                    tipoCredito = TipoCreditoModel.Direto;
                    break;
                case 2:
                    tipoCredito = TipoCreditoModel.Consignado;
                    break;
                case 3:
                    tipoCredito = TipoCreditoModel.Pj;
                    break;
                case 4:
                    tipoCredito = TipoCreditoModel.Pf;
                    break;
                case 5:
                    tipoCredito = TipoCreditoModel.Imobiliario;
                    break;
            }
            return tipoCredito;
        }
    }
}
