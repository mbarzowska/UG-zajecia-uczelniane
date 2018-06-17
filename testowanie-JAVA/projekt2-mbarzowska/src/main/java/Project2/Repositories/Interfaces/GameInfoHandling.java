package Project2.Repositories.Interfaces;

import Project2.Models.Game;

import java.util.List;

public interface GameInfoHandling {
    List getGames();
    Game getGame(String gameName);
    void addGame(Game game);
    boolean gameExists(String gameName);
}
