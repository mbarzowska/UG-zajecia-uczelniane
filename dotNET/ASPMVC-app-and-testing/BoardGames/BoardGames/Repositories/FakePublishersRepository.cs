using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;

namespace BoardGames.Repositories {
    public class FakePublishersRepository : IPublishersRepository {
        private List<Publisher> publishers = new List<Publisher>();

        public void DeletePublisher(int publisherID) {
            Publisher publisher = publishers.FirstOrDefault(x => x.ID == publisherID);
            publishers.Remove(publisher);
        }

        public async Task<IEnumerable<Publisher>> GetPublishers() {
            return await Task.Run(() => publishers);
        }

        public async Task<Publisher> GetPublisherByID(int publisherID) {
            return await Task.Run(() => publishers.FirstOrDefault(x => x.ID == publisherID));

        }

        public void AddPublisher(Publisher publisher) {
            publishers.Add(publisher);
        }

        public async Task<bool> Save() {
            return await Task.Run(() => true);
        }

        public void UpdatePublisher(Publisher publisher) {
            var p = publishers.FirstOrDefault(x => x.ID == publisher.ID);
            p = publisher;
        }

        public bool PublisherExists(int id) {
            return publishers.Any(e => e.ID == id);
        }
    }
}
