package Project2.Repositories.ListDependent;

import Project2.Models.BookedGame;
import Project2.Repositories.Interfaces.BookedGameHandling;

import java.util.ArrayList;
import java.util.List;

public class BookedGameRepository implements BookedGameHandling {

    private List<BookedGame> bookedGamesList = new ArrayList<BookedGame>();

    @Override
    public List getBookedGames() {
        return bookedGamesList;
    }

    @Override
    public BookedGame getBookedGame(String username, String gameName) {
        return bookedGamesList.stream().filter(x -> x.getUsername().equals(username) && x.getGame().getName().equals(gameName)).findFirst().orElse(null);
    }

    @Override
    public void addBookedGame(BookedGame game) {
        bookedGamesList.add(game);
    }
}
