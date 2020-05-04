using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using sm_coding_challenge.Models;

namespace sm_coding_challenge.Services.DataProvider
{
    public class DataProviderImpl : IDataProvider
    {
        public static TimeSpan Timeout = TimeSpan.FromSeconds(30);

     

        public async Task<PlayerModel> GetPlayerById(string id)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                client.Timeout = Timeout;
                var response = await client.GetAsync("https://gist.githubusercontent.com/RichardD012/a81e0d1730555bc0d8856d1be980c803/raw/3fe73fafadf7e5b699f056e55396282ff45a124b/basic.json");
                var stringData = response.Content.ReadAsStringAsync().Result;
                var dataResponse = JsonConvert.DeserializeObject<DataResponseModel>(stringData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                foreach(var player in dataResponse.Rushing)
                {
                    if(player.Id.Equals(id))
                    {
                        return player;
                    }
                }
                foreach(var player in dataResponse.Passing)
                {
                    if(player.Id.Equals(id))
                    {
                        return player;
                    }
                }
                foreach(var player in dataResponse.Receiving)
                {
                    if(player.Id.Equals(id))
                    {
                        return player;
                    }
                }
                foreach(var player in dataResponse.Receiving)
                {
                    if(player.Id.Equals(id))
                    {
                        return player;
                    }
                }
                foreach(var player in dataResponse.Kicking)
                {
                    if(player.Id.Equals(id))
                    {
                        return player;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<PlayerModel>> GetLatestPlayers(string ids)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                client.Timeout = Timeout;
                var response = await client.GetAsync("https://gist.githubusercontent.com/RichardD012/a81e0d1730555bc0d8856d1be980c803/raw/3fe73fafadf7e5b699f056e55396282ff45a124b/output.json");
                var stringData = response.Content.ReadAsStringAsync().Result;
                var dataResponse = JsonConvert.DeserializeObject<DataResponseModel>(stringData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                var returnList = new List<PlayerModel>();
                foreach (var player in dataResponse.Rushing)
                {
                    if (player.Id.Equals(ids))
                    {
                        returnList.Add(player);
                       
                    }
                    
                }

                foreach (var player in dataResponse.Receiving)
                {
                    if (player.Id.Equals(ids))
                    {
                        returnList.Add(player);
                        
                    }
                   
                }
                return returnList;
            }
        }

        public async Task<IEnumerable<BogusModel>> Get1000Players()
        {

                var returnList = new List<BogusModel>();
      
                for (int i = 0; i < 1000; i++)
                {
                var q = new BogusModel();
                q.Id = i.ToString();
                   returnList.Add(q);
                }
                return returnList;
        }
    }
}
