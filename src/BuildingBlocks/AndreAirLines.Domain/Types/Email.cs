using System.Text.RegularExpressions;

namespace AndreAirLines.Domain.Types
{
    public struct Email
    {
        public const int EmailMaxLength = 254;
        public const int EmailMinLength = 5;
        public string Endereco { get; private set; }


        public Email(string endereco)
        {
           // if (!Validar(endereco)) throw new DomainException("E-mail inválido");
            Endereco = endereco;
        }

        public static bool Validar(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }

        public static implicit operator Email(string value) =>
                new Email(value);
    }
}