using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Data;
using BoardGames.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Repositories {
    public class PublishersRepository : IPublishersRepository {
        private BGContext _context;

        public PublishersRepository(BGContext context) {
            this._context = context;
        }

        public void DeletePublisher(int publisherID) {
            Publisher publisher = _context.Publisher.Find(publisherID);
            _context.Publisher.Remove(publisher);
        }

        public async Task<IEnumerable<Publisher>> GetPublishers() {
            return await _context.Publisher.Include(p => p.Games).ToListAsync();
        }

        public async Task<Publisher> GetPublisherByID(int publisherID) {
            return await _context.Publisher.SingleOrDefaultAsync(x => x.ID == publisherID);
        }

        public void AddPublisher(Publisher publisher) {
            _context.Publisher.Add(publisher);
        }

        public async Task<bool> Save() {
            try {
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) { return false; };
        }

        public void UpdatePublisher(Publisher publisher) {
            _context.Update(publisher);
        }

        public bool PublisherExists(int id) {
            return _context.Publisher.Any(e => e.ID == id);
        }
    }
}
