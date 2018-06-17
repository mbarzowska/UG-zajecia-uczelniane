package Project2.Repositories.Interfaces;

import Project2.Models.BookedGame;

import java.util.List;

public interface BookedGameHandling {
    List getBookedGames();
    BookedGame getBookedGame(String username, String gameName);
    void addBookedGame(BookedGame game);
}
