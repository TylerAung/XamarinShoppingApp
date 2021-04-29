using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace XamarinCapstoneProj.Models
{
    class MyLocationDataModel
    {
        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("title")]
        public string Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("woeid")]
        public string Woeid { get; set; }

        [JsonProperty("latt_long")]
        public string LattLong { get; set; }
    }
}
