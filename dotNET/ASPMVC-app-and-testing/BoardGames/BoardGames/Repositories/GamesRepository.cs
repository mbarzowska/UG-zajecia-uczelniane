using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Data;
using BoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Repositories {
    public class GamesRepository : IGamesRepository {
        private BGContext _context;

        public GamesRepository(BGContext context) {
            this._context = context;
        }

        public void DeleteGame(int gameID) {
            Game game = _context.Game.Find(gameID);
            _context.Game.Remove(game);
        }

        public async Task<Game> GetGameByID(int gameID) {
            return await _context.Game.SingleOrDefaultAsync(x => x.ID == gameID);
        }

        public async Task<IEnumerable<Game>> GetGames() {
            return await _context.Game.Include(p => p.Publisher).ToListAsync();
        }

        public void AddGame(Game game) {
            _context.Game.Add(game);
        }

        public async Task<bool> Save() {
            try {
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) { return false; };
        }

        public void UpdateGame(Game game) {
            _context.Update(game);
        }

        public bool GameExists(int id) {
            return _context.Game.Any(e => e.ID == id);
        }
    }
}
