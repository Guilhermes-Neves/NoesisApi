using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;

namespace TesteNoesisAPI
{
    public class BaseClass
    {
        private RestClient client;
        private RestRequest endpoint;
        public IRestResponse resp;
        
        private string apiKey = "52ec71bf";



        public RestClient Client(string uri)
        {
            client = new RestClient(uri);
            return client;
        }

        public RestRequest Endpoint(string rota)
        {
            endpoint = new RestRequest(rota);
            return endpoint;
        }

        public void Get()
        {
            endpoint.Method = Method.GET;
            endpoint.RequestFormat = DataFormat.Json;
        }

        public IRestResponse StatusCode(int code)
        {
            resp = client.Execute(endpoint);

            if (resp.IsSuccessful)
            {
                var status = (int)resp.StatusCode;
                Assert.AreEqual(code, status);
            }
            else
            {
                var status = (int)resp.StatusCode;
                var desc = resp.StatusDescription;
                var content = resp.Content;

                Console.WriteLine($"{status} - {desc}");
                Console.WriteLine(content);

                Assert.AreEqual(code, status);
            }

            return resp;
        }

        public void ReturnText()
        {
            JObject obs = JObject.Parse(resp.Content);
            Console.WriteLine(obs);
        }

        public string BuscaValor(dynamic chave)
        {
            dynamic obj = JProperty.Parse(resp.Content);
            var valor = obj[chave];
            return valor;
        }


        public string GetURL(string projeto)
        {
            switch (projeto)
            {
                case "omdBapi":
                    var url = "http://www.omdbapi.com";
                    return url;

                default:
                    return "campo não encontrado";
            }

        }

        public string GetEndpoint(string endpoint, string id, string apiKey)
        {
            switch (endpoint)
            {
                case "movie":
                    var urlEndpoint = $"/?i={id}&apikey={apiKey}";
                    return urlEndpoint;

                default:
                    return "campo não encontrado";
            }

        }

        public void Consulta(string idMovie, string endPointDesejado) 
        {
            Client(GetURL("omdBapi"));
            Endpoint(GetEndpoint(endPointDesejado, idMovie, apiKey));
            Get();
            StatusCode(200);
            ReturnText();
        }

        public bool ValidaTitulo(string titulo)
        {
            return BuscaValor("Title").ToString() == titulo;
        }

        public bool ValidaAno(string ano)
        {
            return BuscaValor("Year").ToString() == ano;
        }

        public bool ValidaIdioma(string idioma)
        {
            return BuscaValor("Language").ToString() == idioma;
        }

        public bool ValidaConsultaFilmeInexistente(string response, string mensagem)
        {
            return BuscaValor("Response").ToString() == response 
                && BuscaValor("Error").ToString() == mensagem;
            
        }
    }
}
