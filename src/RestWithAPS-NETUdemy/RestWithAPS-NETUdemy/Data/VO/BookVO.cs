using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tapioca.HATEOAS;

namespace RestWithAPS_NETUdemy.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        [JsonProperty(Order = 1)]
        [JsonPropertyName("codigo")]
        public long? Id { get; set; }

        [JsonProperty(Order = 2)]
        public string Title { get; set; }
        [JsonProperty(Order = 3)]
        public string Author { get; set; }
        [JsonProperty(Order = 5)]
        public decimal Price { get; set; }
        [JsonProperty(Order = 4)]
        public DateTime LaunchDate { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
