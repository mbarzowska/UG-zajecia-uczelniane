using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGames.Models;

namespace BoardGames.Repositories { 
    public interface IPublishersRepository {
        Task<IEnumerable<Publisher>> GetPublishers();
        Task<Publisher> GetPublisherByID(int publisherID);
        void AddPublisher(Publisher publisher);
        void DeletePublisher(int publisherID);
        void UpdatePublisher(Publisher publisher);
        Task<bool> Save();
        bool PublisherExists(int id);
    }
}
