using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using SuperSimpleChatGPT.Models;

namespace SuperSimpleChatGPT
{
    public class ChatClient
    {
        private const string BaseUri = "https://api.openai.com";
        private const string ChatCompletionUrl = "/v1/chat/completions";
        private readonly RestClient _client;
        public  ChatClient(string token)
        {
            var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                token, "Bearer");

            var options = new RestClientOptions(BaseUri)
            {
                Authenticator = authenticator
            };

            _client =  new RestClient(options);
        }

        public async Task<ChatCompletionResponse> GetChatCompletion(ChatCompletionRequest chatRequest)
        {
            var restRequest = new RestRequest(ChatCompletionUrl, Method.Post);
            restRequest.AddJsonBody(chatRequest);
            var jsonBody = JsonConvert.SerializeObject(chatRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            restRequest.AddBody(jsonBody, ContentType.Json);
            var response = await _client.ExecuteAsync(restRequest);

            return JsonConvert.DeserializeObject<ChatCompletionResponse>(response.Content);
        }

    }
}