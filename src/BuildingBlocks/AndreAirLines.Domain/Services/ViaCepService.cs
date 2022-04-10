using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Entities.DTO;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Services
{
    public class ViaCepService
    {
        public async Task<Address> ConsultarCEP(Address address)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                HttpResponseMessage response = await client.GetAsync("ws/" + address.CEP + "/json/");

                if (response.IsSuccessStatusCode)
                {
                    ViaCep viaCep = await response.Content.ReadFromJsonAsync<ViaCep>();

                    if (viaCep.bairro != null) address.District = viaCep.bairro;
                    if (viaCep.localidade != null) address.City = viaCep.localidade;
                    if (viaCep.logradouro != null) address.Street = viaCep.logradouro;
                    if (viaCep.uf != null) address.State = viaCep.uf;

                    return address;
                }
                return null;
            }
        }
    }
}
