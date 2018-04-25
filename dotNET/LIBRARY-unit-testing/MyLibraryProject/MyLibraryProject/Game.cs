namespace MyLibraryProject {
    public interface IGame {
        string Name { get; }
        decimal Price { get; }
    }

    public class Game : IGame {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Game(string name, decimal price) {
            Name = name;
            Price = price;
        }
    }
}
