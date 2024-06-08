using EmptyWallet.Model;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EmptyWallet
{
    public interface IFreeToGameRepository
    {
        public Task<List<Game>> GetAllGames(string genre = "Any", string platform = "Any", string sortMode = "Alphabetical");

        public Task<List<string>> GetSortModes();

        public Task<List<string>> GetGenres();

        public Task<List<string>> GetPlatforms();

    }

}
