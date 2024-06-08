using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EmptyWallet.Model;
using Newtonsoft.Json;

namespace EmptyWallet.Repository
{
    internal class FreeToGameLocalRepository : IFreeToGameRepository
    {
        private static List<Game>? _games;

        public async Task<List<Game>> GetAllGames(string genre = "Any", string platform = "Any", string sortMode = "Alphabetical")
        {
            if (_games == null)
            {

                string json = ReadEmbeddedResource("EmptyWallet.Resources.Data.games.json");
                await ParseGames(json);
            }

            return await FilterGames(_games, genre, platform, sortMode);
        }

        private async Task<List<Game>> FilterGames(List<Game> games, string genre = "Any", string platform = "Any", string sortMode = "Alphabetical")
        {
            // Copy games list
            var filteredGames = games.ToList();

            if (genre != "Any")
                filteredGames.RemoveAll(p => p.Genre != genre);

            if (platform != "Any")
                filteredGames.RemoveAll(p => p.Platform != platform);

            if (sortMode == "Alphabetical")
            {
                filteredGames = filteredGames.OrderBy(p => p.Title).ToList();
            }

            if (sortMode == "ReleaseDate")
            {
                filteredGames = filteredGames.OrderBy(p => p.ReleaseDate).ToList();
            }

            return filteredGames;
        }

        private async Task ParseGames(string json)
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new CustomDateTimeConverter("yyyy-MM-dd"));
            _games = await Task.Run(() => JsonConvert.DeserializeObject<List<Game>>(json, jsonSettings));
        }

        private string ReadEmbeddedResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new Exception("Resource not found");

            using StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public async Task<List<string>> GetSortModes()
        {
            var sortModes = new List<string>() { "Alphabetical", "ReleaseDate" };
            return sortModes;
        }

        public async Task<List<string>> GetGenres()
        {
            var types = _games.Select(p => p.Genre).Distinct().ToList();
            types.Insert(0, "Any");
            return types;
        }

        public async Task<List<string>> GetPlatforms()
        {
            var types = _games.Select(p => p.Platform).Distinct().ToList();
            types.Insert(0, "Any");
            return types;
        }
    }
}
