using Api;
using Api.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    public class ColaboradorService : ConsumirApi
    {
        private readonly Uri uri = new Uri("https://gestao-de-ferias-api-colaborador.azurewebsites.net/colaborador");
        private readonly string rota = "/colaborador";

        public async Task<List<ColaboradorDto>> Buscar(int id = 0)
        {
            var colaboradores = await Buscar<List<ColaboradorDto>>(uri, $"{rota}/{id}", RestSharp.Method.GET);
            return colaboradores;
        }

        public async Task<Response> Criar(int quantidade)
        {
            var resposta = await Enviar<Response>(uri, rota, RestSharp.Method.POST, quantidade);
            return resposta;
        }
           
        public async Task<Response> LimparBanco()
        {
            var resposta = await Enviar<Response>(uri, rota, RestSharp.Method.DELETE, 0);
            return resposta;
        }
    }
}
