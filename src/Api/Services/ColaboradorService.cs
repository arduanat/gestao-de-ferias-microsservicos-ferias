using Api.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    public class ColaboradorService : ConsumirApi
    {
        private readonly Uri uri = new Uri("https://api-gestao-de-ferias-colaborador.azurewebsites.net/Colaborador");

        public async Task<List<ColaboradorDto>> Buscar()
        {
            var rota = "/ObterColaboradores";
            var colaboradores = await Buscar<List<ColaboradorDto>>(uri, rota, RestSharp.Method.GET);
            return colaboradores;
        }
    }
}
