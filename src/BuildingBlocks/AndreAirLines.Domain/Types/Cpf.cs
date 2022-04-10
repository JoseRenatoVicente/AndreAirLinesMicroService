using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Types
{
    public struct Cpf
    {
        private readonly string _value;
        public readonly bool IsValid;
        public readonly int Length;
        public const int LengthCpf = 11;

        private Cpf(string value)
        {
            Length = value.Length;

            _value = value;

            if (value == null)
            {
                IsValid = false;
                return;
            }

            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            bool digitosIdenticos = true;
            var ultimoDigito = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                    {
                        digitosIdenticos = false;
                    }

                    ultimoDigito = digito;
                    if (posicao < 9)
                    {
                        totalDigito1 += digito * (10 - posicao);
                        totalDigito2 += digito * (LengthCpf - posicao);
                    }
                    else if (posicao == 9)
                    {
                        dv1 = digito;
                    }
                    else if (posicao == 10)
                    {
                        dv2 = digito;
                    }

                    posicao++;
                }
            }

            if (posicao > LengthCpf)
            {
                IsValid = false;
                return;
            }

            if (digitosIdenticos)
            {
                IsValid = false;
                return;
            }

            var digito1 = totalDigito1 % LengthCpf;
            digito1 = digito1 < 2
                ? 0
                : LengthCpf - digito1;

            if (dv1 != digito1)
            {
                IsValid = false;
                return;
            }

            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % LengthCpf;
            digito2 = digito2 < 2
                ? 0
                : LengthCpf - digito2;

            IsValid = dv2 == digito2;
        }

        public static Cpf Parse(string value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }
            throw new ArgumentException("Cpf inválido");
        }

        public static bool TryParse(string value, out Cpf cpf)
        {
            cpf = new Cpf(value);
            return true;
        }

        public override string ToString()
            => _value;

        public string ToFormattedString()
        {
            return _value;
        }

        public static implicit operator Cpf(string value)
            => Parse(value);

        public static bool ValidarCPF(Cpf sourceCPF) =>
                sourceCPF.IsValid;
    }
}
