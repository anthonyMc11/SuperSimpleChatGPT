using System;
using System.Collections.Generic;
using System.Linq;
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
            var tokensUsedInSession = 0;
            while (true)
            {
                Console.WriteLine($"Thanks, how can I help you today? You have used {tokensUsedInSession} in this session");
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
}

