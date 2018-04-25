using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;

namespace BoardGames.Repositories {
    public class FakeGamesRepository : IGamesRepository {
        private List<Game> games = new List<Game>();

        public void DeleteGame(int gameID) {
            Game game = games.FirstOrDefault(x => x.ID == gameID);
            games.Remove(game);
        }

        public async Task<IEnumerable<Game>> GetGames() {
            return await Task.Run(() => games);
        }

        public async Task<Game> GetGameByID(int gameID) {
            return await Task.Run(() => games.FirstOrDefault(x => x.ID == gameID));

        }

        public void AddGame(Game game) {
            games.Add(game);
        }

        public async Task<bool> Save() {
            return await Task.Run(() => true);
        }

        public void UpdateGame(Game game) {
            var p = games.FirstOrDefault(x => x.ID == game.ID);
            p = game;
        }

        public bool GameExists(int id) {
            return games.Any(e => e.ID == id);
        }
    }
}
