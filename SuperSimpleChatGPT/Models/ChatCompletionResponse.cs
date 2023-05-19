﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace SuperSimpleChatGPT.Models
{
    public class ChatCompletionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("usage")]
        public Usage Usage { get; set; }

        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }
    }
}