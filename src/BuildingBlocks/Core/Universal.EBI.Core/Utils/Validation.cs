using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Universal.EBI.Core.DomainObjects.Exceptions;

namespace Universal.EBI.Core.Utils
{
    public class Validation
    {
        public static void ValidarSeIgual(object obj1, object obj2, string mensagem)
        {
            if (obj1.Equals(obj2)) throw new DomainException(mensagem);
        }

        public static void ValidarSeDiferente(object obj1, object obj2, string mensagem)
        {
            if (!obj1.Equals(obj2)) throw new DomainException(mensagem);
        }
        
        public static void ValidarSeDiferente(string pattern, string valor, string mensagem)
        {
            var regex = new Regex(pattern);
            if (regex.IsMatch(valor)) throw new DomainException(mensagem);            
        }

        public static void ValidarTamanho(string valor, int minimo, int maximo, string mensagem)
        {
            var tamnho = valor.Trim().Length;
            if (tamnho < minimo || tamnho > maximo) throw new DomainException(mensagem);
        }
                
        public static void ValidarSeNuloVazio(string valor, string mensagem)
        {
            if (string.IsNullOrEmpty(valor)) throw new DomainException(mensagem);
        }

        public static void ValidarSeNulo(object obj, string mensagem)
        {
            if (obj == null) throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo) throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo) throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo) throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(long valor, long minimo, long maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo) throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo) throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(decimal valor, decimal minimo, string mensagem)
        {
            if (valor < minimo) throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(double valor, double minimo, string mensagem)
        {
            if (valor < minimo) throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(float valor, float minimo, string mensagem)
        {
            if (valor < minimo) throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(long valor, long minimo, string mensagem)
        {
            if (valor < minimo) throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(int valor, int minimo, string mensagem)
        {
            if (valor < minimo) throw new DomainException(mensagem);
        }

        public static void ValidarSeFalso(bool param, string mensagem)
        {
            if (!param) throw new DomainException(mensagem);
        }

        public static void ValidarSeVerdadeiro(bool param, string mensagem)
        {
            if (param) throw new DomainException(mensagem);
        }

        public static void ValidarSeExiste(List<object> objs, string mensagem)
        {
            if (!objs.Any()) throw new DomainException(mensagem);
        }
    }
}
