using sm_coding_challenge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sm_coding_challenge.Services.DataProvider
{
    public interface IDataProvider
    {
    Task <PlayerModel> GetPlayerById(string id);
    Task <IEnumerable<PlayerModel>> GetLatestPlayers(string ids);

        Task<IEnumerable<BogusModel>> Get1000Players();
    }
}
