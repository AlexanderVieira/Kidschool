using System;

namespace Universal.EBI.Core.Utils
{
    public static class DateUtils
    {
        public static bool IsDataInformadaMaiorQueDataAtual(string data)
        {
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataInformada = DateTime.Parse(data).Date;

            return dataInformada > dataAtual;
        }

        public static bool IsDataInformadaMaiorOuIgualQueDataAtual(string data)
        {
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataInformada = DateTime.Parse(data).Date;

            return dataInformada >= dataAtual;
        }

        public static bool IsDataInformadaMenorQueDataAtual(string data)
        {            
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataInformada = DateTime.Parse(data).Date;

            return dataInformada < dataAtual;
        }

        public static bool IsDataInformadaMenorOuIgualQueDataAtual(string data)
        {
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataInformada = DateTime.Parse(data).Date;

            return dataInformada <= dataAtual;
        }

        public static bool IsDataInformadaIgualDataAtual(string data)
        {
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataInformada = DateTime.Parse(data).Date;

            return dataInformada == dataAtual;
        }

        public static bool IsDataEmissaoMenorQueDataNascimento(string dataEmissao, string dataNasc)
        {
            DateTime dataEmissaoDoc = DateTime.Parse(dataEmissao).Date;
            DateTime dataNascimento = DateTime.Parse(dataNasc).Date;

            return dataEmissaoDoc < dataNascimento;
        }

        public static bool IsDataEmissaoMaiorQueDataVencimento(string dataEmissao, string dataVencimento)
        {
            DateTime dataEmissaoDoc = DateTime.Parse(dataEmissao).Date;
            DateTime dataVencimentoDoc = DateTime.Parse(dataVencimento).Date;

            return dataEmissaoDoc > dataVencimentoDoc;
        }

        public static bool IsDatasIguais(string data1, string data2)
        {
            DateTime Data1 = DateTime.Parse(data1).Date;
            DateTime Data2 = DateTime.Parse(data2).Date;

            return Data1 == Data2;
        }

        public static int BuscaIdadePorDataNascimento(string dataNascimento)
        {
            DateTime dtDataNasc = Convert.ToDateTime(dataNascimento);
            int anoCliente = DateTime.Now.Year - dtDataNasc.Year;
            if (dtDataNasc.Month > DateTime.Today.Month || dtDataNasc.Month == DateTime.Today.Month && dtDataNasc.Day > DateTime.Today.Day)
            {
                anoCliente--;
            }

            return anoCliente;
        }

        public static bool IsDataInformadaMaisNDiasMenorQueDataAtual(string data, int dias)
        {
            DateTime dt = Convert.ToDateTime(data);
            return IsDataInformadaMenorQueDataAtual(dt.AddDays(dias).Date.ToString());
        }

        public static bool IsDataInformadaMaisNDiasMaiorQueDataAtual(string data, int dias)
        {
            DateTime dt = Convert.ToDateTime(data);
            return IsDataInformadaMaiorQueDataAtual(dt.AddDays(dias).Date.ToString());
        }
    }
}
