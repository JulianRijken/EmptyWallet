using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyWallet.Model
{
    public class Game
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty("thumbnail", Required = Required.Always)]
        public string Thumbnail { get; set; }

        [JsonProperty("short_description", Required = Required.Always)]
        public string ShortDescription { get; set; }

        [JsonProperty("game_url", Required = Required.Always)]
        public string GameUrl { get; set; }

        [JsonProperty("genre", Required = Required.Always)]
        public string Genre { get; set; }

        [JsonProperty("platform", Required = Required.Always)]
        public string Platform { get; set; }

        [JsonProperty("publisher", Required = Required.Always)]
        public string Publisher { get; set; }

        [JsonProperty("developer", Required = Required.Always)]
        public string Developer { get; set; }

        [JsonProperty("release_date",Required = Required.Always)]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("freetogame_profile_url", Required = Required.Always)]
        public string FreetogameProfileUrl { get; set; }
    }

}
