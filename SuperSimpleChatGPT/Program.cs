using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Authenticators.OAuth2;
using Newtonsoft.Json;
using SuperSimpleChatGPT.Models;

namespace SuperSimpleChatGPT
{
    internal class Program
    {

       
        static void Main(string[] args)
        {
            Console.WriteLine("##Welcome to your own personal chatGPT client##");
            Console.WriteLine("##You will need your own personal access token to use this");
            Console.WriteLine("##to get one go to here: https://platform.openai.com/account/api-keys");
            Console.WriteLine("##Go here to check your usage: https://platform.openai.com/account/usage");
            Console.WriteLine("Give me your access token");
            var token = Console.ReadLine();

            var client = new ChatClient(token);
            var tokensUsedinSession = 0;
            while (true)
            {
                Console.WriteLine($"Thanks, how can I help you today? You have used {tokensUsedinSession} in this session");
                var userInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    var request = new ChatCompletionRequest
                    {
                        Messages = new List<Message>
                        {
                            new Message{Content = userInput}
                        }
                    };
                    var response = client.GetChatCompletion(request);
                    tokensUsedinSession += response.Result.Usage.TotalTokens;
                    Console.WriteLine(response.Result.Choices.First().Message.Content);
                }
            }
        }

    }


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

