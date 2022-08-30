using DockerProject.Utils;
using RestSharp;
using RestSharp.Serializers.Json;

namespace DockerProject.Services
{
    public static class ApiUtil
    {
        private static readonly RestClient RestClient;

        static ApiUtil()
        {
            RestClient = new RestClient(Configurator.TestData["BaseUrl"]);
            RestClient.UseSerializer(() => new SystemTextJsonSerializer());
        }

        public static RestResponse SendPostRequest(string resource)
        {
            var request = new RestRequest(resource, Method.Post);

            return RestClient.Execute(request);
        }

        public static RestResponse<T> SendPostRequest<T>(string resource)
        {
            var request = new RestRequest(resource, Method.Post);

            return RestClient.Execute<T>(request);
        }

        public static RestResponse SendPostRequest(string resource, object body)
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddObject(body);

            return RestClient.Execute(request);
        }
    }
}