using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        public static async Task <T> ReadContentAs <T>(this HttpResponseMessage response){
            //verificando se a veio reposta de sucesso
            if (!response.IsSuccessStatusCode) throw new ApplicationException(
                $"Algo Deu errado : {response.ReasonPhrase}");
            // deserializando a resposta json 
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString);
        }

        public static Task<HttpResponseMessage> PostAsJson <T> (this HttpClient httpClient,string ulr, T data){
            // transformando o dado em Json
            var dataAsString = JsonSerializer.Serialize(data);
            // transformando um string em conteudo http
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(ulr,content);
        }

          public static Task<HttpResponseMessage> PutAsJson <T> (this HttpClient httpClient,string ulr, T data){
            // transformando o dado em Json
            var dataAsString = JsonSerializer.Serialize(data);
            // transformando um string em conteudo http
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(ulr,content);
        }
    }
}