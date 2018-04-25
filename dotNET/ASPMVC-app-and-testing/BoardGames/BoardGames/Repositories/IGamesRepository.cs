using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGames.Models;

namespace BoardGames.Repositories {
    public interface IGamesRepository {
        Task<IEnumerable<Game>> GetGames();
        Task<Game> GetGameByID(int gameID);
        void AddGame(Game game);
        void DeleteGame(int gameID);
        void UpdateGame(Game game);
        Task<bool> Save();
        bool GameExists(int id);
    }
}
