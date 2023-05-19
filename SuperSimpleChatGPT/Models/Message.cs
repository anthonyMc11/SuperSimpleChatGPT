using Newtonsoft.Json;

namespace SuperSimpleChatGPT.Models
{
    public class Message
    {
        [JsonProperty("role")] public string Role { get; set; } = "user";

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}