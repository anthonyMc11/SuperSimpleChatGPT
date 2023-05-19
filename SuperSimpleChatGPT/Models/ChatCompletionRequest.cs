using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuperSimpleChatGPT.Models
{
    public class ChatCompletionRequest
    {
        [JsonProperty("model")] public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonProperty("messages")] public List<Message> Messages { get; set; } = new List<Message>();
    }
}