using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace App.Services
{
    public class ConsumirApi
    {
        protected async Task<TEntity> Enviar<TEntity>(Uri uri, string rota, Method method, object body) where TEntity : class
        {
            RestClient cliente = new RestClient(uri);
            cliente.Timeout = 300000;

            var request = new RestRequest(rota, method) { RequestFormat = DataFormat.Json };

            request.AddJsonBody(body);

            var response = await cliente.ExecuteAsync(request);

            var respotaDaApi = await Task.FromResult(JsonConvert.DeserializeObject<TEntity>(response.Content));

            return respotaDaApi;
        }

        protected static async Task<TEntity> Buscar<TEntity>(Uri uri, string rota, Method method) where TEntity : class
        {
            RestClient cliente = new RestClient(uri);
            cliente.Timeout = 300000;

            var request = new RestRequest(rota, method) { RequestFormat = DataFormat.Json };

            var response = await cliente.ExecuteAsync(request);

            var respotaDaApi = await Task.FromResult(JsonConvert.DeserializeObject<TEntity>(response.Content));

            return respotaDaApi;
        }
    }
}
