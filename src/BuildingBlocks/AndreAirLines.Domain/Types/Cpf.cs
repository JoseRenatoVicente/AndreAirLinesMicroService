using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Types
{
    public struct Cpf
    {
        public string Number { get; private set; }
        public readonly bool IsValid;
        public readonly int Length;
        public const int MaxLength = 11;

        private Cpf(string value)
        {
            Length = value.Length;
            Number = value;

            IsValid = ValidarCPF(Number);

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
            => Number;

        public string ToFormattedString()
        {
            return Number;
        }

        public static implicit operator Cpf(string value)
            => Parse(value);

        public static bool ValidarCPF(Cpf sourceCPF)
        {
            bool IsValid;

            if (sourceCPF.Number == null)
            {
                return false;
            }

            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            bool digitosIdenticos = true;
            var ultimoDigito = -1;

            foreach (var c in sourceCPF.Number)
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
                        totalDigito2 += digito * (MaxLength - posicao);
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

            if (posicao > MaxLength)
            {

                return false;
            }

            if (digitosIdenticos)
            {
                return false;
            }

            var digito1 = totalDigito1 % MaxLength;
            digito1 = digito1 < 2
                ? 0
                : MaxLength - digito1;

            if (dv1 != digito1)
            {
                return false;
            }

            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % MaxLength;
            digito2 = digito2 < 2
                ? 0
                : MaxLength - digito2;

            return IsValid = dv2 == digito2;
        }
    }
}
